using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Entity;
using System.Globalization;

namespace INMEDIK.Models.Helpers
{
    public class PaymentAux
    {
        public int id { get; set; }
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeAux employeeAux { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal Amount { get; set; }
        public decimal Commission { get; set; }
        public int clinicId { get; set; }
        public ClinicAux clinicAux { get; set; }
        public DateTime Created { get; set; }
        public string NumOperation { get; set; }
        public string sCreated

        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(Created.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"));
            }
        }
        public DateTime Updated { get; set; }
        public string sUpdated
        {
            get
            {
                return 
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(Updated.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"));
            }
        }
        public string PaymentTypeName { get; set; }
        public PaymentTypeAux PaymentTypeAux { get; set; }
        public PaymentAux()
        {
            PaymentTypeAux = new PaymentTypeAux();
            employeeAux = new EmployeeAux();
            clinicAux = new ClinicAux();
        }

        public void fillDB(ref Payment dbPayment)
        {
            dbPayment.OrderId = this.OrderId;
            dbPayment.PaymentTypeId = this.PaymentTypeId;
            dbPayment.EmployeeId = this.EmployeeId;
            dbPayment.Amount = this.Amount;
            dbPayment.Commission = this.Commission;
            dbPayment.Updated = DateTime.UtcNow;
            dbPayment.Created = DateTime.UtcNow;
            dbPayment.NumOperation = this.NumOperation;
        }
    }
    public class PaymentResult : Result
    {
        public PaymentAux data { get; set; }
        public List<PaymentAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public PaymentResult()
        {
            this.data = new PaymentAux();
            this.data_list = new List<PaymentAux>();
            this.total = new NumericResult();
        }
    }
    public class PaymentTypeAux
    {
        public int id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public PaymentTypeAux()
        {
        }
    }
    public class PaymentTypeResult : Result
    {
        public PaymentTypeAux data { get; set; }
        public List<PaymentTypeAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public PaymentTypeResult()
        {
            this.data = new PaymentTypeAux();
            this.data_list = new List<PaymentTypeAux>();
            this.total = new NumericResult();
        }
    }
    public class PaymentHelper
    {
        public static PaymentTypeResult GetPaymentTypes()
        {
            PaymentTypeResult result = new PaymentTypeResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.PaymentType.AsQueryable();
                    foreach (PaymentType paymenttype in query.ToList())
                    {
                        PaymentTypeAux aux = new PaymentTypeAux();
                        DataHelper.fill(aux, paymenttype);
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

        public static GenericResult AddPayments(List<PaymentAux> payments, OrderAux order, int employeeId)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {

                    foreach (PaymentAux payment in payments)
                    {
                        Payment paymentDB = new Payment();
                        payment.fillDB(ref paymentDB);
                        paymentDB.EmployeeId = employeeId;
                        db.Payment.Add(paymentDB);
                    }
                    Orders dbOrder = db.Orders.Where(o => o.id == order.id).FirstOrDefault();
                    if ((dbOrder.Payment.Sum(p=>p.Amount) + dbOrder.Payment.Sum(p=>p.Commission))
                        >= order.Total)
                    {
                        dbOrder.Paid = true;
                    }
                    dbOrder.Rounding = order.Rounding;
                    dbOrder.Total = order.Total;
                    dbOrder.Discount = order.Discount;
                    dbOrder.Donation = order.Donation;
                    dbOrder.Updated = DateTime.UtcNow;
                    dbOrder.Iva = order.Iva;
                    db.SaveChanges();
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado" + result.exception_message;
                }
            }
            return result;
        }
        public static GenericResult AddPayments(List<PaymentAux> payments, OrderAux order, int employeeId, int clinicId)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {

                    foreach (PaymentAux payment in payments)
                    {
                        Payment paymentDB = new Payment();
                        payment.fillDB(ref paymentDB);
                        paymentDB.EmployeeId = employeeId;
                        paymentDB.ClinicId = clinicId;
                        db.Payment.Add(paymentDB);
                    }
                    Orders dbOrder = db.Orders.Where(o => o.id == order.id).FirstOrDefault();
                    if ((dbOrder.Payment.Sum(p => p.Amount) + dbOrder.Payment.Sum(p => p.Commission))
                        >= order.Total)
                    {
                        dbOrder.Paid = true;
                    }
                    dbOrder.Rounding = order.Rounding;
                    dbOrder.Total = order.Total;
                    dbOrder.Discount = order.Discount;
                    dbOrder.Donation = order.Donation;
                    dbOrder.Updated = DateTime.UtcNow;
                    dbOrder.Iva = order.Iva;
                    db.SaveChanges();
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado" + result.exception_message;
                }
            }
            return result;
        }
    }
}