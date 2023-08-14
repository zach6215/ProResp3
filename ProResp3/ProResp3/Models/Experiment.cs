﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProResp3.Models
{
    using System.Collections.Generic;
    using System.Windows.Threading;

    internal class Experiment
    {
        private LI7000Connection _LI7000;
        private Valve _activeValve;
        private List<int> _activeValveNums = new List<int>();
        private int _currentValveIndex;
        private int _valveSwitchTimeMin;
        private int _dataPollTimeSec;
        DispatcherTimer pollDataTimer;
        DispatcherTimer valveSwitchTimer;

        public Valve ActiveValveData { get { return _activeValve; } set { _activeValve = value; } }
        public string DataHeader { get; private set; }
        public string FileLocation { get; set; }

        public Experiment(List<int> argActiveValveNums, int argValveSwitchTimeMin)
        {
            _activeValveNums = argActiveValveNums;
            _valveSwitchTimeMin = argValveSwitchTimeMin;

            _LI7000 = new LI7000Connection();

            this.pollDataTimer = new DispatcherTimer();
            this.pollDataTimer.Interval = TimeSpan.FromSeconds(this._dataPollTimeSec);
            this.pollDataTimer.Tick += this.PollData;

        }

        void PollData(object sender, EventArgs e)
        {
            Valve resultValve = new Valve(_activeValveNums[_currentValveIndex]);
            string response;

            response = _LI7000.Poll();
            ActiveValveData = resultValve;

        }

        void Start()
        {

        }

        void SwitchValve()
        {

        }

    }
}