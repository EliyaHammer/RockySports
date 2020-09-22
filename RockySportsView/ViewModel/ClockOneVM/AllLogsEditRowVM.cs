using RockyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockySportsView.ViewModel.ClockOneVM
{
    class AllLogsEditRowVM
    {
        public Employee[] SelectedRow { get; set; }
        public UserInterface inter { get; private set; }
        public AllLogsEditRowVM (Employee selectedRow, UserInterface inter)
        {
            this.inter = inter;
            this.SelectedRow = new Employee[1] { selectedRow };
        }
    }
}
