using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMEDIK.Models.Helpers
{
    public class StatusAux
    {
        public int id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public StatusAux()
        {
        }
    }
    public class StatusResult : Result
    {
        public StatusAux data { get; set; }
        public List<StatusAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public StatusResult()
        {
            this.data = new StatusAux();
            this.data_list = new List<StatusAux>();
            this.total = new NumericResult();
        }
    }

    class StatusHelper
    {
        public static StatusResult getStatus(int? id = null)
        {
            StatusResult result = new StatusResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (id != null)
                    {
                        var statusDb = db.Status.Where(s => s.id == id).FirstOrDefault();
                        DataHelper.fill(result.data, statusDb);
                        result.success = true;
                    }
                    else
                    {
                        var statusDbList = db.Status;
                        foreach (var statusDb in statusDbList)
                        {
                            StatusAux aux = new StatusAux();
                            DataHelper.fill(aux, statusDb);
                            result.data_list.Add(aux);
                        }
                        result.success = true;
                    }
                    
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }
    }
}
