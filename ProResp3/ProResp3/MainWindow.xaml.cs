using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProResp3
{
    using ProResp3.ViewModels;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
            this.selectValveListBox.ItemsSource = MakeValveCheckBoxes(Globals.NumValves);
        }

        private List<CheckBox> MakeValveCheckBoxes(int newAmount)
        {
            List<CheckBox> checkBoxes = new List<CheckBox>();

            for (int i = 0; i < newAmount; i++)
            {
                CheckBox newCheckBox = new CheckBox();
                newCheckBox.Name = "valve" + (i + 1).ToString() + "CheckBox";
                newCheckBox.Content = "Valve " + (i + 1).ToString();
                checkBoxes.Add(newCheckBox);

                //Add databinding to checkbox
            }

            return checkBoxes;
        }
    }
}
