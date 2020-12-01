using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMEDIK.Models.Helpers
{
    public class StockAux
    {
        public int id { get; set; }
        public int conceptId { get; set; }
        public ConceptAux conceptAux { get; set; }
        public ClinicAux clinicAux { get; set; }
        public int inStock { get; set; }
        public decimal cost { get; set; }
        public bool iva { get; set; }
        public double currentIva { get; set; }
        public DateTime created { get; set; }
        public string batch { get; set; }
        public int almStockId { get; set; }
        public DateTime? expiredDate { get; set; }

        public StockAux()
        {
            this.conceptAux = new ConceptAux();
            this.clinicAux = new ClinicAux();
        }
    }
    public class StockResult : Result
    {
        public StockAux data { get; set; }
        public List<StockAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public StockResult()
        {
            this.data = new StockAux();
            this.data_list = new List<StockAux>();
            this.total = new NumericResult();
        }
    }

    public class vwStockAux
    {
        public int conceptId { get; set; }
        public string name { get; set; }
        public int inStock { get; set; }
        public int clinicId { get; set; }
        public bool? nurse { get; set; }
    }

    public class vwStockResult : Result
    {
        public vwStockAux data { get; set; }
        public List<vwStockAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public vwStockResult()
        {
            this.data = new vwStockAux();
            this.data_list = new List<vwStockAux>();
            this.total = new NumericResult();
        }
    }

    public class StockHelper
    {
        public static vwStockResult GetStocks(int clinicId, bool isNurse)
        {
            vwStockResult result = new vwStockResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = isNurse ? db.vwStock.Where(s => s.ClinicId == clinicId ).OrderBy(s => s.Name) : db.vwStock.Where(s => s.ClinicId == clinicId).OrderBy(s => s.Name);
                    foreach (vwStock stock in query.ToList())
                    {
                        vwStockAux aux = new vwStockAux();
                        DataHelper.fill(aux, stock);
                        result.data_list.Add(aux);
                    }
                    result.success = true;
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
