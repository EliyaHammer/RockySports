using ExcelDataReader.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses.DAL
{
    abstract class ClockDAO : IClockDAO
    {
        //the all report
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
        public bool Update (Employee newLog)
        {
            using (Model1 entity = new Model1())
            {
                DateTime date = new DateTime(newLog.Date.Year, newLog.Date.Month, newLog.Date.Day);

                Employee found = null;

                for (int i = 0; i < entity.Employees.Count(); i++)
                {
                    if (entity.Employees.ToArray()[i].Date == date && entity.Employees.ToArray()[i].ID == newLog.ID)
                    {
                        found = entity.Employees.ToArray()[i];
                        break;
                    }
                }


                if (found != null)
                {
                    for (int i = 0; i < entity.Employees.Count(); i++)
                    {
                        if (entity.Employees.ToArray()[i] == found)
                        {
                            entity.Employees.ToArray()[i] = newLog;
                            entity.SaveChanges();
                            break;
                        }
                    }

                    return true;
                }
             /*   foreach (Employee e in entity.Employees)
                {
                    if (e.Date == date && e.ID == newLog.ID)
                    {
                        found = e;
                        break;
                    }
                }

                if (found != null)
                {
                    entity.Employees.ToList().ForEach((e) => { if (e == found) { e = newLog; } });
                    entity.SaveChanges();
                    return true;
                }
                */
                return false;

            }
        }
    }
}
