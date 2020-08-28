using RockyClasses.POCO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockySportsView.ViewModel
{
    class LogsForEmpVM : INotifyPropertyChanged
    {
        public LogsForEmpVM(LogsHolder empLogs)
        {
            this.Emp = empLogs;
            EmpName = Emp.logs[0].Name;
            string month = Emp.logs[0].Date.Month.ToString();
            string year = Emp.logs[0].Date.Year.ToString();
            MonthAndYear = $"{month}/{year}";
        }

        private LogsHolder emp;
        public LogsHolder Emp { get { return emp; } set { emp = value; OnPropertyChanged("Emp"); } }
        public string EmpName { get; set; }
        public string MonthAndYear { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        //100% ... etc
        //errors showing
        //updating every change from the "changed?" column
        //remove the hour from the date column
        //show the total calculations 
        private void OnPropertyChanged (string name)
        {
            if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
