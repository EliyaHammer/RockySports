using RockyClasses.DAL;
using RockyClasses.Facades;
using RockyClasses.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses
{
    class UserInterface
    {
        private ClockFacade Clock { get; set; }

        public UserInterface(ClocksEnum clockType)
        {
            Clock = new ClockFacade(clockType);
        }

        public bool Import(string fileLocation)
        {
            if (fileLocation is null)
            {
                return false;
                //needs to add exceptions, needs to make the exceptions and alerts correct !!!!
            }

            try
            {
                Clock.StartOperation(fileLocation);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public void StartOperation(string logLocation)
        {
            Clock.StartOperation(logLocation);
        }

        public LogsHolder GetLogsByEmpName(string name)
        {
            return Clock.GetLogsByEmpName(name);
        }

        public Employee[] GetAllMonthAndYeah(int month, int year)
        {
            return  Clock.GetAllMonthAndYeah(month, year);
        }

        public LogsHolder GetByEmpMonthYear(int month, int year, string name)
        {
            return Clock.GetByEmpMonthYear(month, year, name);
        }
    }
}
