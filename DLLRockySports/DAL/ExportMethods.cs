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
            string oneRow = null;
            List<string> allRows = new List<string>();

            for (int i = 0; i < logs.Length; i++)
            {

                List<TimeSpan> checkings = new List<TimeSpan>();
                checkings[0] = (TimeSpan)logs[i].ChecksInOne;
                checkings[1] = (TimeSpan)logs[i].ChecksOutOne;
                checkings[2] = (TimeSpan)logs[i].ChecksInTwo;
                checkings[3] = (TimeSpan)logs[i].ChecksOutTwo;

                TimeSpan currentTime = new TimeSpan();
                TimeSpan zero = new TimeSpan(0, 0, 0);
                string inOUT = null;

                for (int j = 0; j < 4; j++)
                {
                    if (checkings[0] != zero)
                    {
                        currentTime = checkings[0];
                        if (i % 2 == 0)
                            inOUT = "IN";
                        else
                            inOUT = "OT";

                        oneRow =
                             $"01     {logs[i].FourDigitID} {logs[i].Date.Day}/{logs[i].Date.Month}/{logs[i].Date.Year} {currentTime.Hours}:{currentTime.Minutes} {inOUT} 0000000000 0000";
                       
                        allRows.Add(oneRow);
                    }

                    checkings.Remove(checkings[0]);
                }


            }

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(location, "logsOkez.txt")))
            {
                foreach (string line in allRows)
                    outputFile.WriteLine(line);
            }

        }
    }
}
