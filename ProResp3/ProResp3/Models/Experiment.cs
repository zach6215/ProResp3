using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProResp3.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Markup;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    public class Experiment : INotifyPropertyChanged
    {
        private LI7000Connection _LI7000;
        private Valve _activeValve;
        private DateTime startDate;
        private List<int> _activeValveNums = new List<int>();
        private List<double> _valveWeights = new List<double>();
        private int _activeValveIndex;
        private int _valveSwitchTimeMin;
        private int _dataPollTimeSec;
        DispatcherTimer pollDataTimer;
        DispatcherTimer valveSwitchTimer;
        private string _dataFilePath;
        MccBoardConnection _board;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Valve ActiveValve { get { return _activeValve; } set { _activeValve = value; } }
        public string DataHeader { get; private set; }

        public Experiment(List<int> argActiveValveNums, List<double> argValveWeights, int argValveSwitchTimeMin, string argDataFilePath)
        {
            _activeValveNums = argActiveValveNums;
            _valveWeights = argValveWeights;
            _valveSwitchTimeMin = argValveSwitchTimeMin;
            _dataFilePath = argDataFilePath;

            _board = new MccBoardConnection();

            _LI7000 = new LI7000Connection();

            //Activate first valve
            this._board.TurnOffAllPorts();
            this._activeValve = new Valve(this._activeValveNums.First());
            this._board.open(this._activeValveNums[this._activeValveIndex]);

            //Add units to ActiveValve
            string[] LI7000Units = _LI7000.DataHeader.Split('\t');
            for(int i = 0; i < LI7000Units.Length; i++)
            {
                if (LI7000Units[i].Contains("CO2"))
                {
                    LI7000Units[i] = LI7000Units[i].Substring(LI7000Units[i].IndexOf(' ') + 1);
                    this._activeValve.CO2Units = LI7000Units[i];
                }
                else if (LI7000Units[i].Contains("H2O"))
                {
                    LI7000Units[i] = LI7000Units[i].Substring(LI7000Units[i].IndexOf(' ') + 1);
                    this._activeValve.H2OUnits = LI7000Units[i];
                }
                else if (LI7000Units[i].Contains("T"))
                {
                    LI7000Units[i] = LI7000Units[i].Substring(LI7000Units[i].IndexOf(' ') + 1);
                    this._activeValve.TemperatureUnits = LI7000Units[i];
                }
            }


            //Setup Timers
            this.pollDataTimer = new DispatcherTimer();
            this.pollDataTimer.Interval = TimeSpan.FromSeconds(this._dataPollTimeSec);
            this.pollDataTimer.Tick += this.PollData;

            this.valveSwitchTimer = new DispatcherTimer();
            this.valveSwitchTimer.Interval = TimeSpan.FromMinutes(this._valveSwitchTimeMin);
            this.valveSwitchTimer.Tick += this.SwitchValve;

            //Write data header
            this.SetDataHeader();
            this.WriteDataHeader();
        }

        private void SetDataHeader()
        {
            this.DataHeader = "Day of Experiment\tDate (mm/dd/yyyy) ";
            this.DataHeader += this._LI7000.DataHeader;

            this.DataHeader = this.DataHeader.Replace("ppm", "(ppm)");
            this.DataHeader = this.DataHeader.Replace("mm/m", "(mm/m)");
            this.DataHeader = this.DataHeader.Replace("T C", "Temp. (C)");
        }

        void PollData(object sender, EventArgs e)
        {
            string response = _LI7000.Poll();

            if (response?.Substring(0, 5) == "DATA\t")
            {
                response = response.Substring(5);
                response = response.Replace("\n", string.Empty);

                string[] headers = this._LI7000.DataHeader.Split('\t');
                string[] data = response.Split('\t');
                for (int i = 0; i < headers.Length; i++)
                {
                    switch (headers[i][0])
                    {
                        case 'C':
                            this.ActiveValve.CO2 = double.Parse(data[i]);
                            break;
                        case 'H':
                            this.ActiveValve.H2O = double.Parse(data[i]);
                            break;
                        case 'T':
                            this.ActiveValve.Temperature = double.Parse(data[i]);
                            break;
                    }
                }
            }
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActiveValveData"));
        }

        public void Start()
        {
            this.pollDataTimer?.Start();
            this.valveSwitchTimer?.Start();
        }

        public void SwitchValve(object sender, EventArgs e)
        {
            this._board.close(this.ActiveValve.ValveNum);

            if (this._activeValveIndex + 1 < this._activeValveNums.Count)
            {
                this._activeValveIndex++;
            }
            else
            {
                this._activeValveIndex = 0;
            }

            this._board.open(this._activeValveNums[this._activeValveIndex]);
            this.ActiveValve.ValveNum = this._activeValveNums[this._activeValveIndex];

            this.WriteData();

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActiveValve"));

            return;
        }

        private void WriteDataHeader()
        {
            using (StreamWriter sw = new StreamWriter(this._dataFilePath, false))
            {
                sw.WriteLine(this.DataHeader);
                sw.Close();
            }
        }

        private void WriteData()
        {
            string data = string.Empty;

            data = DateTime.Now.ToString();
            data += "\t" + (this.ActiveValve.ValveNum + 1).ToString();

            using (StreamWriter sw = new StreamWriter(this._dataFilePath, true))
            {
                sw.WriteLine(data);
                sw.Close();
            }
        }
    }
}
