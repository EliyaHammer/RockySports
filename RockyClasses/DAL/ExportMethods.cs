using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses.DAL
{
    class ExportMethods
    {
        public void Okez (Employee[] logs, string location)
        {
            try
            {
                List<string> allRows = new List<string>();
                TimeSpan?[] checkingTimes;
                TimeSpan? zero = new TimeSpan(0, 0, 0);
                string newID = null;

                foreach (Employee e in logs)
                {
                    string day = e.Date.Day.ToString();
                    string month = e.Date.Month.ToString();

                    if (day.Length == 1)
                        day = "0" + day;
                    if (month.Length == 1)
                        month = "0" + month;


                    string hours = null;
                    string minutes = null;

                    checkingTimes = new TimeSpan?[4] { e.ChecksInOne, e.ChecksOutOne, e.ChecksInTwo, e.ChecksOutTwo };
                    if (e.ID.ToString().Length > 4)
                    {
                        newID = e.ID.ToString().Substring(0, 4);
                    }

                    else if (e.ID.ToString().Length < 4)
                    {
                        int j = 4 - e.ID.ToString().Length;
                        newID = e.ID.ToString();

                        for (int x = 0; x < j; x++)
                        {
                            newID += "0";
                        }
                    }

                    else
                        newID = e.ID.ToString();

                    for (int i = 0; i < 4; i++)
                    {
                        hours = checkingTimes[i].Value.Hours.ToString();
                        minutes = checkingTimes[i].Value.Minutes.ToString();

                        if (hours.Length == 1)
                            hours = "0" + hours;
                        if (minutes.Length == 1)
                            minutes = "0" + minutes;

                            if (i % 2 == 0) //in
                            {
                                allRows.Add($"01     {newID} {day}/{month}/{e.Date.Year.ToString().Substring(2, 2)}  {hours}:" +
                                    $"{minutes} IN 0000000000 0000");
                            }

                            else //out
                            {
                                allRows.Add($"01     {newID} {day}/{month}/{e.Date.Year.ToString().Substring(2, 2)}  {hours}:" +
                                    $"{minutes} OT 0000000000 0000");
                            }
                    }
                }

                using (FileStream file = new FileStream(Path.Combine(location, "logsOkez.txt"), FileMode.Create))
                {
                    using (TextWriter t = new StreamWriter(file))
                    {
                        foreach (string line in allRows)
                            t.WriteLine(line);
                    }
                }
            }

            catch (Exception ex)
            {
              
            }


        }

        public void Sinal (Employee[] logs, string location)
        {
            List<string> allRows = new List<string>();
            TimeSpan?[] checkingTimes;
            TimeSpan? zero = new TimeSpan(0, 0, 0);
            string newID = null;

            foreach (Employee e in logs)
            {

                string day = e.Date.Day.ToString();
                string month = e.Date.Month.ToString();

                if (day.Length == 1)
                    day = "0" + day;
                if (month.Length == 1)
                    month = "0" + month;


                string hours = null;
                string minutes = null;

                checkingTimes = new TimeSpan?[4] { e.ChecksInOne, e.ChecksOutOne, e.ChecksInTwo, e.ChecksOutTwo };
                if (e.ID.ToString().Length > 4)
                {
                    newID = e.ID.ToString().Substring(0, 4);
                }

                else if (e.ID.ToString().Length < 4)
                {
                    int j = 4 - e.ID.ToString().Length;
                    newID = e.ID.ToString();

                    for (int x = 0; x < j; x ++)
                    {
                        newID += "0";
                    }
                }

                else
                    newID = e.ID.ToString();

                string row = null;

                for (int i = 0; i < 4; i++)
                {
                    hours = checkingTimes[i].Value.Hours.ToString();
                    minutes = checkingTimes[i].Value.Minutes.ToString();

                    if (hours.Length == 1)
                        hours = "0" + hours;
                    if (minutes.Length == 1)
                        minutes = "0" + minutes;

                        if (i%2 == 0) //in
                        {
                            row = $"010.010.010.010 d0000000{day}{month}{e.Date.Year.ToString().Substring(2, 2)}A100000000000000000{newID}{hours}" +
                                $"{minutes}0000";
                            allRows.Add(row);
                        }

                        else //out
                        {
                            row = $"010.010.010.010 d0000000{day}{month}{e.Date.Year.ToString().Substring(2, 2)}B200000000000000000{newID}{hours}" +
                                    $"{minutes}0000";
                            allRows.Add(row);
                        }
                }
            }

            using (FileStream file = new FileStream(Path.Combine(location, "logsSinal.txt"), FileMode.Create))
            {
                using (TextWriter t = new StreamWriter(file))
                {
                    foreach (string line in allRows)
                        t.WriteLine(line);
                }
            }
        }
    }
}
