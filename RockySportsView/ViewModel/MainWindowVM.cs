using Microsoft.Win32;
using RockyClasses;
using RockyClasses.DAL;
using RockySportsView.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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

            DefineDates();
        }
        public UserInterface Interface { get; set; }

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

        private int[] months;
        public int[] Months
        {
            get
            {
                return months;
            }

            set
            {
                months = value;
                OnPropertyChanged("Months");
            }
        }

        private int[] years;
        public int[] Years
        {
            get
            {
                return years;
            }

            set
            {
                years = value;
                OnPropertyChanged("Years");
            }
        }
        private List<string> names;
        public List<string> Names
        {
            get
            {
                return names;
            }

            set
            {
                names = value;
                OnPropertyChanged("Names");
            }
        }

        private int selectedYear;
        public int SelectedYear
        {
            get
            {
                return selectedYear;
            }

            set
            {
                selectedYear = value;
                OnPropertyChanged("SelectedYear");
                DefineEmpList();
            }
        }

        private int selectedMonth;
        public int SelectedMonth
        {
            get
            {
                return selectedMonth;
            }

            set
            {
                selectedMonth = value;
                OnPropertyChanged("SelectedMonth");
                DefineEmpList();
            }
        }

        private bool isRadiosEnabled;
        public bool IsRadiosEnabled
        {
            get
            {
                return isRadiosEnabled;
            }

            set
            {
                isRadiosEnabled = value;
                OnPropertyChanged("IsRadiosEnabled");
            }
        }

        private bool isButtonEnabledAllEmp;
        public bool IsButtonEnabledAllEmp
        {
            get { return isButtonEnabledAllEmp; }
            set
            {
                isButtonEnabledAllEmp = value;
                OnPropertyChanged("IsButtonEnabledAllEmp");
            }
        }

        private bool isListEnabled;
        public bool IsListEnabled
        {
            get { return isListEnabled; }
            set
            {
                isListEnabled = value;
                OnPropertyChanged("IsListEnabled");
            }
        }

        private string selectedEmp;
        public string SelectedEmp
        {
            get { return selectedEmp; }
            set {
                selectedEmp = value;
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
        private void DefineDates ()
        {
            years = new int[2] { Convert.ToInt32(DateTime.Now.Year), Convert.ToInt32(DateTime.Now.Year) - 1};
            months = new int[12] { 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12 };
        }
        private void DefineEmpList ()
        {
            if (SelectedMonth != 0 && SelectedYear != 0)
            {
                List<string> tempNames = new List<string>();

                Employee[] logs = Interface.GetAllMonthAndYeah(SelectedMonth, SelectedYear);
                string currentName = null;

                if (logs.Length > 0)
                {
                    currentName = logs[0].Name;
                }


                for (int i = 0; i < logs.Length; i++)
                {
                    if (currentName != logs[i].Name)
                    {
                        tempNames.Add(currentName);
                        currentName = logs[i].Name;
                    }

                    if (i == logs.Length - 1)
                    {
                        tempNames.Add(currentName);
                    }
                }

                Names = tempNames;
                IsRadiosEnabled = true;
            }
        }

    }
}
