using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProResp3.ViewModels
{
    using ProResp3.Models;
    using ProResp3.Collections;
    using System.ComponentModel;

    public class ExperimentViewModel : BaseViewModel
    {
        Experiment experiment;
        string _activeValveNum;
        string _currentCO2;
        string _currentH2O;
        string _currentTemperature;
        string _previousValveNum;
        string _previousCO2;
        string _previousH2O;
        string _previousTemperature;

        public string ActiveValveNum
        {
            get { return  _activeValveNum; }
            set 
            { 
                _activeValveNum = value; 
                OnPropertyChanged(nameof(ActiveValveNum));
            }
        }
        public string CurrentCO2
        {
            get { return _currentCO2; }
            set
            {
                _currentCO2 = value;
                OnPropertyChanged(nameof(CurrentCO2));
            }
        }
        public string CurrentH2O
        {
            get { return _currentH2O; }
            set
            {
                _currentH2O = value;
                OnPropertyChanged(nameof(CurrentH2O));
            }
        }
        public string CurrentTemperature
        {
            get { return _currentTemperature; }
            set
            {
                _currentTemperature = value;
                OnPropertyChanged(nameof(CurrentTemperature));
            }
        }
        public string PreviousValveNum
        {
            get { return _previousValveNum; }
            set
            {
                _previousValveNum = value;
                OnPropertyChanged(nameof(PreviousValveNum));
            }
        }
        public string PreviousCO2
        {
            get { return _previousCO2; }
            set
            {
                _previousCO2 = value;
                OnPropertyChanged(nameof(PreviousCO2));
            }
        }
        public string PreviousH2O
        {
            get { return _previousH2O; }
            set
            {
                _previousH2O = value;
                OnPropertyChanged(nameof(PreviousH2O));
            }
        }
        public string PreviousTemperature
        {
            get { return _previousTemperature; }
            set
            {
                _previousTemperature = value;
                OnPropertyChanged(nameof(PreviousTemperature));
            }
        }

        public ExperimentViewModel()
        {

        }

        public ExperimentViewModel(ValveWeightCollection argValveWeights, MainViewModel mainViewModel)
        {
            List <int> activeValvesNums = new List <int>();
            List <double> valveWeights = new List <double>();

            for (int i = 0; i < Globals.NumValves; i++)
            {
                if (mainViewModel.CheckedValves[i] == true)
                {
                    activeValvesNums.Add(i);

                    if (double.TryParse(argValveWeights[i], out double valveWeight))
                    {
                        valveWeights.Add(valveWeight);
                    }
                }
            }

            if (activeValvesNums.Count > 0 && int.TryParse(mainViewModel.ValveSwitchTime, out int valveSwitchTime))
            {
                experiment = new Experiment(activeValvesNums, valveWeights, valveSwitchTime, mainViewModel.DataFilePath);
                experiment.PropertyChanged += this.ExperimentUpdated;
                experiment.Start();
            }

            //Initialize previous valve data
            PreviousValveNum = "n/a";
            PreviousCO2 = "n/a";
            PreviousH2O = "n/a";
            PreviousTemperature = "n/a";
        }

        public void ExperimentUpdated(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ActiveValve")
            {
                PreviousValveNum = ActiveValveNum;
                PreviousCO2 = CurrentCO2;
                PreviousH2O = CurrentH2O;
                PreviousTemperature = CurrentTemperature;
            }
            if (e.PropertyName == "ActiveValveData")
            {
                ActiveValveNum = (this.experiment.ActiveValve.ValveNum + 1).ToString();
                CurrentCO2 = this.experiment.ActiveValve.CO2.ToString() + " " + this.experiment.ActiveValve.CO2Units;
                CurrentH2O = this.experiment.ActiveValve.H2O.ToString() + " " + this.experiment.ActiveValve.H2OUnits;
                CurrentTemperature = this.experiment.ActiveValve.Temperature.ToString() + " " + this.experiment.ActiveValve.TemperatureUnits;
            }
        }
    }
}
