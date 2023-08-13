using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProResp3.ViewModels
{
    using System.Windows.Input;
    using ProResp3.Commands;
    using System.Windows.Controls;

    public class MainViewModel : BaseViewModel
    {
        private const int _numValves = 24;
        private List<CheckBox> valveCheckBoxes = new List<CheckBox>();
        private BaseViewModel _selectedViewModel = new SetupViewModel();
        private string _dataFilePath = string.Empty;

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { _selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); }
        }
        public string DataFilePath
        {
            get { return _dataFilePath; }
            set { _dataFilePath = value; OnPropertyChanged(nameof(DataFilePath)); }
        }

        public List<CheckBox> ValveCheckBoxes
        {
            get { return valveCheckBoxes; }
            set { valveCheckBoxes = value; OnPropertyChanged(nameof(ValveCheckBoxes));}
        }

        public ICommand UpdateViewCommand { get; set; }
        
        public ICommand CreateFileCommand { get; set; }

        public MainViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
            CreateFileCommand = new CreateFileCommand(this);

            UpdateValveCheckBoxes(_numValves);
        }

        private void UpdateValveCheckBoxes(int newAmount)
        {
            List<CheckBox> checkBoxes = new List<CheckBox>();

            for(int i = 0; i < newAmount; i++)
            {
                CheckBox newCheckBox = new CheckBox();
                newCheckBox.Content = "Valve " + (i + 1).ToString();
                checkBoxes.Add(newCheckBox);
            }

            this.ValveCheckBoxes = checkBoxes;
        }
    }
}
