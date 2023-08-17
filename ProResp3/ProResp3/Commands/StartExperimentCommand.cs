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
                //Testing Experiment
                CheckedValvesCollection testCheckedValvesCollection = new CheckedValvesCollection(Globals.NumValves);
                ValveWeightCollection testValveWeightCollection = new ValveWeightCollection(Globals.NumValves);

                testCheckedValvesCollection[3] = true;
                testValveWeightCollection[3] = "0";

                
                try
                {
                    if (this.viewModel.SelectedViewModel.GetType() == typeof(SetupViewModel))
                    {
                        SetupViewModel localSetup = (SetupViewModel)this.viewModel.SelectedViewModel;

                        viewModel.SelectedViewModel = new ExperimentViewModel(this.viewModel.Checked, testValveWeightCollection, "15");
                    }
                    else
                    {
                        throw new Exception("Error: SelectedViewModel not of type SetupViewModel.");
                    }
                }
                catch (Exception ex)
                {

                }

                this.viewModel.ExperimentRunning = true;
            }
        }

        void GetExperimentArgs()
        {

        }
    }
}
