using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMEDIK.Models.Helpers
{
    public class OrderPromotionAux
    {
        public int id { get; set; }
        public int OrderId { get; set; }
        public int? ConceptId { get; set; }
        public string TextPromotion { get; set; }
        public decimal AmountSaved { get; set; }
        public int PromotionId { get; set; }
        public OrderPromotionAux()
        {
        }
    }

    public class OrderPromotionResult : Result
    {
        public OrderPromotionAux data { get; set; }
        public List<OrderPromotionAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public OrderPromotionResult()
        {
            this.data = new OrderPromotionAux();
            this.data_list = new List<OrderPromotionAux>();
            this.total = new NumericResult();
        }
    }

    public class OrderPromotionDiscountAppliedAux
    {
        public int id { get; set; }
        public int OrderId { get; set; }
        public int? ConceptId { get; set; }
        public int DiscountTypeId { get; set; }
        public string TextPromotion { get; set; }
        public decimal AmountSaved { get; set; }
        public OrderPromotionDiscountAppliedAux()
        {
        }
    }

    public class OrderPromotionDiscountAppliedResult : Result
    {
        public OrderPromotionDiscountAppliedAux data { get; set; }
        public List<OrderPromotionDiscountAppliedAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public OrderPromotionDiscountAppliedResult()
        {
            this.data = new OrderPromotionDiscountAppliedAux();
            this.data_list = new List<OrderPromotionDiscountAppliedAux>();
            this.total = new NumericResult();
        }
    }

    public class OrderPromotionHelper
    {
        public static OrderPromotionResult getDonation(int id)
        {
            OrderPromotionResult result = new OrderPromotionResult();
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
