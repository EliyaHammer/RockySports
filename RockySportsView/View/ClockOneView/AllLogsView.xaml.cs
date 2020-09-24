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
            try
            {
                InitializeComponent();
                this.inter = inter;
                VM = new AllLogsVM(logs, inter);
                this.DataContext = VM;
                LogsView.ItemsSource = VM.Logs.ToList();
            }

            catch (Exception ex)
            {
                MessageBox.Show("תקלה. אנא נסה שנית או צור קשר");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("תקלה. אנא נסה שנית או צור קשר");
            }
        }

        private void EditRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (LogsView.SelectedItem == null)
                    {
                        MessageBox.Show("לא נבחרה שורה.");
                    }

                    else
                    {

                            AllLogsEditRowView edit = new AllLogsEditRowView((Employee)LogsView.SelectedItem, this, inter);
                            edit.ShowDialog();
                    }

            }


            catch (Exception ex)
            {
                MessageBox.Show("תקלה. אנא נסה שנית או צור קשר");
            }
        }

        private void ImportButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            Export ex = new Export(inter, VM.Logs.ToArray());
            ex.ShowDialog();
        }
    }
}
