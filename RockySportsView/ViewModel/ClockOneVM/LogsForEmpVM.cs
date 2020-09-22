using RockyClasses;
using RockyClasses.POCO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockySportsView.ViewModel
{
   public class LogsForEmpVM : INotifyPropertyChanged
    {
        public LogsForEmpVM(LogsHolder empLogs, UserInterface inter)
        {
            try
            {
                this.Emp = empLogs;
                EmpName = Emp.logs[0].Name;
                string month = Emp.logs[0].Date.Month.ToString();
                string year = Emp.logs[0].Date.Year.ToString();
                MonthAndYear = $"{month}/{year}";

                this.Logs = emp.logs;
            }

            catch (Exception ex)
            {
                MessageBox.Show("תקלה. אנא נסה שנית או צור קשר");
            }
        }

        private LogsHolder emp;
        public LogsHolder Emp { get { return emp; } set { emp = value; OnPropertyChanged("Emp"); } }
        public string EmpName { get; set; }
        public string MonthAndYear { get; set; }
        private Employee[] logs;
        public Employee[] Logs { get { return logs; } set { logs = value; Emp.logs = Logs; Emp.CalculateAll(); OnPropertyChanged("Emp"); OnPropertyChanged("IsManuallyChanged"); OnPropertyChanged("Logs"); OnPropertyChanged("IsError"); } }
        public Employee SelectedRow { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        //100% ... etc
        //updating every change from the "changed?" column
        //add is chabged manually
        //remove the hour from the date column
        //make object oriented the whole view picker by clocks!
        //bind all the calculations to the changing right away 
        private void OnPropertyChanged (string name)
        {
            if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
