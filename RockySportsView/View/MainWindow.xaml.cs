using Microsoft.Win32;
using RockyClasses;
using RockySportsView.View;
using RockySportsView.View.ClockOneView;
using RockySportsView.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RockySportsView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM viewModel { get; set; }
        public MainWindow()
        {
            //clean DB
            viewModel = new MainWindowVM(this);
            this.DataContext = this.viewModel;

            InitializeComponent();

        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.Import();
            }

            catch (Exception ex)
            {
                MessageBox.Show("תקלה. אנא נסה שנית או צור קשר");
            }

        }

        private void ShowByEmpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsForEmpView logsView = new LogsForEmpView(viewModel.Interface.GetByEmpMonthYear(viewModel.SelectedMonth, viewModel.SelectedYear, viewModel.SelectedEmp), viewModel.Interface);
                logsView.Show();
                this.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show("תקלה. אנא נסה שנית או צור קשר");
            }
        }

        private void ShowAllWorkers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AllLogsView allView = new AllLogsView(viewModel.Interface.GetAllMonthAndYeah(viewModel.SelectedMonth, viewModel.SelectedYear), viewModel.Interface);
                allView.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("תקלה. אנא נסה שנית או צור קשר");
            }
        }
    }
}
