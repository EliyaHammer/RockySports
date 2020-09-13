using ExcelDataReader.Log;
using RockyClasses.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses.DAL
{
    class GetDataDAO
    {
        public LogsHolder GetLogsByEmpName (string name)
        {
            LogsHolder result = null;

            using (Model1 entity = new Model1())
            {
                List<Employee> logs = new List<Employee>();

                logs =
                    (from Employee in entity.Employees
                     where Employee.Name.ToLower() == name.ToLower()
                     orderby Employee.Date ascending
                     select Employee).ToList();

                foreach (Employee log in logs)
                {
                    log.CalculateIsError();
                    if (log.IsManuallyChanged == null)
                        log.IsManuallyChanged = 0;
                }

                result = new LogsHolder(logs.ToArray());
                result.CalculateAll();
            }

            return result;
        }
        public Employee[] GetLogsByMonthAndYear (int month, int year)
        {
            List<Employee> logs = new List<Employee>();

            using (Model1 entity = new Model1())
            {

                logs =
                    (from Employee in entity.Employees
                     where Employee.Date.Month == month && Employee.Date.Year == year
                     orderby Employee.Name
                     select Employee).ToList();

                foreach (Employee log in logs)
                {
                    log.CalculateIsError();
                    if (log.IsManuallyChanged == null)
                        log.IsManuallyChanged = 0;
                }
            }

            return logs.ToArray();
        }
        public LogsHolder GetLogsByMonthYearAndName (int month, int year, string name)
        {
            LogsHolder result = null;

            if (name != null)
            {
                Employee[] logs = null;
                List<Employee> newLogs = new List<Employee>();

                    logs = GetLogsByMonthAndYear(month, year);
                    for (int i = 0; i < logs.Count(); i ++)
                    {
                        if (logs[i].Name.ToLower() == name.ToLower())
                            newLogs.Add(logs[i]);
                    }


                foreach (Employee log in logs)
                {
                    log.CalculateIsError();
                    if (log.IsManuallyChanged == null)
                        log.IsManuallyChanged = 0;
                }
                result = new LogsHolder(newLogs.ToArray());
                    result.CalculateAll();
            }

            return result;
        }

    }
}
