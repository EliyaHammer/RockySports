using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RockySportsView.View
{
    /// <summary>
    /// Interaction logic for PasswordView.xaml
    /// </summary>
    public partial class PasswordView : Window
    {
        public string Pass { get; private set; }
        public Window show { get; private set; }
        public PasswordView(string pass, Window show)
        {
            this.Pass = pass;
            this.show = show;
            InitializeComponent();
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordInput.Text == Pass)
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["succeeded"].Value = 1.ToString();
                configuration.Save();
                this.Close();
                show.Show();
            }

            else
            {
                MessageBox.Show("הקוד שהוקש שגוי.");
            }
        }
    }
}
