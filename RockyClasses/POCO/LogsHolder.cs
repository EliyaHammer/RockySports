using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses.POCO
{
   public class LogsHolder
    {
        public Employee[] logs { get; private set; }
        public int TotalAbsence { get; private set; }
        public int TotalDaysOfWork { get; private set; }
        public int? TotalEarlyLeave { get; private set; }
        public int? TotalMinutesLate { get; private set; }

        public LogsHolder(Employee[] logs)
        {
            this.logs = logs;
        }


        public void CalculateAll()
        {
            CalcuateTotalEarlyLeave();
            CalculateTotalLates();
            CalculateAbsence();
            CalculateTotalWorkingDays();
        }//ready

        private void CalculateAbsence()
        {
            foreach (Employee log in logs)
            {
                if (log.IsAbsance == 1)
                    TotalAbsence++;
            }
        }//check

        private void CalculateTotalWorkingDays()
        {
            int total = logs.Length - TotalAbsence;
            this.TotalDaysOfWork = total;
        }//check

        private void CalculateTotalLates()
        {
            foreach (Employee log in logs)
            {
                TotalMinutesLate += log.MinutesLate;
            }
        } // check

        private void CalcuateTotalEarlyLeave()
        {

            foreach (Employee log in logs)
            {
                TotalEarlyLeave += log.MinutesEarlyLeave;
            }

        }
    }
}
