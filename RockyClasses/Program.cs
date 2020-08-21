using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            //NEED TO CREATE THE SQL FILE WITH OPENING !! 
            //NEED TO FIX THE HEBREW!!!
            //NEED TO MAKE ALL THE CHECKUPS THAT DAD SAID !!
            //NEED TO ALTER THE DB FOR THE INFORMATION MAAKAV !!
            //NEED TO MAKE THE LOGS !!!!!!!!!!
            //WHEN TAKING OUT FROM DB > MUST USE CALCULATE CLASS ! 
            //NEED TO NOT BE ABLE TO IMPORT TWICE!!!!!!!!!
            //check if hebrew is workign

            //clock two doesnt show total lates etc . 
            UserInterface clock = new UserInterface(DAL.ClocksEnum.ClockThree);

            Console.WriteLine(clock.Import(@"C:\Users\ELIYA\Downloads\254_report.xls"));
        }
    }
}
