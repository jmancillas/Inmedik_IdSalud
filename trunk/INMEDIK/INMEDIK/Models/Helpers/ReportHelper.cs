using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{


    #region Aux
    public class ProductivityReportAux
    {
        public int orderId { get; set; }
        public int conceptId { get; set; }
        public int categoryId { get; set; }
        public int employeeId { get; set; }
        public string discountTypeId { get; set; }
        public string conceptName { get; set; }
        public DateTime dateCreated { get; set; }
        public decimal price { get; set; }
        public decimal cost { get; set; }
        public decimal profit { get; set; }
        public string categoryName { get; set; }
        public string roleName { get; set; }
        public string medicName { get; set; }
        public int quantity { get; set; }
        public bool xray { get; set; }
        public bool laboratory { get; set; }
    }
    public class CardPaymentReportAux
    {
        public string employeeName { get; set; }
        public int ticket { get; set; }
        public decimal amount { get; set; }
        public decimal commission { get; set; }
        public DateTime created { get; set; }
        public string NumOperation { get; set; }
        public string createdString
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(created.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
    }
    public class CashClosingReportAux
    {
        public int id { get; set; }
        public int? number { get; set; }
        public string employeeName { get; set; }
        public decimal efective { get; set; }
        public decimal creditCard { get; set; }
        public decimal vales { get; set; }
        public decimal totalFisic { get; set; }
        public decimal totalSale { get; set; }
        public DateTime date { get; set; }
        public string dateString
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(date.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public int soldItems { get; set; }
    }
    public class RoundingReportAux
    {
        public string employeeName { get; set; }
        public int orderId { get; set; }
        public decimal total { get; set; }
        public decimal rounding { get; set; }
        public int? Ticket { get; set; }
    }

    public class PaymentReportAux
    {
        public int paymentId { get; set; }
        public int paymentTypeId { get; set; }
        public string paymentTypeName { get; set; }
        public int employeeId { get; set; }
        public string employeeName { get; set; }
        public int orderId { get; set; }
        public int clinicId { get; set; }
        public string clinicName { get; set; }
        public decimal amount { get; set; }
        public decimal commission { get; set; }
        public decimal total { get; set; }
        public DateTime created { get; set; }
        public int? ticket { get; set; }
        public bool isCanceled { get; set; }
    }

    public class CountyServicesAux
    {
        public int countyId { get; set; }
        public int conceptId { get; set; }
        public string countyName { get; set; }
        public string conceptName { get; set; }
        public decimal price { get; set; }
        public decimal cost { get; set; }
        public decimal profit { get; set; }
    }

    public class PatientDiseaseAux
    {
        public string patientName { get; set; }
        public string patientLastName { get; set; }
        public DateTime patientBirthDate { get; set; }
        public string birthDateString
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(patientBirthDate.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy", new CultureInfo("es-MX"));
            }
        }
        public int patientAge { get; set; }
        public string patientSex { get; set; }
        public string countyName { get; set; }
        public string patientPhoneNumber { get; set; }
    }

    public class AgesGroup
    {
        public int mCount;
        public int fCount;
    }

    public class HealthReportAux
    {
        public string diseaseName { get; set; }
        public Dictionary<string, AgesGroup> agesCount { get; set; }
        public HealthReportAux()
        {
            this.agesCount = new Dictionary<string, AgesGroup>();
        }
    }

    public class DonationsReportAux
    {
        public int orderId { get; set; }
        public int ticket { get; set; }
        public int patientId { get; set; }
        public int employeeId { get; set; }
        public decimal donation { get; set; }
        public string patientName { get; set; }
        public string employeeName { get; set; }
        public int clinicId { get; set; }
        public string clinicName { get; set; }
        public DateTime created { get; set; }
        public string createdString
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(this.created.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"));
            }
        }
    }

    public class GetWaitingTimesReportAux
    {
        public int Id { get; set; }
        public int ConsultId { get; set; }
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public DateTime Created { get; set; }
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public string StartProcess { get; set; }
        public string StartStageReception { get; set; }
        public string EndStageReception { get; set; }
        public int TAPReception { get; set; }
        public int TEPRecNur { get; set; }
        public string StartStageNursery { get; set; }
        public string EndStageNursery { get; set; }
        public int TAPNursery { get; set; }
        public int TEPNurMed { get; set; }
        public string StartStageMedic { get; set; }
        public string EndStageMedic { get; set; }
        public int TAPMedic { get; set; }
        public string EndProcess { get; set; }
        public int TotalMinutes { get; set; }
        //public string LabelsArray { get; set; }
        //public string DataArray { get; set; }
        //public GetWaitingTimesReportAux()
        //{
        //    Array LabelsArray{ get; set;}
        //}
    }

    public class GetWaitingTimesGraphDataAux
    {
        public int RecData { get; set; }
        public int RecNurData { get; set; }
        public int NurData { get; set; }
        public int NurMedData { get; set; }
        public int MedData { get; set; }
    }

    public class GetWaitingTimesGraphLabelsAux
    {
        public string Labels { get; set; }
    }

    public class GetWaitingTimesGraphAux
    {
        public List<GetWaitingTimesGraphLabelsAux> LabelsArray { get; set; }
        public List<GetWaitingTimesGraphDataAux> DataArray { get; set; }
        public GetWaitingTimesGraphAux()
        {
            LabelsArray = new List<GetWaitingTimesGraphLabelsAux>();
            DataArray = new List<GetWaitingTimesGraphDataAux>();
        }
    }
    #endregion

    #region Results
    public class HealthReportResult : Result
    {
        public List<HealthReportAux> data_list { get; set; }

        public HealthReportResult()
        {
            this.data_list = new List<HealthReportAux>();
        }
    }
    public class ProductivityReportResult : Result
    {
        public List<ProductivityReportAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public ProductivityReportResult()
        {
            this.data_list = new List<ProductivityReportAux>();
            this.total = new NumericResult();
        }
    }
    public class RoundingReportResult : Result
    {
        public List<RoundingReportAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public RoundingReportResult()
        {
            this.data_list = new List<RoundingReportAux>();
            this.total = new NumericResult();
        }
    }

    public class CardPaymentReportResult : Result
    {
        public List<CardPaymentReportAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public CardPaymentReportResult()
        {
            this.data_list = new List<CardPaymentReportAux>();
            this.total = new NumericResult();
        }
    }

    public class CashClosingReportResult : Result
    {
        public List<CashClosingReportAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public CashClosingReportResult()
        {
            this.data_list = new List<CashClosingReportAux>();
            this.total = new NumericResult();
        }
    }

    public class CountyServicesResult : Result
    {
        public List<CountyServicesAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public CountyServicesResult()
        {
            this.data_list = new List<CountyServicesAux>();
            this.total = new NumericResult();
        }
    }
    public class PatientDiseaseResult : Result
    {
        public List<PatientDiseaseAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public PatientDiseaseResult()
        {
            this.data_list = new List<PatientDiseaseAux>();
            this.total = new NumericResult();
        }
    }

    public class PaymentReportResult : Result
    {
        public List<PaymentReportAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public PaymentReportResult()
        {
            this.data_list = new List<PaymentReportAux>();
            this.total = new NumericResult();
        }
    }

    public class DonationsReportResult : Result
    {
        public List<DonationsReportAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public DonationsReportResult()
        {
            this.data_list = new List<DonationsReportAux>();
            this.total = new NumericResult();
        }
    }

    public class GetWaitingTimesReportResult : Result
    {
        public List<GetWaitingTimesReportAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public GetWaitingTimesReportResult()
        {
            this.data_list = new List<GetWaitingTimesReportAux>();
            this.total = new NumericResult();
        }
    }

    public class GetWaitingTimesGraphResult : Result
    {
        public List<GetWaitingTimesGraphAux> data_list { get; set; }
        public GetWaitingTimesGraphAux data { get; set; }
        public NumericResult total { get; set; }
        public GetWaitingTimesGraphResult()
        {
            this.data = new GetWaitingTimesGraphAux();
            this.data_list = new List<GetWaitingTimesGraphAux>();
            this.total = new NumericResult();
        }
    }
    #endregion

    public class ReportFilter
    {
        public int? medicId { get; set; }
        public int? categoryId { get; set; }
        public string discountTypeId { get; set; }
        public int? discountTypeId_number { get; set; }
        public string dateStart { get; set; }
        public DateTime? dateStart_ { get { return string.IsNullOrEmpty(dateStart) ? (DateTime?)null : Convert.ToDateTime(dateStart).ToUniversalTime(); } }
        public string dateEnd { get; set; }
        public DateTime? dateEnd_ { get { return string.IsNullOrEmpty(dateEnd) ? (DateTime?)null : Convert.ToDateTime(dateEnd).ToUniversalTime(); } }
        public List<ClinicAux> clinics { get; set; }
        public ClinicAux clinic { get; set; }
        public string concept { get; set; }
        public string categoryName { get; set; }
        public string chronicDiseaseCode { get; set; }
        public bool xray { get; set; }
        public bool laboratory { get; set; }
        public int clinicId { get; set; }
    }

    public class ReportHelper
    {
        public static ProductivityReportResult GetProductivityReport(DTParameterModel filter, ReportFilter extraFilter)
        {
            ProductivityReportResult result = new ProductivityReportResult();
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            if (!extraFilter.dateStart_.HasValue || extraFilter.clinics == null || extraFilter.clinics.Count == 0)
            {
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                db.Database.CommandTimeout = 180;
                try
                {

                    var productivityReports = db.vwProductivityMedic.ToList();
                    var query = db.vwProductivityMedic.AsQueryable();
                    //var query = db.vwMedicProductivity.AsQueryable();
                    //Aplicar filtros
                    #region filtros
                    if (extraFilter.medicId.HasValue)
                    {
                        query = query.Where(g => g.EmployeeId == extraFilter.medicId.Value);
                    }
                    if (extraFilter.dateStart_.HasValue)
                    {
                        var date = extraFilter.dateStart_.Value;
                        query = query.Where(g => g.DateCreated >= date);
                    }
                    if (extraFilter.dateEnd_.HasValue)
                    {
                        var date = extraFilter.dateEnd_.Value;
                        query = query.Where(g => g.DateCreated <= date);
                    }
                    if (extraFilter.clinics != null)
                    {
                        List<int?> clinicList = extraFilter.clinics.Select(c => c.id).ToList();
                        query = query.Where(g => clinicList.Contains(g.ClinicId));
                    }
                    #endregion
                    //Agrupar y sumatorioa
                    var grouped = from q in query
                                  group q by new {q.CategoryName, q.ConceptName, q.MedicName, q.Quantity } into g
                                  select new ProductivityReportAux()
                                  {
                                      quantity = g.FirstOrDefault().Quantity,
                                      categoryName = g.FirstOrDefault().CategoryName,
                                      conceptName = g.FirstOrDefault().ConceptName,
                                      medicName = g.FirstOrDefault().MedicName,
                                  };

                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "medicName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.medicName);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.medicName);
                            }
                        }
                        if (orderColumn == "conceptName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.conceptName);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.conceptName);
                            }
                        }
                        if (orderColumn == "categoryName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.categoryName);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.categoryName);
                            }
                        }
                        if (orderColumn == "quantity")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.quantity);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.quantity);
                            }
                        }
                        //if (orderColumn == "price")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        grouped = grouped.OrderBy(g => g.price);
                        //    }
                        //    else
                        //    {
                        //        grouped = grouped.OrderByDescending(g => g.price);
                        //    }
                        //}
                        //if (orderColumn == "cost")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        grouped = grouped.OrderBy(g => g.cost);
                        //    }
                        //    else
                        //    {
                        //        grouped = grouped.OrderByDescending(g => g.cost);
                        //    }
                        //}
                        //if (orderColumn == "profit")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        grouped = grouped.OrderBy(g => g.profit);
                        //    }
                        //    else
                        //    {
                        //        grouped = grouped.OrderByDescending(g => g.profit);
                        //    }
                        //}
                    }
                    #endregion
                    result.total.value = grouped.Count();
                    if (filter.Length != -1)
                    {
                        grouped = grouped.Skip(filter.Start).Take(filter.Length);
                    }
                    result.data_list = grouped.ToList();
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

        public static ProductivityReportResult GetPharmacyProductivityReport(DTParameterModel filter, ReportFilter extraFilter)
        {
            ProductivityReportResult result = new ProductivityReportResult();
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            if (!extraFilter.dateStart_.HasValue || extraFilter.clinics == null || extraFilter.clinics.Count == 0)
            {
                return result;
            }
            if (extraFilter.categoryId != 1)
            {
                extraFilter.discountTypeId_number = null;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                db.Database.CommandTimeout = 180;
                try
                {
                    var query = db.vwPharmacyProductivity.AsQueryable();
                    //Aplicar filtros
                    #region filtros
                    if (extraFilter.medicId.HasValue)
                    {
                        query = query.Where(g => g.EmployeeId == extraFilter.medicId.Value);
                    }
                    ////////////////////////////////////////////////////////////////////////////
                    if (extraFilter.categoryId.HasValue)
                    {
                        query = query.Where(g => g.CategoryId == extraFilter.categoryId.Value);
                    }
                    ////////////////////////////////////////////////////////////////////////////
                    if (extraFilter.discountTypeId_number.HasValue)
                    {
                        query = query.Where(g => g.discounttypeId == extraFilter.discountTypeId_number.Value);
                    }
                    ////////////////////////////////////////////////////////////////////////////
                    if (extraFilter.dateStart_.HasValue)
                    {
                        var date = extraFilter.dateStart_.Value;
                        query = query.Where(g => g.DateCreated >= date);
                    }
                    if (extraFilter.dateEnd_.HasValue)
                    {
                        var date = extraFilter.dateEnd_.Value;
                        query = query.Where(g => g.DateCreated <= date);
                    }
                    if (extraFilter.clinics != null)
                    {
                        List<int?> clinicList = extraFilter.clinics.Select(c => c.id).ToList();
                        query = query.Where(g => clinicList.Contains(g.ClinicId));
                    }
                    #endregion
                    //Agrupar y sumatorioa
                    var grouped = from q in query
                                  group q by new { q.CategoryId, q.CategoryName, q.ConceptId, q.ConceptName, q.EmployeeId, q.MedicName, q.RoleName } into g
                                  select new ProductivityReportAux()
                                  {
                                      categoryId = g.FirstOrDefault().CategoryId,
                                      categoryName = g.FirstOrDefault().CategoryName,
                                      conceptId = g.FirstOrDefault().ConceptId,
                                      conceptName = g.FirstOrDefault().ConceptName,
                                      employeeId = g.FirstOrDefault().EmployeeId,
                                      medicName = g.FirstOrDefault().MedicName,
                                      roleName = g.FirstOrDefault().RoleName,
                                      cost = g.Sum(c => c.Cost * c.Quantity),
                                      price = g.Sum(c => (c.Price + (c.Iva.HasValue ? c.Iva.Value : 0) - c.Discount) * c.Quantity),
                                      profit = g.Sum(c => c.Profit),
                                      quantity = g.Sum(c => c.Quantity)
                                  };
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "medicName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.medicName);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.medicName);
                            }
                        }
                        if (orderColumn == "conceptName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.conceptName);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.conceptName);
                            }
                        }
                        if (orderColumn == "categoryName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.categoryName);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.categoryName);
                            }
                        }
                        if (orderColumn == "quantity")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.quantity);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.quantity);
                            }
                        }
                        if (orderColumn == "price")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.price);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.price);
                            }
                        }
                        if (orderColumn == "cost")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.cost);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.cost);
                            }
                        }
                        if (orderColumn == "profit")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.profit);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.profit);
                            }
                        }
                    }
                    #endregion
                    result.total.value = grouped.Count();
                    if (filter.Length != -1)
                        grouped = grouped.Skip(filter.Start).Take(filter.Length);
                    result.data_list = grouped.ToList();
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

        public static PaymentReportResult GetPaymentReport(DTParameterModel filter, ReportFilter extraFilter)
        {
            PaymentReportResult result = new PaymentReportResult();
            string order = "";
            string orderColumn = "";

            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            if (!extraFilter.dateStart_.HasValue || extraFilter.clinics == null || extraFilter.clinics.Count == 0)
            {
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                db.Database.CommandTimeout = 300;
                try
                {
                    var query = db.vwPaymentReport.Where(p => !p.IsCanceled);

                    //Aplicar filtros
                    #region filtros
                    if (extraFilter.dateStart_.HasValue)
                    {
                        var date = extraFilter.dateStart_.Value;
                        query = query.Where(g => g.Created >= date);
                    }
                    if (extraFilter.dateEnd_.HasValue)
                    {
                        var date = extraFilter.dateEnd_.Value;
                        query = query.Where(g => g.Created <= date);
                    }
                    if (extraFilter.clinics != null)
                    {
                        List<int?> clinicList = extraFilter.clinics.Select(c => c.id).ToList();
                        query = query.Where(g => clinicList.Contains(g.clinicId));
                    }
                    #endregion

                    //Agrupar y sumatoria
                    #region group and sum
                    var grouped = from q in query
                                  group q by new { q.orderId, q.employeeId, q.clinicId, q.paymentTypeId, } into g
                                  select new PaymentReportAux()
                                  {
                                      orderId = g.FirstOrDefault().orderId,
                                      employeeId = g.FirstOrDefault().employeeId,
                                      employeeName = g.FirstOrDefault().employeeName,
                                      clinicId = g.FirstOrDefault().clinicId,
                                      clinicName = g.FirstOrDefault().clinicName,
                                      paymentTypeId = g.FirstOrDefault().paymentTypeId,
                                      paymentTypeName = g.FirstOrDefault().paymentTypeName,
                                      ticket = g.FirstOrDefault().Ticket,
                                      amount = g.Sum(c => c.Amount),
                                      commission = g.Sum(c => c.Commission),
                                      total = g.Sum(c => c.total.HasValue ? c.total.Value : 0)
                                  };
                    #endregion

                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "employeeName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.employeeName);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.employeeName);
                            }
                        }
                        if (orderColumn == "clinicName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.clinicName);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.clinicName);
                            }
                        }
                        if (orderColumn == "paymentTypeName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.paymentTypeName);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.paymentTypeName);
                            }
                        }
                        if (orderColumn == "ticket")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.ticket);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.ticket);
                            }
                        }
                        if (orderColumn == "amount")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.amount);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.amount);
                            }
                        }
                        if (orderColumn == "commission")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.commission);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.commission);
                            }
                        }
                        if (orderColumn == "total")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.total);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.total);
                            }
                        }
                    }
                    #endregion

                    result.total.value = grouped.Count();
                    if (filter.Length != -1)
                        grouped = grouped.Skip(filter.Start).Take(filter.Length);
                    result.data_list = grouped.ToList();
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

        public static ProductivityReportResult GetConceptProductivityReport(DTParameterModel filter, ReportFilter extraFilter)
        {
            ProductivityReportResult result = new ProductivityReportResult();
            string order = "";
            string orderColumn = "";

            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            if (!extraFilter.dateStart_.HasValue || extraFilter.clinics == null || extraFilter.clinics.Count == 0)
            {
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                db.Database.CommandTimeout = 300;
                try
                {
                    var query = db.vwProductivityConcept.Where(c => !c.IsCanceled);

                    //Aplicar filtros
                    #region filtros
                    if (extraFilter.dateStart_.HasValue)
                    {
                        var date = extraFilter.dateStart_.Value;
                        query = query.Where(g => g.DateCreated >= date);
                    }
                    if (extraFilter.dateEnd_.HasValue)
                    {
                        var date = extraFilter.dateEnd_.Value;
                        query = query.Where(g => g.DateCreated <= date);
                    }
                    if (!string.IsNullOrEmpty(extraFilter.concept))
                    {
                        query = query.Where(g => g.ConceptName == extraFilter.concept);
                    }
                    if (extraFilter.clinics != null)
                    {
                        List<int?> clinicList = extraFilter.clinics.Select(c => c.id).ToList();
                        query = query.Where(g => clinicList.Contains(g.ClinicId));
                    }
                    //FILTRO DE CADA TABLA por nombre de categoría
                    if (!string.IsNullOrEmpty(extraFilter.categoryName))
                    {
                        query = query.Where(q => q.CategoryName == extraFilter.categoryName);
                    }
                    // filtro solo si la categoria es examenes
                    //if (extraFilter.categoryName == "Exámenes")
                    //{
                    //    if (extraFilter.laboratory == true && extraFilter.xray == false)
                    //    {
                    //        query = query.Where(q => q.Laboratory == true);
                    //    }
                    //    if (extraFilter.laboratory == false && extraFilter.xray == true)
                    //    {
                    //        query = query.Where(q => q.XRay == true);
                    //    }
                    //    if (extraFilter.laboratory == false && extraFilter.xray == false)
                    //    {
                    //        query = query.Where(q => q.XRay == true && q.Laboratory == true);
                    //    }
                    //}
                    #endregion

                    //Agrupar y sumatoria
                    #region group and sum
                    List<vwProductivityConcept> concepts = query.ToList();
                    var grouped = from q in concepts
                                  group q by new { q.CategoryName, q.ConceptName } into g
                                  select new ProductivityReportAux()
                                  {
                                      categoryName = g.FirstOrDefault().CategoryName,
                                      conceptName = g.FirstOrDefault().ConceptName,
                                      quantity = g.Sum(c => c.Quantity)
                                  };
                    #endregion

                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "conceptName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.conceptName);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.conceptName);
                            }
                        }
                        if (orderColumn == "quantity")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                grouped = grouped.OrderBy(g => g.quantity);
                            }
                            else
                            {
                                grouped = grouped.OrderByDescending(g => g.quantity);
                            }
                        }
                        //if (orderColumn == "price")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        grouped = grouped.OrderBy(g => g.price);
                        //    }
                        //    else
                        //    {
                        //        grouped = grouped.OrderByDescending(g => g.price);
                        //    }
                        //}
                        //if (orderColumn == "cost")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        grouped = grouped.OrderBy(g => g.cost);
                        //    }
                        //    else
                        //    {
                        //        grouped = grouped.OrderByDescending(g => g.cost);
                        //    }
                        ////}
                        //if (orderColumn == "profit")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        grouped = grouped.OrderBy(g => g.profit);
                        //    }
                        //    else
                        //    {
                        //        grouped = grouped.OrderByDescending(g => g.profit);
                        //    }
                        //}
                    }
                    #endregion

                    result.total.value = grouped.Count();
                    if (filter.Length != -1)
                        grouped = grouped.Skip(filter.Start).Take(filter.Length);
                    result.data_list = grouped.ToList();
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

        public static RoundingReportResult GetRoundingReport(DTParameterModel filter, ReportFilter extraFilter)
        {
            RoundingReportResult result = new RoundingReportResult();
            if (!extraFilter.dateStart_.HasValue || extraFilter.clinics == null || extraFilter.clinics.Count == 0)
            {
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                db.Database.CommandTimeout = 180;
                string order = "";
                string orderColumn = "";
                if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
                {
                    order = filter.Order.First().Dir;
                    orderColumn = filter.Order.First().Data;
                }

                try
                {
                    var query = db.vwRoundings.AsQueryable();
                    //Aplicar filtros
                    #region filtros
                    if (extraFilter.dateStart_.HasValue)
                    {
                        var date = extraFilter.dateStart_.Value;
                        query = query.Where(g => g.Created >= date);
                    }
                    if (extraFilter.dateEnd_.HasValue)
                    {
                        var date = extraFilter.dateEnd_.Value;
                        query = query.Where(g => g.Created <= date);
                    }
                    if (extraFilter.medicId.HasValue)
                    {
                        query = query.Where(g => g.EmployeeId == extraFilter.medicId);
                    }
                    if (extraFilter.clinics != null)
                    {
                        List<int?> clinicList = extraFilter.clinics.Select(c => c.id).ToList();
                        query = query.Where(g => clinicList.Contains(g.ClinicId));
                    }
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "employeeName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(g => g.EmployeeName);
                            }
                            else
                            {
                                query = query.OrderByDescending(g => g.EmployeeName);
                            }
                        }
                        if (orderColumn == "Ticket")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(g => g.Ticket);
                            }
                            else
                            {
                                query = query.OrderByDescending(g => g.Ticket);
                            }
                        }
                        if (orderColumn == "total")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(g => g.Total);
                            }
                            else
                            {
                                query = query.OrderByDescending(g => g.Total);
                            }
                        }
                        if (orderColumn == "rounding")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(g => g.Rounding);
                            }
                            else
                            {
                                query = query.OrderByDescending(g => g.Rounding);
                            }
                        }
                    }
                    #endregion
                    result.total.value = query.Count();
                    if (filter.Length != -1)
                        query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (var registro in query.ToList())
                    {
                        RoundingReportAux aux = new RoundingReportAux();
                        DataHelper.fill(aux, registro);
                        result.data_list.Add(aux);
                    }
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

        public static CardPaymentReportResult GetCardPaymentReport(DTParameterModel filter, ReportFilter extraFilter)
        {
            CardPaymentReportResult result = new CardPaymentReportResult();
            if (!extraFilter.dateStart_.HasValue || extraFilter.clinics == null || extraFilter.clinics.Count == 0)
            {
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                db.Database.CommandTimeout = 180;
                string order = "";
                string orderColumn = "";
                if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
                {
                    order = filter.Order.First().Dir;
                    orderColumn = filter.Order.First().Data;
                }

                try
                {
                    var query = db.vwCardPayments.AsQueryable();
                    //Aplicar filtros
                    #region filtros
                    if (extraFilter.dateStart_.HasValue)
                    {
                        var date = extraFilter.dateStart_.Value;
                        query = query.Where(g => g.Created >= date);
                    }
                    if (extraFilter.dateEnd_.HasValue)
                    {
                        var date = extraFilter.dateEnd_.Value;
                        query = query.Where(g => g.Created <= date);
                    }
                    if (extraFilter.medicId.HasValue)
                    {
                        query = query.Where(g => g.EmployeeId == extraFilter.medicId);
                    }
                    if (extraFilter.clinics != null)
                    {
                        List<int?> clinicList = extraFilter.clinics.Select(c => c.id).ToList();
                        query = query.Where(g => clinicList.Contains(g.ClinicId));
                    }
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "employeeName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.EmployeeName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.EmployeeName);
                            }
                        }
                        if (orderColumn == "ticket")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Ticket);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Ticket);
                            }
                        }
                        if (orderColumn == "amount")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Amount);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Amount);
                            }
                        }
                        if (orderColumn == "commission")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Commission);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Commission);
                            }
                        }
                        if (orderColumn == "NumOperation")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.NumOperation);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.NumOperation);
                            }
                        }
                    }
                    #endregion
                    result.total.value = query.Count();
                    if (filter.Length != -1)
                        query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (var registro in query.ToList())
                    {
                        CardPaymentReportAux aux = new CardPaymentReportAux();
                        DataHelper.fill(aux, registro);
                        result.data_list.Add(aux);
                    }
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

        public static CountyServicesResult GetCountyServicesReport(DTParameterModel filter, ReportFilter extraFilter)
        {
            CountyServicesResult result = new CountyServicesResult();
            if (!extraFilter.dateStart_.HasValue || extraFilter.clinics == null || extraFilter.clinics.Count == 0)
            {
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                db.Database.CommandTimeout = 180;
                string order = "";
                string orderColumn = "";
                if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
                {
                    order = filter.Order.First().Dir;
                    orderColumn = filter.Order.First().Data;
                }

                try
                {
                    var query = db.vwDepartamentProductivity.AsQueryable();
                    //Aplicar filtros
                    #region filtros
                    if (extraFilter.dateStart_.HasValue)
                    {
                        var date = extraFilter.dateStart_.Value;
                        query = query.Where(g => g.Created >= date);
                    }
                    if (extraFilter.dateEnd_.HasValue)
                    {
                        var date = extraFilter.dateEnd_.Value;
                        query = query.Where(g => g.Created <= date);
                    }
                    if (!string.IsNullOrEmpty(extraFilter.concept))
                    {
                        query = query.Where(g => g.ConceptName == extraFilter.concept);
                    }
                    if (extraFilter.clinics != null)
                    {
                        List<int?> clinicList = extraFilter.clinics.Select(c => c.id).ToList();
                        query = query.Where(g => clinicList.Contains(g.ClinicId));
                    }
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "countyName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.CountyName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.CountyName);
                            }
                        }
                        if (orderColumn == "conceptName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.ConceptName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.ConceptName);
                            }
                        }
                        //if (orderColumn == "price")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        query = query.OrderBy(q => q.Price);
                        //    }
                        //    else
                        //    {
                        //        query = query.OrderByDescending(q => q.Price);
                        //    }
                        //}
                        //if (orderColumn == "cost")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        query = query.OrderBy(q => q.Cost);
                        //    }
                        //    else
                        //    {
                        //        query = query.OrderByDescending(q => q.Cost);
                        //    }
                        //}
                        //if (orderColumn == "profit")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        query = query.OrderBy(q => q.Profit);
                        //    }
                        //    else
                        //    {
                        //        query = query.OrderByDescending(q => q.Profit);
                        //    }
                        //}
                    }
                    #endregion
                    result.total.value = query.Count();
                    if (filter.Length != -1)
                        query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (var registro in query.ToList())
                    {
                        CountyServicesAux aux = new CountyServicesAux();
                        DataHelper.fill(aux, registro);
                        result.data_list.Add(aux);
                    }
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

        public static PatientDiseaseResult GetPatientsReport(DTParameterModel filter, ReportFilter extraFilter)
        {
            PatientDiseaseResult result = new PatientDiseaseResult();
            if (extraFilter.clinics == null || extraFilter.clinics.Count == 0)
            {
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                db.Database.CommandTimeout = 180;
                string order = "";
                string orderColumn = "";
                if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
                {
                    order = filter.Order.First().Dir;
                    orderColumn = filter.Order.First().Data;
                }

                try
                {
                    var query = db.vwPatientChronicDisease.AsQueryable();
                    //Aplicar filtros
                    #region filtros
                    if (!string.IsNullOrEmpty(extraFilter.chronicDiseaseCode))
                    {
                        query = query.Where(g => g.ChronicDiseaseCode == extraFilter.chronicDiseaseCode);
                    }
                    if (extraFilter.clinics != null)
                    {
                        List<int?> clinicList = extraFilter.clinics.Select(c => c.id).ToList();
                        query = query.Where(g => clinicList.Contains(g.ClinicId));
                    }
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "patientName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.PatientName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.PatientName);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "patientLastName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.PatientLastName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.PatientLastName);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "birthDateString")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.PatientBirthDate);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.PatientBirthDate);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "patientAge")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.PatientAge);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.PatientAge);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "patientSex")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.PatientSex);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.PatientSex);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "countyName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.CountyName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.CountyName);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "patientPhoneNumber")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.PatientPhoneNumber);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.PatientPhoneNumber);
                            }
                        }
                    }
                    #endregion
                    result.total.value = query.Count();
                    if (filter.Length != -1)
                        query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (var registro in query.ToList())
                    {
                        PatientDiseaseAux aux = new PatientDiseaseAux();
                        DataHelper.fill(aux, registro);
                        result.data_list.Add(aux);
                    }
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

        public static HealthReportResult GetHealthReport(ReportFilter filter)
        {
            HealthReportResult result = new HealthReportResult();
            if (!filter.dateStart_.HasValue || filter.clinics == null || filter.clinics.Count == 0)
            {
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                db.Database.CommandTimeout = 180;
                try
                {
                    //Harcodeado =(
                    var query = db.vwReportHealth.AsQueryable();
                    if (filter.dateStart_.HasValue)
                    {
                        var date = filter.dateStart_.Value;
                        query = query.Where(q => q.Updated >= date);
                    }
                    if (filter.dateEnd_.HasValue)
                    {
                        var date = filter.dateEnd_.Value;
                        query = query.Where(q => q.Updated <= date);
                    }
                    if (filter.clinics != null)
                    {
                        List<int?> clinicList = filter.clinics.Select(c => c.id).ToList();
                        query = query.Where(g => clinicList.Contains(g.ClinicId));
                    }
                    List<int> idDiseases = query.GroupBy(q => q.DiseaseId).Select(gpo => gpo.FirstOrDefault().DiseaseId).ToList();
                    foreach (int idDisease in idDiseases)
                    {
                        HealthReportAux aux = new HealthReportAux();
                        string diseaseName = query.Where(q => q.DiseaseId == idDisease).Select(q => q.DiseaseName).FirstOrDefault();
                        aux.diseaseName = !String.IsNullOrEmpty(diseaseName) ? diseaseName : "SIN NOMBRE";

                        var queryConcept = query.Where(q => q.DiseaseId == idDisease);
                        #region Fill
                        aux.agesCount.Add("< 1", new AgesGroup
                        {
                            mCount = queryConcept.Where(qc => qc.Sex == "M" && qc.Age < 1).Count(),
                            fCount = queryConcept.Where(qc => qc.Sex == "F" && qc.Age < 1).Count()
                        });
                        aux.agesCount.Add("1-4", new AgesGroup
                        {
                            mCount = queryConcept.Where(qc => qc.Sex == "M" && qc.Age >= 1 && qc.Age <= 4).Count(),
                            fCount = queryConcept.Where(qc => qc.Sex == "F" && qc.Age >= 1 && qc.Age <= 4).Count()
                        });
                        aux.agesCount.Add("5-9", new AgesGroup
                        {
                            mCount = queryConcept.Where(qc => qc.Sex == "M" && qc.Age >= 5 && qc.Age <= 9).Count(),
                            fCount = queryConcept.Where(qc => qc.Sex == "F" && qc.Age >= 5 && qc.Age <= 9).Count()
                        });
                        aux.agesCount.Add("10-14", new AgesGroup
                        {
                            mCount = queryConcept.Where(qc => qc.Sex == "M" && qc.Age >= 10 && qc.Age <= 14).Count(),
                            fCount = queryConcept.Where(qc => qc.Sex == "F" && qc.Age >= 10 && qc.Age <= 14).Count()
                        });
                        aux.agesCount.Add("15-19", new AgesGroup
                        {
                            mCount = queryConcept.Where(qc => qc.Sex == "M" && qc.Age >= 15 && qc.Age <= 19).Count(),
                            fCount = queryConcept.Where(qc => qc.Sex == "F" && qc.Age >= 15 && qc.Age <= 19).Count()
                        });
                        aux.agesCount.Add("20-24", new AgesGroup
                        {
                            mCount = queryConcept.Where(qc => qc.Sex == "M" && qc.Age >= 20 && qc.Age <= 24).Count(),
                            fCount = queryConcept.Where(qc => qc.Sex == "F" && qc.Age >= 20 && qc.Age <= 24).Count()
                        });
                        aux.agesCount.Add("25-44", new AgesGroup
                        {
                            mCount = queryConcept.Where(qc => qc.Sex == "M" && qc.Age >= 25 && qc.Age <= 44).Count(),
                            fCount = queryConcept.Where(qc => qc.Sex == "F" && qc.Age >= 25 && qc.Age <= 44).Count()
                        });
                        aux.agesCount.Add("45-49", new AgesGroup
                        {
                            mCount = queryConcept.Where(qc => qc.Sex == "M" && qc.Age >= 45 && qc.Age <= 49).Count(),
                            fCount = queryConcept.Where(qc => qc.Sex == "F" && qc.Age >= 45 && qc.Age <= 49).Count()
                        });
                        aux.agesCount.Add("50-59", new AgesGroup
                        {
                            mCount = queryConcept.Where(qc => qc.Sex == "M" && qc.Age >= 50 && qc.Age <= 59).Count(),
                            fCount = queryConcept.Where(qc => qc.Sex == "F" && qc.Age >= 50 && qc.Age <= 59).Count()
                        });
                        aux.agesCount.Add("60-64", new AgesGroup
                        {
                            mCount = queryConcept.Where(qc => qc.Sex == "M" && qc.Age >= 60 && qc.Age <= 64).Count(),
                            fCount = queryConcept.Where(qc => qc.Sex == "F" && qc.Age >= 60 && qc.Age <= 64).Count()
                        });
                        aux.agesCount.Add(">= 65", new AgesGroup
                        {
                            mCount = queryConcept.Where(qc => qc.Sex == "M" && qc.Age >= 65).Count(),
                            fCount = queryConcept.Where(qc => qc.Sex == "F" && qc.Age >= 65).Count()
                        });
                        #endregion

                        result.data_list.Add(aux);
                    }
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

        public static CashClosingReportResult GetCashClosingReport(DTParameterModel filter, ReportFilter extraFilter)
        {
            CashClosingReportResult result = new CashClosingReportResult();
            if (!extraFilter.dateStart_.HasValue || extraFilter.clinics == null || extraFilter.clinics.Count == 0)
            {
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                db.Database.CommandTimeout = 180;
                string order = "";
                string orderColumn = "";
                if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
                {
                    order = filter.Order.First().Dir;
                    orderColumn = filter.Order.First().Data;
                }
                try
                {
                    var query = db.vwCashClosing.AsQueryable();
                    //Aplicar filtros
                    #region filtros
                    if (extraFilter.dateStart_.HasValue)
                    {
                        var date = extraFilter.dateStart_.Value;
                        query = query.Where(g => g.Date >= date);
                    }
                    if (extraFilter.dateEnd_.HasValue)
                    {
                        var date = extraFilter.dateEnd_.Value;
                        query = query.Where(g => g.Date <= date);
                    }
                    if (extraFilter.medicId.HasValue)
                    {
                        query = query.Where(g => g.EmployeeId == extraFilter.medicId.Value);
                    }
                    if (extraFilter.clinics != null)
                    {
                        List<int?> clinicList = extraFilter.clinics.Select(c => c.id).ToList();
                        query = query.Where(g => clinicList.Contains(g.ClinicId));
                    }
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "employeeName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.EmployeeName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.EmployeeName);
                            }
                        }
                        if (orderColumn == "number")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Number);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Number);
                            }
                        }
                        if (orderColumn == "efective")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Efective);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Efective);
                            }
                        }
                        if (orderColumn == "creditCard")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.CreditCard);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.CreditCard);
                            }
                        }
                        if (orderColumn == "vales")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Vales);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Vales);
                            }
                        }
                        if (orderColumn == "totalFisic")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.TotalFisic);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.TotalFisic);
                            }
                        }
                        if (orderColumn == "totalSale")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.TotalSale);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.TotalSale);
                            }
                        }
                        if (orderColumn == "dateString")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Date);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Date);
                            }
                        }
                        if (orderColumn == "soldItems")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.SoldItems);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.SoldItems);
                            }
                        }
                    }
                    #endregion
                    result.total.value = query.Count();
                    if (filter.Length != -1)
                        query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (var registro in query)
                    {
                        CashClosingReportAux aux = new CashClosingReportAux();
                        DataHelper.fill(aux, registro);
                        result.data_list.Add(aux);
                    }
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

        public static DonationsReportResult GetDonationsReport(DTParameterModel filter, ReportFilter extraFilter)
        {
            DonationsReportResult result = new DonationsReportResult();
            if (!extraFilter.dateStart_.HasValue || extraFilter.clinics == null || extraFilter.clinics.Count == 0)
            {
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                db.Database.CommandTimeout = 180;
                string order = "";
                string orderColumn = "";
                if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
                {
                    order = filter.Order.First().Dir;
                    orderColumn = filter.Order.First().Data;
                }

                try
                {
                    var query = db.vwDonations.AsQueryable();
                    //Aplicar filtros
                    #region filtros
                    if (extraFilter.dateStart_.HasValue)
                    {
                        var date = extraFilter.dateStart_.Value;
                        query = query.Where(g => g.Created >= date);
                    }
                    if (extraFilter.dateEnd_.HasValue)
                    {
                        var date = extraFilter.dateEnd_.Value;
                        query = query.Where(g => g.Created <= date);
                    }
                    if (extraFilter.medicId.HasValue)
                    {
                        query = query.Where(g => g.EmployeeId == extraFilter.medicId);
                    }
                    if (extraFilter.clinics != null)
                    {
                        List<int?> clinicList = extraFilter.clinics.Select(c => c.id).ToList();
                        query = query.Where(g => clinicList.Contains(g.ClinicId));
                    }
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "employeeName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.employeeName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.employeeName);
                            }
                        }
                        if (orderColumn == "ticket")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Ticket);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Ticket);
                            }
                        }
                        if (orderColumn == "donation")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Donation);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Donation);
                            }
                        }
                        if (orderColumn == "patientName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.patientName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.patientName);
                            }
                        }
                        if (orderColumn == "clinicName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.clinicName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.clinicName);
                            }
                        }
                        if (orderColumn == "createdString")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Created);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Created);
                            }
                        }
                    }
                    #endregion
                    result.total.value = query.Count();
                    if (filter.Length != -1)
                        query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (var registro in query.ToList())
                    {
                        DonationsReportAux aux = new DonationsReportAux();
                        DataHelper.fill(aux, registro);
                        result.data_list.Add(aux);
                    }
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

        public static GetWaitingTimesReportResult GetWaitingTimesReport(DTParameterModel model, ReportFilter filterextra)
        {
            GetWaitingTimesReportResult result = new GetWaitingTimesReportResult();
            if (!filterextra.dateStart_.HasValue || filterextra.clinic == null)
            {
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                db.Database.CommandTimeout = 180;
                string order = "";
                string orderColumn = "";
                if (!string.IsNullOrEmpty(model.Order.First().Dir) && !string.IsNullOrEmpty(model.Order.First().Data))
                {
                    order = model.Order.First().Dir;
                    orderColumn = model.Order.First().Data;
                }

                try
                {
                    var query = db.vwWaitingTimes.AsQueryable();
                    if (filterextra.dateStart_.HasValue)
                    {
                        var date = filterextra.dateStart_.Value;
                        var nDate = date.AddDays(1);
                        query = query.Where(q => q.Created >= date && q.Created <= nDate);
                    }
                    if (filterextra.clinic != null)
                    {
                        query = query.Where(g => g.ClinicId == filterextra.clinic.id);

                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "Id")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Id);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Id);
                            }
                        }

                        if (orderColumn == "ConsultId")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.ConsultId);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.ConsultId);
                            }
                        }
                        //if (orderColumn == "PatientId")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        query = query.OrderBy(q => q.PatientId);
                        //    }
                        //    else
                        //    {
                        //        query = query.OrderByDescending(q => q.PatientId);
                        //    }
                        //}
                        if (orderColumn == "FullName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.FullName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.FullName);
                            }
                        }
                        //if (orderColumn == "Created")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        query = query.OrderBy(q => q.Created);
                        //    }
                        //    else
                        //    {
                        //        query = query.OrderByDescending(q => q.Created);
                        //    }
                        //}
                        //if (orderColumn == "ClinicId")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        query = query.OrderBy(q => q.ClinicId);
                        //    }
                        //    else
                        //    {
                        //        query = query.OrderByDescending(q => q.ClinicId);
                        //    }
                        //}
                        //if (orderColumn == "Clinic")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        query = query.OrderBy(q => q.ClinicName);
                        //    }
                        //    else
                        //    {
                        //        query = query.OrderByDescending(q => q.ClinicName);
                        //    }
                        //}
                        if (orderColumn == "StartProcess")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.StartProcess);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.StartProcess);
                            }
                        }
                        if (orderColumn == "StartStageReception")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.StartStageReception);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.StartStageReception);
                            }
                        }
                        if (orderColumn == "EndStageReception")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.EndStageReception);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.EndStageReception);
                            }
                        }
                        if (orderColumn == "TAPReception")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.TAPReception);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.TAPReception);
                            }
                        }
                        if (orderColumn == "TEPRecNur")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.TEPRecNur);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.TEPRecNur);
                            }
                        }
                        if (orderColumn == "StartStageNursery")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.StartStageNursery);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.StartStageNursery);
                            }
                        }
                        if (orderColumn == "EndStageNursery")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.EndStageNursery);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.EndStageNursery);
                            }
                        }
                        if (orderColumn == "TAPNursery")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.TAPNursery);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.TAPNursery);
                            }
                        }
                        if (orderColumn == "TEPNurMed")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.TEPNurMed);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.TEPNurMed);
                            }
                        }
                        if (orderColumn == "StartStageMedic")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.StartStageMedic);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.StartStageMedic);
                            }
                        }
                        if (orderColumn == "EndStageMedic")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.EndStageMedic);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.EndStageMedic);
                            }
                        }
                        if (orderColumn == "TAPMedic")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.TAPMedic);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.TAPMedic);
                            }
                        }
                        //if (orderColumn == "EndProcess")
                        //{
                        //    if (order.ToUpper() == "ASC")
                        //    {
                        //        query = query.OrderBy(q => q.EndProcess);
                        //    }
                        //    else
                        //    {
                        //        query = query.OrderByDescending(q => q.EndProcess);
                        //    }
                        //}
                        if (orderColumn == "TotalMinutes")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.TotalMinutes);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.TotalMinutes);
                            }
                        }
                    }
                    result.total.value = query.Count();
                    if (model.Length != -1)
                        query = query.Skip(model.Start).Take(model.Length);
                    foreach (var registro in query.ToList())
                    {
                        
                            GetWaitingTimesReportAux aux = new GetWaitingTimesReportAux();
                            DataHelper.fill(aux, registro);
                            result.data_list.Add(aux);
                        
                    }
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
        public static GetWaitingTimesGraphResult GetWaitingTimesGraph(ReportFilter filter)
        {
            GetWaitingTimesGraphResult result = new GetWaitingTimesGraphResult();
            if (!filter.dateStart_.HasValue || filter.clinic == null)
            {
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.vwWaitingTimes.AsQueryable();
                    if (filter.dateStart_.HasValue)
                    {
                        var date = filter.dateStart_.Value;
                        var nDate = date.AddDays(1);
                        query = query.Where(q => q.Created >= date && q.Created <= nDate);
                    }
                    if (filter.clinic != null)
                    {
                        query = query.Where(g => g.ClinicId == filter.clinic.id);
                    }
                    foreach (var registro in query.ToList())
                    {
                        GetWaitingTimesGraphDataAux auxD = new GetWaitingTimesGraphDataAux();
                        GetWaitingTimesGraphLabelsAux auxL = new GetWaitingTimesGraphLabelsAux();
                        auxD.RecData = registro.TAPReception.HasValue ? registro.TAPReception.Value : 0;
                        auxD.RecNurData = registro.TEPRecNur.HasValue ? registro.TEPRecNur.Value : 0;
                        auxD.NurData = registro.TAPNursery.HasValue ? registro.TAPNursery.Value : 0;
                        auxD.NurMedData = registro.TEPNurMed.HasValue ? registro.TEPNurMed.Value : 0;
                        auxD.MedData = registro.TAPMedic.HasValue ? registro.TAPMedic.Value : 0;

                        auxL.Labels = registro.FullName;
                        result.data.DataArray.Add(auxD);
                        result.data.LabelsArray.Add(auxL);
                    }
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