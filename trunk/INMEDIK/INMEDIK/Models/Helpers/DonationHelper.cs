using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMEDIK.Models.Helpers
{
    public class DonationAux
    {
        public int id { get; set; }
        public decimal Amount { get; set; }
        public int OrderId { get; set; }
        public bool DoYouWantToDonate { get; set; }
        public DonationAux()
        {
        }
    }

    public class DonationResult : Result
    {
        public DonationAux data { get; set; }
        public List<DonationAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public DonationResult()
        {
            this.data = new DonationAux();
            this.data_list = new List<DonationAux>();
            this.total = new NumericResult();
        }
    }

    public class DonationHelper
    {
        public static DonationResult getDonation(int id)
        {
            DonationResult result = new DonationResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    //var cartDb = db.Cart.Where(c => c.EmployeeId == EmployeeId && c.ClinicId == ClinicId).FirstOrDefault();
                    //if(cartDb != null)
                    //{
                    //    //DataHelper.fill(result.data,cartDb);
                    //}
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
