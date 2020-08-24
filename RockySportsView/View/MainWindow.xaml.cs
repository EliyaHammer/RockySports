using Microsoft.Win32;
using RockyClasses;
using RockySportsView.View;
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

            viewModel = new MainWindowVM(this);
            this.DataContext = this.viewModel;

            InitializeComponent();

        }

        public void ChooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "(.xls)|*.xls";
            openFile.ShowDialog();
            FilePathText.Text = openFile.FileName;
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
                Task import = new Task(() => viewModel.Import());

                import.Start();


                import.Wait();
        }
    }
}
