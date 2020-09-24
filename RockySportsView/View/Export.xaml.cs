using Microsoft.Win32;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RockySportsView.View
{
    /// <summary>
    /// Interaction logic for Export.xaml
    /// </summary>
    public partial class Export : Window
    {
        private ExportVM vm { get; set; }

        private UserInterface Inter { get; set; }

        private Employee[] Logs { get; set; }

        public Export(UserInterface inter, Employee[] logs)
        {
            vm = new ExportVM();
            this.Inter = inter;
            this.DataContext = vm;
            this.Logs = logs;
            InitializeComponent();

            this.FormatBox.ItemsSource = vm.source;
        }

        private void ImportButton_Copy1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ImportButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (this.FormatBox.SelectedItem.ToString() == "סינאל")
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.ShowDialog();

                if (Inter.Export(Logs, dialog.SelectedPath, FormatsEnum.Sinal) == true)
                    System.Windows.MessageBox.Show("ייצוא בוצע בהצלחה");
                else
                    System.Windows.MessageBox.Show("ייצוא נכשל. אנא נסה שנית");
            }

            if (this.FormatBox.SelectedItem.ToString() == "עוקץ")
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.ShowDialog();

                if (Inter.Export(Logs, dialog.SelectedPath, FormatsEnum.Okez) == true)
                    System.Windows.MessageBox.Show("ייצוא בוצע בהצלחה");
                else
                    System.Windows.MessageBox.Show("ייצוא נכשל. אנא נסה שנית");
            }
        }
    }
}
