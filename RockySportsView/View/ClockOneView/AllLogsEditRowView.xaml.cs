using RockyClasses;
using RockySportsView.ViewModel.ClockOneVM;
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
    /// Interaction logic for AllLogsEditRowView.xaml
    /// </summary>
    public partial class AllLogsEditRowView : Window
    {
        private AllLogsEditRowVM VM { get; set; }
        private AllLogsView windowBack { get; set; }
        public AllLogsEditRowView(Employee selectedRow, AllLogsView windowBack, UserInterface inter)
        {
            try
            {
                this.windowBack = windowBack;

                InitializeComponent();
                VM = new AllLogsEditRowVM(selectedRow, inter);
                this.DataContext = VM;
                RowView.ItemsSource = VM.SelectedRow;
            }

            catch (Exception ex)
            {
                MessageBox.Show("תקלה. אנא נסה שנית או צור קשר");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                updateRowChanges();
                this.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("תקלה. אנא נסה שנית או צור קשר");
            }
        }

        private void updateRowChanges()
        {
            try
            {
                Employee oldRow = VM.SelectedRow[0];
                Employee newRow = (Employee)RowView.Items[0];
                newRow.CalculateIsAbsence();
                newRow.CalculateIsError();
                newRow.IsManuallyChanged = 1;
                windowBack.VM.Logs.ToList().ForEach((e) => { if (e == oldRow) { e = newRow; } });

                VM.inter.UpdateLog(newRow);
                // windowBack.VM.Logs = (Employee[])windowBack.VM.Logs;
                // newEmp.CalculateAll();
                //windowBack.VM.Emp = newEmp;
                windowBack.LogsView.ItemsSource = windowBack.VM.Logs;
            }

            catch (Exception ex)
            {
                MessageBox.Show("תקלה. אנא נסה שנית או צור קשר");
            }
        }
    }
}
