﻿using RockyClasses;
using RockyClasses.DAL;
using RockySportsView.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RockySportsView.ViewModel
{
    class MainWindowVM
    {
        private UserInterface Interface { get; set; }
        public MainWindowVM (Window hide)
        {
            if (ConfigurationManager.AppSettings["ClockType"] == "")
            {
                ChooseClock(hide);
            }
            else if (ConfigurationManager.AppSettings["ClockType"] == "One")
            {
                Interface = new UserInterface(ClocksEnum.ClockOne);
            }
            else if (ConfigurationManager.AppSettings["ClockType"] == "Two")
            {
                Interface = new UserInterface(ClocksEnum.ClockTwo);
            }
            else if (ConfigurationManager.AppSettings["ClockType"] == "Three")
            {
                Interface = new UserInterface(ClocksEnum.ClockThree);
            }
            //here check if the sql exists, if not > create !
            //else > check the type > make a user interface.
        }


        public void ChooseClock (Window hide)
        {
            ChooseClockView clock = new ChooseClockView();
            clock.Show();
            hide.Close();
        }
    }
}
