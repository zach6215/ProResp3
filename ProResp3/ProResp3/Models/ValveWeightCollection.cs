﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProResp3.Models
{
    using System.ComponentModel;
    using System.Windows.Data;
    internal class ValveWeightCollection : INotifyPropertyChanged
    {
        string[] _weights;
        public event PropertyChangedEventHandler? PropertyChanged;

        public ValveWeightCollection(int size)
        {
            _weights = new string[size];
        }

        public string this[int index]
        {
            get 
            { 
                if (index < _weights.Length)
                {
                    return _weights[index];
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Index {index} is not in range of current ValveWeightCollection");
                }
                
            }

            set
            {
                if (index < _weights.Length)
                {
                    _weights[index] = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Binding.IndexerName));
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Index {index} is not in range of current ValveWeightCollection");
                }
            }
        }
    }
}
