using RockyClasses;
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

namespace RockySportsView.View.ClockOneView
{
    /// <summary>
    /// Interaction logic for AllLogsView.xaml
    /// </summary>
    public partial class AllLogsView : Window
    {
        public AllLogsVM VM { get; private set; }
        public UserInterface inter { get; set; }
        public AllLogsView(Employee[] logs, UserInterface inter)
        {
            InitializeComponent();
            this.inter = inter;
            VM = new AllLogsVM(logs, inter);
            this.DataContext = VM;
            LogsView.ItemsSource = VM.Logs.ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void EditRow_Click(object sender, RoutedEventArgs e)
        {
          //  EditRowView edit = new EditRowView((Employee)LogsView.SelectedItem, this, inter);
           // edit.ShowDialog();
        }
    }
}
