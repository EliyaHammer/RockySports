using RockyClasses.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockySportsView.ViewModel
{
    class LogsForEmpVM
    {
        private LogsHolder emp;
        public string EmpName { get; set; }
        public string MonthAndYear { get; set; }
        public LogsForEmpVM(LogsHolder empLogs)
        {
            this.emp = empLogs;
            EmpName = emp.logs[0].Name;
            string month = emp.logs[0].Date.Month.ToString();
            string year = emp.logs[0].Date.Year.ToString();
            MonthAndYear = $"{month}/{year}";
        }
    }
}
