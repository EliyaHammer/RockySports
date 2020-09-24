using RockyClasses.DAL;
using RockyClasses.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses.Facades
{
    class ClockFacade
    {
        private ClockDAO MyClock { get; set; }
        private GetDataFacade Data { get; set; }
        private ExportDataFacade ExportFacade { get; set; }

        public ClockFacade(ClocksEnum clock)
        {
            Data = new GetDataFacade();

            if (clock == ClocksEnum.ClockOne)
                MyClock = new ClockOneDAO();
            if (clock == ClocksEnum.ClockTwo)
                MyClock = new ClockTwoDAO();
            if (clock == ClocksEnum.ClockThree)
                MyClock = new ClockThreeDAO();
        }

        //need to make sure that the sql file as well saves only one year and not all the time 
        public void StartOperation(string logLocation)
        {
            MyClock.StartOperation(logLocation);
        }

        public void UpdateLog (Employee newLog)
        {
            MyClock.Update(newLog);
        }

        public LogsHolder GetLogsByEmpName(string name)
        {
            return Data.GetLogsByEmpName(name);
        }

        public Employee[] GetAllMonthAndYeah(int month, int year)
        {
            return Data.GetAllMonthAndYeah(month, year);
        }

        public LogsHolder GetByEmpMonthYear(int month, int year, string name)
        {
            return Data.GetByEmpMonthYear(month, year, name);
        }

        public bool Export (Employee[] logs, string location, FormatsEnum formatType)
        {
            ExportFacade = new ExportDataFacade(logs, location, formatType);
            return ExportFacade.Export();
        }

        public void CleanDB ()
        {
            Data.CleanDB();
        }

    }
}
