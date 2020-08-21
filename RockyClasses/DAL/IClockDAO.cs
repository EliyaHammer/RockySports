using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses.DAL
{
    public interface IClockDAO
    {
        void TakeData(string logLocation);
        void PutInDB(Employee[] logs);
        void StartOperation(string clockLogLocation);
    }
}
