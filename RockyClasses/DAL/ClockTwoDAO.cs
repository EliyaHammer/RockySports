using ExcelDataReader;
using ExcelDataReader.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses.DAL
{
    class ClockTwoDAO : ClockDAO
    {
        public List<Employee> logs { get; private set; }

        protected override Employee[] TakeData(string logLocation)
        {
            int rowStart = 2;
            int column = 9;

            using (FileStream streamer = File.Open(@logLocation, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(streamer);
                DataSet spreadSheet = excelReader.AsDataSet();
                DataTable AttendaceLog = spreadSheet.Tables[2];
                int currentTable = 2;
                DataTable Summery = spreadSheet.Tables[0];
                logs = new List<Employee>();

                // need to run as number of blocks
                // loop for whole sheet
                for (int x = 0; x < 3; x++) // MUST check if its going down as well ! this is only for the side. 
                                            // if makes another log > check if there's another table after this one.
                {
                    Employee log = null;
                    column = 0;

                    string name = null;
                    if (AttendaceLog.Rows[rowStart][column] is DBNull || AttendaceLog.Rows[rowStart][column] is null)
                    {
                        break;
                    }
                    else
                        name = (string)AttendaceLog.Rows[rowStart][column];
                    log = new Employee();
                    log.Name = name;

                    rowStart++;
                    string idRaw = null;
                    if (!(AttendaceLog.Rows[rowStart][column] is DBNull))
                    {
                        idRaw = (string)AttendaceLog.Rows[rowStart][column];
                        log.ID = Convert.ToInt32(idRaw);
                    }
                    else
                        throw new NullReferenceException($"ID is null: row- {rowStart}, column- {column}");

                    column -= 8;
                    string yearAndMonth = null;
                    int year = 0;
                    int month = 0;

                    if (!(AttendaceLog.Rows[rowStart][column] is DBNull))
                    {
                        yearAndMonth = (string)AttendaceLog.Rows[rowStart][column];
                        year = Convert.ToInt32(yearAndMonth.Substring(0, 4));
                        month = Convert.ToInt32(yearAndMonth.Substring(5, 2));
                    }
                    else
                        throw new NullReferenceException($"Year and month is null: row- {rowStart}, column- {column}");

                    column -= 1;
                    int columnStartLog = column;
                    rowStart = 11;

                    // loop for one block
                    for (int i = 0; i < 32; i++)
                    {
                        //log

                        column = columnStartLog;

                        TimeSpan time = new TimeSpan();
                        TimeSpan[] checkingTimes = new TimeSpan[4];
                        int day = 0;
                        DateTime date = new DateTime();
                        string dayInWeekRaw = null;
                        int dayWeek = 0;

                        if (!(AttendaceLog.Rows[rowStart][column] is DBNull))
                        {
                            string allRaw = (string)AttendaceLog.Rows[rowStart][column];
                            if (allRaw != null && allRaw.Length > 1)
                            {
                                day = Convert.ToInt32(allRaw.Substring(0, 2));
                                dayInWeekRaw = allRaw.Substring(3, 2);

                                switch (dayInWeekRaw.ToUpper())
                                {
                                    case "SUNDAY":
                                        dayWeek = 1;
                                        break;
                                    case "MONDAY":
                                        dayWeek = 2;
                                        break;
                                    case "TUESDAY":
                                        dayWeek = 3;
                                        break;
                                    case "WEDNESDAY":
                                        dayWeek = 4;
                                        break;
                                    case "THURSDAY":
                                        dayWeek = 5;
                                        break;
                                    case "FRIDAY":
                                        dayWeek = 6;
                                        break;
                                    case "SATURDAY":
                                        dayWeek = 7;
                                        break;
                                }
                                date = new DateTime(year, month, day);
                            }
                        }

                        column++;

                        //checking times loop
                        for (int j = 0; j < 4; j++)
                        {
                            int hour = 0;
                            int minutes = 0;
                            int seconds = 0;

                            if (!(AttendaceLog.Rows[rowStart][column] is DBNull))
                            {
                                string timeRaw = (string)AttendaceLog.Rows[rowStart][column];
                                int.TryParse(timeRaw.Substring(0, 2), out hour);
                                int.TryParse(timeRaw.Substring(3, 2), out minutes);
                            }

                            time = new TimeSpan(hour, minutes, seconds);
                            checkingTimes[j] = time;

                            switch (j)
                            {
                                case 0:
                                    column += 2;
                                    break;
                                case 1:
                                    column += 3;
                                    break;
                                case 2:
                                    column += 2;
                                    break;
                            }

                        }

                        DateTime check = new DateTime();
                        if (date != check)
                        {
                            log.Date = date;
                            log.DayOfWeek = dayWeek;
                            log.ChecksInOne = checkingTimes[0];
                            log.ChecksOutOne = checkingTimes[1];
                            log.ChecksInTwo = checkingTimes[2];
                            log.ChecksOutTwo = checkingTimes[3];

                            TimeSpan zero = new TimeSpan(0, 0, 0);

                            if ((log.ChecksInOne == zero || log.ChecksInOne is null) && (log.ChecksInTwo == zero || log.ChecksInTwo is null))
                              log.IsAbsance = 1;

                            logs.Add(log);
                            Employee newLog = new Employee() { Name = log.Name, ID = log.ID };
                            log = newLog;
                        }
                        rowStart++;

                    }

                    rowStart = 2;
                    column += 16;

                    if (x == 2)
                    {
                        if (spreadSheet.Tables.Count >= currentTable)
                            x = 0;
                    }
                }
            }

            return logs.ToArray();
        }
    }
}
        