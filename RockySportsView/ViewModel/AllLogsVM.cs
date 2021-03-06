﻿using RockyClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RockySportsView.ViewModel
{
    public class AllLogsVM : INotifyPropertyChanged
    {
        private Employee[] logs;

        public event PropertyChangedEventHandler PropertyChanged;
        public Employee[] Logs { get { return logs; } set { logs = value; OnPropertyChanged("Logs"); } }
        public string MonthAndYear { get; set; }
        public Employee SelectedRow { get; set; }
        public AllLogsVM (Employee[] logs, UserInterface inter)
        {
            try
            {
                this.Logs = logs;
                string month = Logs[0].Date.Month.ToString();
                string year = Logs[0].Date.Year.ToString();
                MonthAndYear = $"{month}/{year}";
            }

            catch (Exception ex)
            {
                MessageBox.Show("תקלה. אנא נסה שנית או צור קשר");
            }

        }

        private void OnPropertyChanged (string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

