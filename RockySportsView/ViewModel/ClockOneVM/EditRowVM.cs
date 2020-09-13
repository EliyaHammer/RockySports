using RockyClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockySportsView.ViewModel.ClockOneVM
{
    class EditRowVM
    {
        public Employee[] row { get; set; }
        public UserInterface inter { get; private set; }
        public EditRowVM (Employee row, UserInterface inter)
        {
            this.row = new Employee[1] { row };
            this.inter = inter;
        }


    }
}
