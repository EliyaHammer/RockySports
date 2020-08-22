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
    /// Interaction logic for ChooseClockView.xaml
    /// </summary>
    public partial class ChooseClockView : Window
    {
        private ChooseClockVM VM { get; set; }
        public ChooseClockView()
        {
            VM = new ChooseClockVM();
            this.DataContext = VM;

            InitializeComponent();
        }
    }
}
