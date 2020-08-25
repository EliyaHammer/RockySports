using Microsoft.Win32;
using RockyClasses;
using RockyClasses.DAL;
using RockySportsView.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RockySportsView.ViewModel
{
    class MainWindowVM : INotifyPropertyChanged
    {
        public MainWindowVM(Window hide)
        {
            if (ConfigurationManager.AppSettings["ClockType"] == "")
            {
                ChooseClock(hide);
            }
            else if (ConfigurationManager.AppSettings["ClockType"] == "One")
            {
                Interface = new UserInterface(ClocksEnum.ClockOne);
            }
            else if (ConfigurationManager.AppSettings["ClockType"] == "Two")
            {
                Interface = new UserInterface(ClocksEnum.ClockTwo);
            }
            else if (ConfigurationManager.AppSettings["ClockType"] == "Three")
            {
                Interface = new UserInterface(ClocksEnum.ClockThree);
            }
            //here check if the sql exists, if not > create !
            //else > check the type > make a user interface.
        }
        private UserInterface Interface { get; set; }

        private string isSucceeded;
        public string IsSucceeded { 
            get
            {
                return this.isSucceeded;
            }

            set
            {
                this.isSucceeded = value;
                OnPropertyChanged("IsSucceeded");
            }
        }
        public string FilePath { get; set; }

        private bool isEnabled = true;
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        //here make a variable for emp names 

        public event PropertyChangedEventHandler PropertyChanged;



        private void OnPropertyChanged (string name)
        {
            if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public void ChooseClock (Window hide)
        {
            ChooseClockView clock = new ChooseClockView();
            clock.Show();
            hide.Close();
        }
        public void Import ()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "(.xls)|*.xls";
            openFile.ShowDialog();
            FilePath = openFile.FileName;

            bool succeeded = false;

            Task import = new Task(() =>
            {
                succeeded = Interface.Import(FilePath);
            });

            IsSucceeded = "";
            import.Start();
            isEnabled = false;
            //here show the dots or whatever- use is completed
            import.Wait();

            if (succeeded == true)
                IsSucceeded = "ייבוא בוצע בהצלחה";
            else
                IsSucceeded = "הייבוא נכשל";

            isEnabled = true;
            //here stop the dots

        }
        private void DefineEmpList (int month = 0, int year = 0)
        {
            //this is already for choosing by times.
            if (month == 0 || year == 0)
            {
            }

            else
            {

            }
        }


    }
}
