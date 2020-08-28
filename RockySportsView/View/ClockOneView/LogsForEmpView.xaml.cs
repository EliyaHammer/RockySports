using RockyClasses.POCO;
using RockySportsView.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for LogsForEmpView.xaml
    /// </summary>
    public partial class LogsForEmpView : Window
    {
        private LogsForEmpVM VM;
        public LogsForEmpView(LogsHolder empLogs)
        {
            InitializeComponent();
            VM = new LogsForEmpVM(empLogs);
            this.DataContext = VM;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
