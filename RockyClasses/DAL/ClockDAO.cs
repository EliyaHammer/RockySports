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
                foreach (Employee e in logs)
                {
                    Employee fourIDRaw =
                        (from log in entity.Employees
                        where log.ID == e.ID
                        select log) as Employee;

                    if (fourIDRaw != null)
                    {
                        e.FourDigitID = fourIDRaw.FourDigitID;

                        if (e.FourDigitID == 0)
                        {
                            int[] digits = new int[4];

                            for (int i  = 0; i < 4; i++)
                            {
                                Random r = new Random();
                                digits[i] = r.Next();

                                if (i == 3)
                                {
                                    foreach (Employee x in entity.Employees)
                                    {
                                        if (x.FourDigitID == Convert.ToInt32(digits))
                                        {
                                            i = 0;
                                            break;
                                        }
                                    }
                                }
                            }

                            e.FourDigitID = Convert.ToInt32(digits);
                        }
                    }

                }

                    entity.Employees.AddRange(logs);
                    entity.SaveChanges();
            }
        }
        public void StartOperation (string logLocation)
        {
            Employee[] logs = TakeData(logLocation);
            PutInDB(logs);
        }
        public bool Update (Employee oldLog, Employee newLog)
        {
            using (Model1 entity = new Model1())
            {
                Employee found = entity.Employees.Find(oldLog);

                if (found != null)
                {
                    found = newLog;
                    entity.SaveChanges();
                    return true;
                }

                return false;

            }
        }
    }
}
