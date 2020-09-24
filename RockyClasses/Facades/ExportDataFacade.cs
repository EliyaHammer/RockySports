using RockyClasses.DAL;
using RockyClasses.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyClasses.Facades
{
    class ExportDataFacade
    {
        private Employee[] Logs { get; set; }
        private string Location { get; set; }
        private FormatsEnum type { get; set; }
        private ExportMethods export { get; set; }

        public ExportDataFacade (Employee[] logs, string location, FormatsEnum formatType)
        {
            this.Logs = logs;
            this.Location = location;
            this.type = formatType;
            export = new ExportMethods();
        }

        public bool Export ()
        {
            if (type == FormatsEnum.Okez)
            {
                try
                {
                    export.Okez(Logs, Location);
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            else if (type == FormatsEnum.Sinal)
            {
                try
                {
                    export.Sinal(Logs, Location);
                }

                catch (Exception ex)
                {
                    return false;
                }
            }

            //need to add all the rest methods

            return true;
        }
    }
}
