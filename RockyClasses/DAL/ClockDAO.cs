using ExcelDataReader.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses.DAL
{
    abstract class ClockDAO : IClockDAO
    {
        protected abstract Employee[] TakeData(string logLocation);
        protected void PutInDB (Employee[] logs) 
        {
        using (Model1 entity = new Model1())
            {
                entity.Employees.AddRange(logs);
                entity.SaveChanges();
            }
        }
        public void StartOperation (string logLocation)
        {
            Employee[] logs = TakeData(logLocation);
            PutInDB(logs);
        }
    }
}
