using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RockySportsView.ViewModel
{
    class ChooseClockVM
    {
        public void InsertClock (string ClockType, Window hide)
        {

            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["ClockType"].Value = ClockType;
                configuration.Save();

                MainWindow main = new MainWindow();
                main.Activate();
                hide.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("תקלה. אנא נסה שנית או צור קשר");
            }

        }
    }
}
