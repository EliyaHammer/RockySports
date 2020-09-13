using RockyClasses;
using RockyClasses.POCO;
using RockySportsView.ViewModel;
using RockySportsView.ViewModel.ClockOneVM;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
    /// Interaction logic for EditRowView.xaml
    /// </summary>
    public partial class EditRowView : Window
    {
        private EditRowVM VM { get; set; }
        private LogsForEmpView windowBack { get; set; }
        public EditRowView(Employee row, LogsForEmpView windowBack, UserInterface inter)
        {
            this.windowBack = windowBack;

            InitializeComponent();
            VM = new EditRowVM(row, inter);
            this.DataContext = VM;
            RowView.ItemsSource = VM.row;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            updateRowChanges();
        }

        private void updateRowChanges ()
        {
            Employee oldRow = VM.row[0];
            Employee newRow = (Employee)RowView.Items[0];
            newRow.CalculateIsAbsence();
            newRow.CalculateIsError();
            newRow.IsManuallyChanged = 1;
            windowBack.VM.Logs.ToList().ForEach((e) => { if (e == oldRow) { e = newRow; } });

            VM.inter.UpdateLog(newRow);
            windowBack.VM.Emp.logs = (Employee[])windowBack.VM.Logs;
            LogsHolder newEmp = windowBack.VM.Emp;
            //database update not working
            newEmp.CalculateAll();
            windowBack.VM.Emp = newEmp;
            windowBack.LogsView.ItemsSource = windowBack.VM.Emp.logs;
            this.Close();
        }
    }
}
