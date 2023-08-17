using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ProResp3
{
    using ProResp3.ViewModels;
    using System.Diagnostics;

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
                newCheckBox.DataContext = DataContext;
                

                Binding valveCheckBinding = new Binding();
                valveCheckBinding.Path = new PropertyPath("CheckedValves[" + i + "]");
                valveCheckBinding.Mode = BindingMode.OneWayToSource;
                newCheckBox.SetBinding(CheckBox.IsCheckedProperty, valveCheckBinding);
                BindingOperations.SetBinding(newCheckBox, CheckBox.IsCheckedProperty, valveCheckBinding);

                checkBoxes.Add(newCheckBox);
            }

            return checkBoxes;
        }

    }
}
