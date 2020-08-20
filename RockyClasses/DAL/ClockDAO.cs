using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses.DAL
{
    abstract class ClockDAO : IClockDAO
    {
        protected abstract void TakeData(string logLocation);
        protected void PutInDB () 
        {
        
        }
        
        public void StartOperation (string logLocation)
        {
            TakeData(logLocation);
            PutInDB();
        }
    }
}
