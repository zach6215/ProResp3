using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProResp3.ViewModels
{
    using ProResp3.Models;
    using ProResp3.Collections;

    public class ExperimentViewModel : BaseViewModel
    {
        Experiment experiment;

        public Valve ActiveValve { get; set; }

        //public string ValveNum
        //{
        //    get
        //    {
        //        return 
        //    }
        //    set;
        //}

        public ExperimentViewModel()
        {

        }

        public ExperimentViewModel(CheckedValvesCollection argActiveValves, ValveWeightCollection argValveWeights, string argValveSwitchTime)
        {
            List <int> activeValvesNums = new List <int>();
            List <double> valveWeights = new List <double>();

            for (int i = 0; i < Globals.NumValves; i++)
            {
                if (argActiveValves[i] == true)
                {
                    activeValvesNums.Add(i);

                    if (double.TryParse(argValveWeights[i], out double valveWeight))
                    {
                        valveWeights.Add(valveWeight);
                    }
                }
            }

            if (activeValvesNums.Count > 0 && int.TryParse(argValveSwitchTime, out int valveSwitchTime))
            {
                experiment = new Experiment(activeValvesNums, valveWeights, valveSwitchTime);
            }
        }

        void ValveDataUpdated(object sender, EventArgs e)
        {
            //switch (string e.ToString())
        }
    }
}
