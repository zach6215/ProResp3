using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProResp3.ViewModels
{
    using ProResp3.Commands;
    using System.Windows.Input;
    using ProResp3.Collections;
    using ProResp3.Models;

    public class MainViewModel : BaseViewModel
    {

        private BaseViewModel _selectedViewModel;
        private string _dataFilePath = string.Empty;
        public CheckedValvesCollection _checkBoxWeightRelationship;
        public Experiment experiment;

        public ICommand UpdateViewCommand { get; set; }
        public ICommand CreateFileCommand { get; set; }
        public ICommand StartButtonClick { get; set; }
        public ICommand CheckAllValves { get; set; }


        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set 
            {   
                _selectedViewModel = value; 
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
        public string DataFilePath
        {
            get { return _dataFilePath; }
            set { _dataFilePath = value; OnPropertyChanged(nameof(DataFilePath)); }
        }



        public MainViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
            CreateFileCommand = new CreateFileCommand(this);
            StartButtonClick = new StartExperimentCommand(this);
            CheckAllValves = new CheckAllValvesCommand(this);

            this.SelectedViewModel = new SetupViewModel();
        }
    }
}
