using ExcelDataReader;
using ExcelDataReader.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses.DAL
{
    class ClockThreeDAO : ClockDAO
    {
        public List<Employee> logs { get; private set; }

        // the hebrew - RS-20
        protected override Employee[] TakeData(string logLocation)
        {
                using (FileStream streamer = File.Open(logLocation, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(streamer);
                    DataSet spreadSheet = excelReader.AsDataSet();
                    DataTable AttendanceLog = spreadSheet.Tables[3];
                    logs = new List<Employee>();
                    Employee log = null;

                    //big loop for whole log
                    int rowStart = 4;
                    int columnStart = 0;
                    for (int i = 0; i < AttendanceLog.Rows.Count - 4; i++)
                    {
                        columnStart = 0;

                        string idRaw = (string)AttendanceLog.Rows[rowStart][columnStart];
                        int id = Convert.ToInt32(idRaw);
                        columnStart++;
                        string name = (string)AttendanceLog.Rows[rowStart][columnStart];

                    //need to convert to hebrew only for the printing ! 

                    log = new Employee() { Name = name, ID = id };


                        columnStart += 2;
                        string dateRaw = (string)AttendanceLog.Rows[rowStart][columnStart];
                        int year = Convert.ToInt32(dateRaw.Substring(0, 4));
                        int month = Convert.ToInt32(dateRaw.Substring(5, 2));
                        int day = Convert.ToInt32(dateRaw.Substring(8, 2));
                        DateTime date = new DateTime(year, month, day);

                        columnStart++;
                        TimeSpan[] checkingTimes = new TimeSpan[4];
                        for (int j = 0; j < 4; j++)
                        {
                            int hour = 0;
                            int minutes = 0;
                            int seconds = 0;

                            string timeRaw = null;
                            if (AttendanceLog.Rows[rowStart][columnStart] is DBNull || AttendanceLog.Rows[rowStart][columnStart] is null)
                            {
                            }

                            else
                            {
                                timeRaw = (string)AttendanceLog.Rows[rowStart][columnStart];
                                hour = Convert.ToInt32(timeRaw.Substring(0, 2));
                                minutes = Convert.ToInt32(timeRaw.Substring(3, 2));
                            }

                            checkingTimes[j] = new TimeSpan(hour, minutes, seconds);
                            columnStart++;
                        }

                        double lateRaw = (double)AttendanceLog.Rows[rowStart][columnStart];
                        int late = Convert.ToInt32(lateRaw);

                        columnStart++;
                        double earlyRaw = (double)AttendanceLog.Rows[rowStart][columnStart];
                        int early = Convert.ToInt32(earlyRaw);

                    log.Date = date;
                    log.ChecksInOne = checkingTimes[0];
                    log.ChecksOutOne = checkingTimes[1];
                    log.ChecksInTwo = checkingTimes[2];
                    log.ChecksOutTwo = checkingTimes[3];
                    log.MinutesEarlyLeave = early;
                    log.MinutesLate = late;
                    TimeSpan zero = new TimeSpan(0, 0, 0);
                    if ((log.ChecksInOne is null || log.ChecksInOne == zero) && (log.ChecksInTwo == zero || log.ChecksInTwo is null))
                    {
                        log.IsAbsance = 1;
                    }

                    logs.Add(log);

                    rowStart++;
                    }
                }

            return logs.ToArray();
        }
        }
    }
