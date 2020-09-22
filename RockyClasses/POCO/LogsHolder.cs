using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses.POCO
{
   public class LogsHolder
    {
        public Employee[] logs { get; set; }
        public int TotalAbsence { get; private set; }
        public int TotalDaysOfWork { get; private set; }
        public int? TotalEarlyLeave { get; private set; }
        public int? TotalMinutesLate { get; private set; }
        private TimeSpan? totalWorkingHours { get; set; }
        public string TotalWorkingHours { get; private set; }
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
            CalculateTotalWorkingHours();
        }//ready

        private void CalculateAbsence()
        {
            TotalAbsence = 0;

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
            TotalMinutesLate = 0;

            foreach (Employee log in logs)
            {
                TotalMinutesLate += log.MinutesLate;
            }
        } // check

        private void CalcuateTotalEarlyLeave()
        {
            TotalEarlyLeave = 0;

            foreach (Employee log in logs)
            {
                TotalEarlyLeave += log.MinutesEarlyLeave;
            }

        }
        private void CalculateTotalWorkingHours  () 
        {
            totalWorkingHours = new TimeSpan(0,0,0);
            TimeSpan? endTime = new TimeSpan();
            TimeSpan? startTime = new TimeSpan();
            TimeSpan? endTime2 = new TimeSpan();
            TimeSpan? startTime2 = new TimeSpan();
            TimeSpan zero = new TimeSpan(0, 0, 0);

            foreach (Employee l in logs)
            {
                if (l.IsError == 0)
                {
                    if (l.ChecksInOne != zero)
                        startTime = l.ChecksInOne;
                    {
                        if (l.ChecksOutOne != zero)
                            endTime = l.ChecksOutOne;
                        else
                            endTime = l.ChecksOutTwo;
                    }

                    if (l.ChecksInTwo != zero)
                    {
                        startTime2 = l.ChecksInTwo;
                        endTime2 = l.ChecksOutTwo;
                    }

                    TimeSpan? totalHour1 = endTime - startTime;
                    TimeSpan? totalHour2 = endTime2 - startTime2;

                    totalWorkingHours += totalHour1 + totalHour2;

                    endTime = new TimeSpan();
                    startTime = new TimeSpan();
                    endTime2 = new TimeSpan();
                    startTime2 = new TimeSpan();
                }
            }

            TotalWorkingHours = $"{totalWorkingHours.Value.TotalHours}";

        }
    }
}
