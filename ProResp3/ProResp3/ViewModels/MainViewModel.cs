using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProResp3.ViewModels
{
    using ProResp3.Commands;
    using System.Windows.Input;
    using System.Windows.Controls;
    using ProResp3.UserControls;

    public class MainViewModel : BaseViewModel
    {

        private BaseViewModel _selectedViewModel;
        private string _dataFilePath = string.Empty;
        public CheckedValvesCollection _checkBoxWeightRelationship;
        

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

        public ICommand UpdateViewCommand { get; set; }
        
        public ICommand CreateFileCommand { get; set; }

        public MainViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
            CreateFileCommand = new CreateFileCommand(this);

            this.SelectedViewModel = new SetupViewModel();
        }
    }
}
