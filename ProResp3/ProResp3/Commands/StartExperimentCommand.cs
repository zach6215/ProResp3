using ProResp3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProResp3.Commands
{
    using ProResp3.Collections;
    internal class StartExperimentCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private MainViewModel viewModel;
        public StartExperimentCommand(MainViewModel argViewModel)
        {
            this.viewModel = argViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter?.ToString() == "Experiment")
            {
                CheckedValvesCollection testCheckedValvesCollection = new CheckedValvesCollection(Globals.NumValves);
                ValveWeightCollection testValveWeightCollection = new ValveWeightCollection(Globals.NumValves);

                testCheckedValvesCollection[3] = true;
                testValveWeightCollection[3] = "0";

                //Get experiment variables from setup context and MainViewModel
                GetExperimentArgs();


                viewModel.SelectedViewModel = new ExperimentViewModel(testCheckedValvesCollection, testValveWeightCollection, "15");
            }
            
        }

        void GetExperimentArgs()
        {

        }
    }
}
