using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMEDIK.Models.Helpers
{
    public class CancelationsAux
    {
        public int id { get; set; }
        public decimal total { get; set; }
        public string created { get; set; }
        public string employeeName { get; set; }
        public DateTime dateCancelled { get; set; }
        public int? Ticket { get; set; }
        public string stringDateCancelled
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(dateCancelled.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
    }

    public class OrderAux
    {
        public int id { get; set; }
        public int EmployeeId { get; set; }
        public int PatientId { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal Iva { get; set; }
        public decimal Donation { get; set; }
        public decimal? Rounding { get; set; }
        public bool Paid { get; set; }
        public bool? Pack { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int? InternmentId { get; set; }
        public int ClinicId { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime? DateCanceled { get; set; }
        public int? EmployeeCancelId { get; set; }
        public int? Ticket { get; set; }



        public List<OrderPackageAux> orderPackageAuxList { get; set; }
        public List<CancelledPackagesAux> cancelledPackageAuxList { get; set; }
        public CancelledPackagesAux cancelledPackageAux { get; set; }

        public OrderPackageAux orderPackageAux { get; set; }
        public List<OrderConceptAux> OrderConceptAux { get; set; }
        public PatientAux patientAux { get; set; }
        public ClinicAux clinicAux { get; set; }
        public EmployeeAux employeeAux { get; set; }
        public EmployeeAux EmployeeCancel { get; set; }
        public List<PaymentAux> paymentAux { get; set; }
        public List<OrderPromotionAux> Promotions { get; set; }
        public List<OrderPromotionDiscountAppliedAux> PromotionsApplied { get; set; }
        public string sCreated
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(Created.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public string sUpdated
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(Updated.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public string sDateCanceled
        {
            get
            {
                return DateCanceled.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(DateCanceled.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MMMM/yyyy HH:mm:ss", new CultureInfo("es-MX"))
                : "";
            }
        }
        public decimal totalpayed
        {
            get
            {
                decimal tot = 0;
                if (paymentAux != null)
                {
                    foreach (var payment in paymentAux)
                    {
                        tot = tot + payment.Amount + payment.Commission;
                    }
                }
                return tot;

            }
        }

        public decimal paymentAmmount
        {
            get
            {
                decimal total = 0;
                if (paymentAux != null)
                {
                    foreach (var payment in paymentAux)
                    {
                        total = total + payment.Amount;
                    }
                }
                return total;

            }
        }

        public decimal packageselectediva
        {
            get
            {
                decimal ammountiva = 0;
                if (orderPackageAuxList != null)
                {
                    foreach (var package in orderPackageAuxList)
                    {
                        if (package.packageSelected == true)
                        {
                            if (package.packageAux.iva)
                            {
                                ammountiva = package.packageAux.price * .16m;
                            }
                        }
                    }
                }
                return ammountiva;
            }
        }

        public decimal packagenoselectediva
        {
            get
            {
                decimal ammountiva = 0;
                if (orderPackageAuxList != null)
                {
                    foreach (var package in orderPackageAuxList)
                    {
                        if (package.packageSelected == false)
                        {
                            if (package.packageAux.iva)
                            {
                                ammountiva = package.packageAux.price * .16m;
                            }
                        }
                    }
                }
                return ammountiva;
            }
        }

        public decimal packageselectedammount
        {
            get
            {
                decimal packageammount = 0;
                if (orderPackageAuxList != null)
                {
                    foreach (var package in orderPackageAuxList)
                    {
                        if (package.packageSelected == true)
                        {
                            packageammount = packageammount + package.packageAux.price;
                        }
                    }
                }
                return packageammount;
            }
        }

        public decimal packagenoselectedammount
        {
            get
            {
                decimal packageammount = 0;
                if (orderPackageAuxList != null)
                {
                    foreach (var package in orderPackageAuxList)
                    {
                        if (package.packageSelected == false)
                        {
                            packageammount = packageammount + package.packageAux.price;
                        }
                    }
                }
                return packageammount;
            }
        }

        public decimal totalpackselected
        {
            get
            {
                decimal totalpack = 0;
                totalpack = packageselectediva + packageselectedammount;
                return totalpack;
            }
        }

        public decimal totalpacknoselected
        {
            get
            {
                decimal totalpack = 0;
                totalpack = packagenoselectediva + packagenoselectedammount;
                return totalpack;
            }
        }

        public decimal refund
        {
            get
            {
                decimal totalrefund = 0;
                totalrefund = totalpackselected + totalpacknoselected;
                return totalrefund;
            }
        }


        public decimal totalpackagesselected
        {
            get
            {
                decimal totalpackselected = 0;
                decimal ammountiva = 0;

                if (orderPackageAuxList != null)
                {
                    foreach (var package in orderPackageAuxList)
                    {
                        if (package.packageSelected)
                        {
                            if (package.packageAux.iva)
                            {
                                ammountiva = package.packageAux.price * .16m;
                                totalpackselected = totalpackselected + ammountiva + package.packageAux.price;
                            }
                            else
                            {
                                totalpackselected = totalpackselected + package.packageAux.price;
                            }
                        }
                    }
                }
                return totalpackselected;
            }
        }

        public decimal totalpackagesnoselected
        {
            get
            {
                decimal totalpacknoselected = 0;
                decimal ammountiva = 0;

                if (orderPackageAuxList != null)
                {
                    foreach (var package in orderPackageAuxList)
                    {
                        if (package.packageSelected == false)
                        {
                            if (package.packageAux.iva)
                            {
                                ammountiva = package.packageAux.price * .16m;
                                totalpacknoselected = totalpacknoselected + ammountiva + package.packageAux.price;
                            }
                            else
                            {
                                totalpacknoselected = totalpacknoselected + package.packageAux.price;
                            }
                        }
                    }
                }
                return totalpacknoselected;
            }
        }


        public decimal totalrefund
        {
            get
            {
                decimal refund = 0;


                refund = (totalpackagesnoselected - paymentAmmount) * -1;

                if (refund < 0)
                {
                    refund = paymentAmmount;
                }


                return refund;
            }
        }

        public bool AllPackSelected
        {
            get
            {
                bool allpackages = false;
                decimal countpack = 0;

                if (orderPackageAuxList != null)
                {
                    foreach (var package in orderPackageAuxList)
                    {
                        if (package.packageSelected == true)
                        {
                            countpack = countpack + 1;
                        }
                    }
                    if (orderPackageAuxList.Count() == countpack && countpack != 0)
                    {
                        allpackages = true;
                    }
                }
                return allpackages;
            }
        }


        public OrderAux()
        {
            OrderConceptAux = new List<OrderConceptAux>();
            orderPackageAux = new OrderPackageAux();
            patientAux = new PatientAux();
            clinicAux = new ClinicAux();
            employeeAux = new EmployeeAux();
            EmployeeCancel = new EmployeeAux();
            paymentAux = new List<PaymentAux>();
            Promotions = new List<OrderPromotionAux>();
            PromotionsApplied = new List<OrderPromotionDiscountAppliedAux>();
            orderPackageAuxList = new List<OrderPackageAux>();
            cancelledPackageAuxList = new List<CancelledPackagesAux>();
            cancelledPackageAux = new CancelledPackagesAux();
        }
    }
    public class OrderPackageAux
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public int packageId { get; set; }
        public int medicId { get; set; }
        public DateTime scheduled { get; set; }
        public string medicName { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }

        public PatientAux patientAux { get; set; }
        public PackageAux packageAux { get; set; }
        public EmployeeAux medic { get; set; }
        public ClinicAux clinicAux { get; set; }
        public bool packageSelected { get; set; }
        public string reason { get; set; }


        public OrderPackageAux()
        {
            patientAux = new PatientAux();
            packageAux = new PackageAux();
            medic = new EmployeeAux();
            clinicAux = new ClinicAux();
        }
    }
    public class OrderResult : Result
    {
        public OrderAux data { get; set; }
        public List<OrderAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public OrderResult()
        {
            this.data = new OrderAux();
            this.data_list = new List<OrderAux>();
            this.total = new NumericResult();
        }
    }

    public class vwSalesResult : Result
    {
        public vwSalesAux data { get; set; }
        public List<vwSalesAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public vwSalesResult()
        {
            this.data = new vwSalesAux();
            this.data_list = new List<vwSalesAux>();
            this.total = new NumericResult();
        }
    }
    public class CancellationsResult : Result
    {
        public CancelationsAux data { get; set; }
        public List<CancelationsAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public CancellationsResult()
        {
            this.data_list = new List<CancelationsAux>();
            this.total = new NumericResult();
        }
    }

    public class OrderConceptAux
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public int? conceptId { get; set; }
        public int? medicId { get; set; }
        //public int? referenceId { get; set; }
        public int quantity { get; set; }
        public decimal discount { get; set; }
        public decimal price { get; set; }
        public decimal cost { get; set; }
        public decimal total { get; set; }
        public decimal iva { get; set; }
        public DateTime? scheduled { get; set; }
        public string medicname { get; set; }
        public string decree { get; set; }
        public DateTime created { get; set; }
        public DateTime? updated { get; set; }
        public ConceptAux ConceptAux { get; set; }
        public int ClinicId { get; set; }
        public string categoryName { get; set; }
        public int? Stockid { get; set; }
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
        public OrderConceptAux()
        {
            ConceptAux = new ConceptAux();
        }
    }

    public class Order_ConceptResult : Result
    {
        public OrderConceptAux data { get; set; }
        public List<OrderConceptAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public Order_ConceptResult()
        {
            this.data = new OrderConceptAux();
            this.data_list = new List<OrderConceptAux>();
            this.total = new NumericResult();
        }
    }

    public class vwSalesAux
    {
        public int OrderId { get; set; }
        public int OrderTicket { get; set; }
        public string Ticket { get; set; }
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeFullName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFullName { get; set; }
        public decimal Total { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public bool Paid { get; set; }
        public bool IsCanceled { get; set; }
        public string sCreated { get; set; }
        public DateTime PaymentCreated { get; set; }
        public string createdString
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(PaymentCreated.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public vwSalesAux()
        {

        }
    }

    public class OrderHelper
    {
        public static OrderResult GetOrderedPackages(DTParameterModel filter, int ClinicId)
        {
            OrderResult result = new OrderResult();
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Orders.Where(pt => !pt.Internment.Any() && pt.OrderPackage.Any() && pt.OrderPackage.Where(op => op.ClinicId == ClinicId).Any());
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "id" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.id.ToString().Contains(column.Search.Value));
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "id")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.id);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.id);
                            }
                        }
                    }

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (Orders Or in query.ToList())
                    {
                        OrderAux aux = new OrderAux();
                        OrderPackageAux package = new OrderPackageAux();
                        DataHelper.fill(aux, Or);
                        DataHelper.fill(package.patientAux, Or.Patient);
                        DataHelper.fill(package.patientAux.personAux, Or.Patient.Person);
                        DataHelper.fill(package.packageAux, Or.OrderPackage.FirstOrDefault().Package);
                        DataHelper.fill(package.medic, Or.OrderPackage.FirstOrDefault().Employee);
                        DataHelper.fill(package.medic.personAux, Or.OrderPackage.FirstOrDefault().Employee.Person);
                        aux.orderPackageAux = package;

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

        public static vwInternmentResult GetInternments(DTParameterModel filter, int ClinicId)
        {
            vwInternmentResult result = new vwInternmentResult();
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<vwInternment> query = db.vwInternment;
                    query = query.Where(q => q.ClinicId == ClinicId);

                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "id" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.id.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "PatientName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.PatientName.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "sCreated" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.sCreated.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "Name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Name.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "MedicName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.MedicName.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "Total" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Total.ToString().Contains(column.Search.Value));
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "id")
                        {
                            //if (order.ToUpper() == "ASC")
                            //{
                            //    query = query.OrderBy(q => q.id);
                            //}
                            //else
                            //{
                            query = query.OrderByDescending(q => q.id);
                            //}
                        }
                        if (orderColumn == "PatientName")
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
                        if (orderColumn == "sCreated")
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
                        if (orderColumn == "Name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Name);
                            }
                        }
                        if (orderColumn == "MedicName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.MedicName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.MedicName);
                            }
                        }
                        if (orderColumn == "Total")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Total);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Total);
                            }
                        }
                    }

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (vwInternment Or in query.ToList())
                    {
                        vwInternmentAux aux = new vwInternmentAux();
                        //    OrderPackageAux package = new OrderPackageAux();
                        DataHelper.fill(aux, Or);
                        //    DataHelper.fill(package.patientAux, Or.Patient);
                        //    DataHelper.fill(package.patientAux.personAux, Or.Patient.Person);
                        //    DataHelper.fill(package.packageAux, Or.OrderPackage.FirstOrDefault().Package);
                        //    DataHelper.fill(package.medic, Or.OrderPackage.FirstOrDefault().Employee);
                        //    DataHelper.fill(package.medic.personAux, Or.OrderPackage.FirstOrDefault().Employee.Person);
                        //    aux.orderPackageAux = package;

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

        public static Result ConfirmInternment(int idOrder)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var order = db.Orders.Where(o => o.id == idOrder).FirstOrDefault();
                    if (order != null)
                    {
                        Internment newInternment = db.Internment.Create();
                        newInternment.OrderId = order.id;
                        newInternment.PatientId = order.PatientId;
                        newInternment.InternmentDate = DateTime.UtcNow;
                        if (order.OrderPackage.Any())
                        {
                            newInternment.PackageId = order.OrderPackage.FirstOrDefault().PackageId;
                            newInternment.ClinicId = order.OrderPackage.FirstOrDefault().ClinicId;
                            db.Internment.Add(newInternment);
                            db.SaveChanges();
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = "Cirugía no encontrada. Vuelva a intentar más tarde.";
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Cirugía no encontrada. Vuelva a intentar más tarde.";
                    }
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

        public static OrderResult getpackageByid(int idOrder)
        {
            OrderResult result = new OrderResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var orderDb = db.Orders.Where(o => o.id == idOrder).FirstOrDefault();
                    var cancelledpackDb = db.CancelledPackages.Where(o => o.OldOrder == idOrder);

                    if (orderDb != null)
                    {
                        DataHelper.fill(result.data, orderDb);
                        DataHelper.fill(result.data.patientAux, orderDb.Patient);
                        DataHelper.fill(result.data.patientAux.personAux, orderDb.Patient.Person);
                        DataHelper.fill(result.data.employeeAux, orderDb.Employee);
                        DataHelper.fill(result.data.employeeAux.personAux, orderDb.Employee.Person);
                        DataHelper.fill(result.data.clinicAux, orderDb.Clinic);
                        DataHelper.fill(result.data.clinicAux.addressAux, orderDb.Clinic.Address);
                        DataHelper.fill(result.data.clinicAux.addressAux.countyAux, orderDb.Clinic.Address.County);
                        DataHelper.fill(result.data.clinicAux.addressAux.countyAux.cityAux, orderDb.Clinic.Address.County.City);
                        DataHelper.fill(result.data.clinicAux.addressAux.countyAux.cityAux.stateAux, orderDb.Clinic.Address.County.City.State);
                        if (orderDb.Employee1 != null)
                        {
                            DataHelper.fill(result.data.EmployeeCancel, orderDb.Employee1);
                            DataHelper.fill(result.data.EmployeeCancel.personAux, orderDb.Employee1.Person);
                        }
                        foreach (var orderConceptDb in orderDb.OrdersConcepts)
                        {
                            OrderConceptAux ocAux = new OrderConceptAux();
                            DataHelper.fill(ocAux, orderConceptDb);
                            DataHelper.fill(ocAux.ConceptAux, orderConceptDb.Concept);
                            DataHelper.fill(ocAux.ConceptAux.categoryAux, orderConceptDb.Concept.Category);
                            //result.data.Discount = result.data.OrderConceptAux.Sum(o => o.discount * o.quantity);
                            result.data.Discount = result.data.Discount + (orderConceptDb.Discount * orderConceptDb.Quantity);
                            result.data.OrderConceptAux.Add(ocAux);
                        }
                        foreach (var orderPackageDB in orderDb.OrderPackage)
                        {
                            if (orderDb.CancelledPackages.Any(p => p.OrderPackageId == orderPackageDB.Id))
                            {
                                OrderPackageAux auxop = new OrderPackageAux();
                                DataHelper.fill(auxop, orderPackageDB);
                                DataHelper.fill(auxop.packageAux, orderPackageDB.Package);
                                DataHelper.fill(auxop.packageAux.categoryAux, orderPackageDB.Package.Category);
                                DataHelper.fill(auxop.clinicAux, orderPackageDB.Clinic);
                                DataHelper.fill(auxop.clinicAux.addressAux, orderPackageDB.Clinic.Address);
                                DataHelper.fill(auxop.medic, orderPackageDB.Employee);
                                DataHelper.fill(auxop.medic.personAux, orderPackageDB.Employee.Person);
                                result.data.orderPackageAuxList.Add(auxop);
                            }
                        }

                        foreach (var cancelledPackgeDB in orderDb.CancelledPackages)
                        {
                            CancelledPackagesAux cpaux = new CancelledPackagesAux();
                            DataHelper.fill(cpaux, cancelledPackgeDB);
                            result.data.cancelledPackageAuxList.Add(cpaux);
                        }

                        //foreach (var orderPackageDB in orderDb.OrderPackage)
                        //{
                        //    DataHelper.fill(result.data.orderPackageAux, orderPackageDB);
                        //    DataHelper.fill(result.data.orderPackageAux.packageAux, orderPackageDB.Package);
                        //    DataHelper.fill(result.data.orderPackageAux.packageAux.categoryAux, orderPackageDB.Package.Category);
                        //    DataHelper.fill(result.data.orderPackageAux.clinicAux, orderPackageDB.Clinic);
                        //    DataHelper.fill(result.data.orderPackageAux.medic, orderPackageDB.Employee);
                        //    DataHelper.fill(result.data.orderPackageAux.medic.personAux, orderPackageDB.Employee.Person);
                        //}

                        foreach (var PaymentItem in orderDb.Payment)
                        {
                            PaymentAux aux = new PaymentAux();
                            DataHelper.fill(aux, PaymentItem);
                            DataHelper.fill(aux.PaymentTypeAux, PaymentItem.PaymentType);
                            DataHelper.fill(aux.clinicAux, PaymentItem.Clinic);
                            DataHelper.fill(aux.employeeAux, PaymentItem.Employee);
                            DataHelper.fill(aux.employeeAux.personAux, PaymentItem.Employee.Person);
                            result.data.paymentAux.Add(aux);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.message = "No se encontro la orden.";
                    }
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

        public static OrderResult getOrderByid(int idOrder)
        {
            OrderResult result = new OrderResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var orderDb = db.Orders.Where(o => o.id == idOrder).FirstOrDefault();
                    var cancelledpackDb = db.CancelledPackages.Where(cp => cp.NewOrder == idOrder);

                    if (orderDb != null)
                    {
                        DataHelper.fill(result.data, orderDb);
                        DataHelper.fill(result.data.patientAux, orderDb.Patient);
                        DataHelper.fill(result.data.patientAux.personAux, orderDb.Patient.Person);
                        DataHelper.fill(result.data.employeeAux, orderDb.Employee);
                        DataHelper.fill(result.data.employeeAux.personAux, orderDb.Employee.Person);
                        DataHelper.fill(result.data.clinicAux, orderDb.Clinic);
                        DataHelper.fill(result.data.clinicAux.addressAux, orderDb.Clinic.Address);
                        DataHelper.fill(result.data.clinicAux.addressAux.countyAux, orderDb.Clinic.Address.County);
                        DataHelper.fill(result.data.clinicAux.addressAux.countyAux.cityAux, orderDb.Clinic.Address.County.City);
                        DataHelper.fill(result.data.clinicAux.addressAux.countyAux.cityAux.stateAux, orderDb.Clinic.Address.County.City.State);
                        if (orderDb.Employee1 != null)
                        {
                            DataHelper.fill(result.data.EmployeeCancel, orderDb.Employee1);
                            DataHelper.fill(result.data.EmployeeCancel.personAux, orderDb.Employee1.Person);
                        }
                        foreach (var orderConceptDb in orderDb.OrdersConcepts)
                        {
                            OrderConceptAux ocAux = new OrderConceptAux();
                            DataHelper.fill(ocAux, orderConceptDb);
                            DataHelper.fill(ocAux.ConceptAux, orderConceptDb.Concept);
                            DataHelper.fill(ocAux.ConceptAux.categoryAux, orderConceptDb.Concept.Category);
                            result.data.Discount = result.data.Discount + (orderConceptDb.Discount * orderConceptDb.Quantity);
                            result.data.OrderConceptAux.Add(ocAux);
                        }
                        foreach (var cancelledPackgeDB in cancelledpackDb)
                        {
                            CancelledPackagesAux cpaux = new CancelledPackagesAux();
                            DataHelper.fill(cpaux, cancelledPackgeDB);
                            result.data.cancelledPackageAuxList.Add(cpaux);
                        }

                        foreach (var orderPackageDB in orderDb.OrderPackage)
                        {
                            OrderPackageAux auxop = new OrderPackageAux();
                            DataHelper.fill(auxop, orderPackageDB);
                            DataHelper.fill(auxop.packageAux, orderPackageDB.Package);
                            DataHelper.fill(auxop.packageAux.categoryAux, orderPackageDB.Package.Category);
                            DataHelper.fill(auxop.clinicAux, orderPackageDB.Clinic);
                            DataHelper.fill(auxop.clinicAux.addressAux, orderPackageDB.Clinic.Address);
                            DataHelper.fill(auxop.medic, orderPackageDB.Employee);
                            result.data.orderPackageAuxList.Add(auxop);
                        }

                        foreach (var orderPackageDB in orderDb.OrderPackage)
                        {
                            DataHelper.fill(result.data.orderPackageAux, orderPackageDB);
                            DataHelper.fill(result.data.orderPackageAux.packageAux, orderPackageDB.Package);
                            DataHelper.fill(result.data.orderPackageAux.packageAux.categoryAux, orderPackageDB.Package.Category);
                            DataHelper.fill(result.data.orderPackageAux.clinicAux, orderPackageDB.Clinic);
                            DataHelper.fill(result.data.orderPackageAux.medic, orderPackageDB.Employee);
                            DataHelper.fill(result.data.orderPackageAux.medic.personAux, orderPackageDB.Employee.Person);
                        }


                        foreach (var PaymentItem in orderDb.Payment)
                        {
                            PaymentAux aux = new PaymentAux();
                            DataHelper.fill(aux, PaymentItem);
                            DataHelper.fill(aux.PaymentTypeAux, PaymentItem.PaymentType);
                            DataHelper.fill(aux.clinicAux, PaymentItem.Clinic);
                            DataHelper.fill(aux.employeeAux, PaymentItem.Employee);
                            DataHelper.fill(aux.employeeAux.personAux, PaymentItem.Employee.Person);
                            result.data.paymentAux.Add(aux);
                        }

                        foreach (var OrderPromotionItem in orderDb.OrderPromotion)
                        {
                            OrderPromotionAux aux = new OrderPromotionAux();
                            DataHelper.fill(aux, OrderPromotionItem);
                            result.data.Promotions.Add(aux);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.message = "No se encontro la orden.";
                    }
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

        public static OrderResult getTicketByid(int idTicket, int idClinic)
        {
            OrderResult result = new OrderResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var orderDb = db.Orders.Where(o => o.Ticket == idTicket && o.ClinicId == idClinic).FirstOrDefault();
                    if (orderDb != null)
                    {
                        DataHelper.fill(result.data, orderDb);
                        DataHelper.fill(result.data.patientAux, orderDb.Patient);
                        DataHelper.fill(result.data.patientAux.personAux, orderDb.Patient.Person);
                        DataHelper.fill(result.data.employeeAux, orderDb.Employee);
                        DataHelper.fill(result.data.employeeAux.personAux, orderDb.Employee.Person);
                        DataHelper.fill(result.data.clinicAux, orderDb.Clinic);
                        DataHelper.fill(result.data.clinicAux.addressAux, orderDb.Clinic.Address);
                        DataHelper.fill(result.data.clinicAux.addressAux.countyAux, orderDb.Clinic.Address.County);
                        DataHelper.fill(result.data.clinicAux.addressAux.countyAux.cityAux, orderDb.Clinic.Address.County.City);
                        DataHelper.fill(result.data.clinicAux.addressAux.countyAux.cityAux.stateAux, orderDb.Clinic.Address.County.City.State);
                        if (orderDb.Employee1 != null)
                        {
                            DataHelper.fill(result.data.EmployeeCancel, orderDb.Employee1);
                            DataHelper.fill(result.data.EmployeeCancel.personAux, orderDb.Employee1.Person);
                        }
                        foreach (var orderConceptDb in orderDb.OrdersConcepts)
                        {
                            OrderConceptAux ocAux = new OrderConceptAux();
                            DataHelper.fill(ocAux, orderConceptDb);
                            DataHelper.fill(ocAux.ConceptAux, orderConceptDb.Concept);
                            DataHelper.fill(ocAux.ConceptAux.categoryAux, orderConceptDb.Concept.Category);
                            result.data.OrderConceptAux.Add(ocAux);
                        }
                        foreach (var orderPackageDB in orderDb.OrderPackage)
                        {
                            DataHelper.fill(result.data.orderPackageAux, orderPackageDB);
                            DataHelper.fill(result.data.orderPackageAux.packageAux, orderPackageDB.Package);
                            DataHelper.fill(result.data.orderPackageAux.packageAux.categoryAux, orderPackageDB.Package.Category);
                            DataHelper.fill(result.data.orderPackageAux.clinicAux, orderPackageDB.Clinic);
                            DataHelper.fill(result.data.orderPackageAux.medic, orderPackageDB.Employee);
                        }
                        foreach (var PaymentItem in orderDb.Payment)
                        {
                            PaymentAux aux = new PaymentAux();
                            DataHelper.fill(aux, PaymentItem);
                            DataHelper.fill(aux.PaymentTypeAux, PaymentItem.PaymentType);
                            result.data.paymentAux.Add(aux);
                        }

                        foreach (var OrderPromotionItem in orderDb.OrderPromotion)
                        {
                            OrderPromotionAux aux = new OrderPromotionAux();
                            DataHelper.fill(aux, OrderPromotionItem);
                            result.data.Promotions.Add(aux);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.message = "No se encontro la orden.";
                    }


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

        public static OrderResult getOrderByTicket(int idTicket, int idClinic)
        {
            OrderResult result = new OrderResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var orderDb = db.Orders.Where(o => o.Ticket == idTicket && o.ClinicId == idClinic).FirstOrDefault();
                    if (orderDb != null)
                    {
                        DataHelper.fill(result.data, orderDb);

                        if (orderDb.Employee1 != null)
                        {
                            DataHelper.fill(result.data.EmployeeCancel, orderDb.Employee1);
                        }
                        foreach (var orderConceptDb in orderDb.OrdersConcepts)
                        {
                            OrderConceptAux ocAux = new OrderConceptAux();
                            DataHelper.fill(ocAux, orderConceptDb);
                            DataHelper.fill(ocAux.ConceptAux, orderConceptDb.Concept);
                            DataHelper.fill(ocAux.ConceptAux.categoryAux, orderConceptDb.Concept.Category);
                            result.data.OrderConceptAux.Add(ocAux);
                        }
                        foreach (var orderPackageDB in orderDb.OrderPackage)
                        {
                            DataHelper.fill(result.data.orderPackageAux, orderPackageDB);
                            DataHelper.fill(result.data.orderPackageAux.packageAux, orderPackageDB.Package);
                            DataHelper.fill(result.data.orderPackageAux.packageAux.categoryAux, orderPackageDB.Package.Category);
                            DataHelper.fill(result.data.orderPackageAux.clinicAux, orderPackageDB.Clinic);
                            DataHelper.fill(result.data.orderPackageAux.medic, orderPackageDB.Employee);
                        }
                        foreach (var PaymentItem in orderDb.Payment)
                        {
                            PaymentAux aux = new PaymentAux();
                            DataHelper.fill(aux, PaymentItem);
                            DataHelper.fill(aux.PaymentTypeAux, PaymentItem.PaymentType);
                            result.data.paymentAux.Add(aux);
                        }

                        foreach (var OrderPromotionItem in orderDb.OrderPromotion)
                        {
                            OrderPromotionAux aux = new OrderPromotionAux();
                            DataHelper.fill(aux, OrderPromotionItem);
                            result.data.Promotions.Add(aux);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.message = "No se encontro el ticket.";
                    }


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

        public static Result cancelOrder(int idOrder, int EmployeeCancelId)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var orderDb = db.Orders.Where(o => o.id == idOrder).FirstOrDefault();
                    if (orderDb != null)
                    {
                        orderDb.EmployeeCancelId = EmployeeCancelId;
                        orderDb.IsCanceled = true;
                        orderDb.DateCanceled = DateTime.UtcNow;

                        foreach (var orderConceptDb in orderDb.OrdersConcepts)
                        {
                            if (orderConceptDb.Stock != null)
                            {
                                orderConceptDb.Stock.InStock = orderConceptDb.Stock.InStock + orderConceptDb.Quantity;
                            }
                        }

                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.message = "No se encontro la orden.";
                    }


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
        public static Result SaveTimeResult(int waitingTimeId)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var waitingTimeDb = db.WaitingTimes.Where(t => t.Id == waitingTimeId).FirstOrDefault();
                    if (waitingTimeDb != null)
                    {

                        string StartProcess = TimeZoneInfo.ConvertTimeFromUtc(
                    new DateTime(waitingTimeDb.AttendingTimePatientRecS.Value.Ticks, DateTimeKind.Utc),
                    TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                    ).ToString("HH:mm", new CultureInfo("es-MX"));
                        string EndStageReception = TimeZoneInfo.ConvertTimeFromUtc(
                    new DateTime(waitingTimeDb.AttendingTimePatientRecF.Value.Ticks, DateTimeKind.Utc),
                    TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                    ).ToString("HH:mm", new CultureInfo("es-MX"));

                        var TAPR = (waitingTimeDb.AttendingTimePatientRecF - waitingTimeDb.AttendingTimePatientRecS).Value.TotalMinutes;

                        WaitingTimeCalc CalcDate = new WaitingTimeCalc();
                        CalcDate.WaitingTimeId = waitingTimeId;
                        CalcDate.StartProcess = StartProcess;
                        CalcDate.StartStageReception = StartProcess;
                        CalcDate.EndStageReception = EndStageReception;
                        CalcDate.TAPReception = Convert.ToInt32(TAPR);
                        db.SaveChanges();
                    }
                    result.success = true;

                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
                return result;
            }
        }
        public static GenericResult saveOrder(OrderAux Order, List<OrderConceptAux> Concepts, List<PaymentAux> Payments, List<OrderPromotionAux> Promotions, List<OrderPromotionDiscountAppliedAux> PromotionsApplied, DateTime dateTime)
        {
            GenericResult result = new GenericResult();
            bool EverythingInStock = true;
            List<string> NamesNotEnoughStock = new List<string>();
            decimal ivaConfig = ParameterHelper.GetTax().value;

            using (dbINMEDIK db = new dbINMEDIK())
            {
                Orders newOrder = new Orders();
                int newOrderId = 0;
                try
                {
                    /*primero revizamos que se tenga una cantidad suficiente del producto en stock*/
                    #region se reviza que los productos que se quieren esten en stock
                    foreach (var concept in Concepts.Where(c => c.categoryName == "Productos"))
                    {
                        Concept conceptDb = db.Concept.Where(c => c.id == concept.conceptId.Value).FirstOrDefault();
                        List<Stock> conceptInStock = conceptDb.Stock.Where(s => s.ClinicId == Order.ClinicId).OrderBy(s => s.Created).ToList();
                        if (!(conceptInStock.Count() > 0 && conceptInStock.Sum(s => s.InStock) >= concept.quantity))
                        {
                            EverythingInStock = false;
                            NamesNotEnoughStock.Add(conceptDb.Name);
                        }
                    }
                    #endregion
                    /*si todos los conceptos de la categoria "Productos" estan en stock, se continua */
                    if (EverythingInStock)
                    {
                        #region se crea la orden y se agrega  a la base de datos
                        /*guardamos la nueva orden*/
                        newOrder.EmployeeId = Order.EmployeeId;
                        newOrder.PatientId = Order.PatientId;
                        newOrder.Discount = Order.Discount;
                        newOrder.Total = Order.Total;
                        newOrder.Iva = Order.Iva;
                        newOrder.Donation = Order.Donation;
                        newOrder.Rounding = Order.Rounding;
                        newOrder.Paid = Order.Paid;
                        newOrder.Pack = Order.Pack;
                        newOrder.ClinicId = Order.ClinicId;
                        newOrder.Created = DateTime.UtcNow;
                        newOrder.Updated = DateTime.UtcNow;
                        db.Orders.Add(newOrder);

                        #endregion
                        #region se guardan los conceptos que NO son productos
                        /*guardamos los conceptos que no sean productos ya que los productos tienen un trato diferente*/
                        var StatusEnEnfermeria = db.Status.Where(s => s.Name == "Enfermeria").FirstOrDefault();
                        foreach (var concept in Concepts.Where(c => c.categoryName != "Productos"))
                        {
                            if (concept.categoryName == "Paquetes")
                            {
                                Package packageDb = db.Package.Where(p => p.id == concept.conceptId.Value).FirstOrDefault();
                                OrderPackage newOrderPackage = new OrderPackage();

                                newOrderPackage.PackageId = concept.conceptId.Value;
                                if (concept.medicId.HasValue)
                                {
                                    newOrderPackage.MedicId = concept.medicId.Value;
                                }
                                newOrderPackage.ClinicId = concept.ClinicId;
                                newOrderPackage.Scheduled = concept.scheduled;
                                newOrderPackage.Medicname = concept.medicname;
                                newOrderPackage.Created = DateTime.UtcNow;
                                newOrderPackage.Updated = DateTime.UtcNow;
                                newOrder.OrderPackage.Add(newOrderPackage);

                            }
                            else
                            {
                                Concept conceptDb = db.Concept.Where(c => c.id == concept.conceptId.Value).FirstOrDefault();
                                OrdersConcepts newOrderConcept = new OrdersConcepts();
                                DiscountConcept dbDiscount =
                                           conceptDb.DiscountConcept
                                           .Where(d => !d.Discount.Deleted
                                           && d.Discount.Clinic.Any(c => c.id == Order.ClinicId)
                                           && d.Discount.StartDate <= DateTime.UtcNow
                                           && d.Discount.EndDate >= DateTime.UtcNow
                                           && d.Discount.DiscountType.Code == "Direct"
                                           )
                                           .FirstOrDefault();
                                decimal realPrice = 0;
                                if (dbDiscount != null)
                                {
                                    realPrice = conceptDb.Price - (conceptDb.Price * dbDiscount.Percentage / 100);
                                }
                                else
                                {
                                    realPrice = conceptDb.Price;
                                }
                                //newOrderConcept.Orders = newOrder.id;
                                newOrderConcept.ConceptId = concept.conceptId.Value;
                                if (concept.medicId.HasValue)
                                {
                                    newOrderConcept.MedicId = concept.medicId.Value;
                                }
                                newOrderConcept.ClinicId = concept.ClinicId;
                                newOrderConcept.Quantity = concept.quantity;
                                newOrderConcept.Discount = concept.discount;
                                newOrderConcept.Price = realPrice;
                                newOrderConcept.Discount = (conceptDb.Iva ? realPrice + (realPrice * ivaConfig / 100) : realPrice) - (concept.total / concept.quantity);
                                newOrderConcept.Cost = conceptDb.Cost;
                                newOrderConcept.Iva = (conceptDb.Iva ? (realPrice * ivaConfig / 100) : 0);
                                newOrderConcept.Total = concept.total;
                                newOrderConcept.Scheduled = concept.scheduled;
                                newOrderConcept.Medicname = concept.medicname;
                                newOrderConcept.Decree = concept.decree;
                                newOrderConcept.Created = DateTime.UtcNow;
                                newOrderConcept.Updated = DateTime.UtcNow;
                                //newOrderConcept.Employee = ;

                                newOrder.OrdersConcepts.Add(newOrderConcept);

                                /*Guardamos en la tabla de Exam/Service/Consult*/
                                switch (concept.categoryName)
                                {
                                    case "Servicios":
                                        Service newService = new Service();
                                        newService.PatientId = Order.PatientId;
                                        newService.ClinicId = Order.ClinicId;
                                        if (concept.medicId.HasValue)
                                        {
                                            newService.CreatedBy = concept.medicId.Value;
                                        }
                                        newService.StatusId = StatusEnEnfermeria.id;
                                        newService.ConceptId = concept.conceptId.Value;
                                        newService.Created = DateTime.UtcNow;
                                        //newService.OrderConceptId = concept.id;

                                        //newOrderConcept.Service.Add(newService);
                                        //newOrder.Service.Add(newService);
                                        break;
                                    case "Exámenes":
                                        Exam newExam = new Exam();
                                        newExam.PatientId = Order.PatientId;
                                        newExam.ClinicId = Order.ClinicId;
                                        if (concept.medicId.HasValue)
                                        {
                                            newExam.MedicId = concept.medicId.Value;
                                        }
                                        newExam.StatusId = StatusEnEnfermeria.id;
                                        newExam.ConceptId = concept.conceptId.Value;
                                     //   newExam.Updated = DateTime.UtcNow;
                                        //newExam.OrderConceptId = concept.id;

                                        //newOrderConcept.Exam.Add(newExam);
                                        //newOrder.Exam.Add(newExam);
                                        break;
                                    case "Consultas":



                                        Consult newConsult = new Consult();
                                        newConsult.PatientId = Order.PatientId;
                                        newConsult.ClinicId = Order.ClinicId;
                                        if (concept.medicId.HasValue)
                                        {
                                            newConsult.MedicId = concept.medicId.Value;
                                        }
                                        newConsult.StatusId = StatusEnEnfermeria.id;
                                        newConsult.ConceptId = concept.conceptId.Value;
                                        newConsult.Scheduled = concept.scheduled;
                                        newConsult.Updated = DateTime.UtcNow;
                                        //newConsult.OrderConceptId = concept.id;

                                        newOrderConcept.Consult.Add(newConsult);

                                        WaitingTimes waitingTimesData = new WaitingTimes();
                                        waitingTimesData.AttendingTimePatientRecS = dateTime;
                                        waitingTimesData.AttendingTimePatientRecF = DateTime.UtcNow;
                                        waitingTimesData.WaitingTimePatientRecInfS = DateTime.UtcNow;
                                        waitingTimesData.Created = DateTime.UtcNow;
                                        newConsult.WaitingTimes.Add(waitingTimesData);
                                        //waitingTimesData.Consult.Add(newConsult);

                                        WaitingTimeCalc aux = new WaitingTimeCalc();
                                        string StartProcess = TimeZoneInfo.ConvertTimeFromUtc(
                    new DateTime(waitingTimesData.AttendingTimePatientRecS.Value.Ticks, DateTimeKind.Utc),
                    TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                    ).ToString("HH:mm", new CultureInfo("es-MX"));
                                        string EndStageReception = TimeZoneInfo.ConvertTimeFromUtc(
                                    new DateTime(waitingTimesData.AttendingTimePatientRecF.Value.Ticks, DateTimeKind.Utc),
                                    TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                                    ).ToString("HH:mm", new CultureInfo("es-MX"));

                                        var TAPR = (waitingTimesData.AttendingTimePatientRecF - waitingTimesData.AttendingTimePatientRecS).Value.TotalMinutes;

                                        WaitingTimeCalc CalcDate = new WaitingTimeCalc();
                                        //CalcDate.WaitingTimeId = waitingTimeId;
                                        CalcDate.StartProcess = StartProcess;
                                        CalcDate.StartStageReception = StartProcess;
                                        CalcDate.EndStageReception = EndStageReception;
                                        CalcDate.TAPReception = Convert.ToInt32(TAPR);
                                        waitingTimesData.WaitingTimeCalc.Add(CalcDate);


                                        //newOrder.Consult.Add(newConsult);
                                        break;
                                }
                            }
                            //db.OrdersConcepts.Add(newOrderConcept);
                        }
                        #endregion
                        #region se guardan los conceptos de tipo "Productos"

                        /*como los productos tienen un trato diferente a los demas conceptos, se realiza un for por separado (tambien para tener mas ordenado el codigo)*/
                        foreach (var concept in Concepts.Where(c => c.categoryName == "Productos"))
                        {
                            Concept conceptDb = db.Concept.Where(c => c.id == concept.conceptId.Value).FirstOrDefault();
                            List<Stock> conceptInStock = conceptDb.Stock.Where(s => s.ClinicId == Order.ClinicId && s.InStock > 0).OrderBy(s => s.Created).ToList();

                            /*si se encuentran registros en la tabla y la suma de estos en el stock es mayor que la cantidad que se quiere vender, entonces se continua con la venta*/
                            if (conceptInStock.Count() > 0)
                            {
                                if (conceptInStock.Sum(s => s.InStock) >= concept.quantity)
                                {
                                    DiscountConcept dbDiscount =
                                           conceptDb.DiscountConcept
                                           .Where(d => !d.Discount.Deleted
                                           && d.Discount.Clinic.Any(c => c.id == Order.ClinicId)
                                           && d.Discount.StartDate <= DateTime.UtcNow
                                           && d.Discount.EndDate >= DateTime.UtcNow
                                           && d.Discount.DiscountType.Code == "Direct"
                                           )
                                           .FirstOrDefault();
                                    decimal realPrice = 0;
                                    if (dbDiscount != null)
                                    {
                                        realPrice = conceptDb.Price - (conceptDb.Price * dbDiscount.Percentage / 100);
                                    }
                                    else
                                    {
                                        realPrice = conceptDb.Price;
                                    }
                                    //Almacena la cantidad real de elementos vendidos
                                    int realQuantity = concept.quantity;

                                    foreach (var stocked in conceptInStock)
                                    {
                                        OrdersConcepts newOrderConcept = new OrdersConcepts();
                                        //newOrderConcept.OrderId = newOrder.id;
                                        newOrderConcept.ConceptId = concept.conceptId.Value;
                                        newOrderConcept.MedicId = null;
                                        newOrderConcept.ClinicId = concept.ClinicId;
                                        newOrderConcept.Quantity = concept.quantity;
                                        //newOrderConcept.Discount = concept.price - concept.total; //concept.discount;
                                        newOrderConcept.Discount = (stocked.Concept.Iva ? realPrice + (realPrice * ivaConfig / 100) : realPrice) - (concept.total / realQuantity);
                                        newOrderConcept.Price = realPrice;
                                        newOrderConcept.Iva = (stocked.Concept.Iva ? (realPrice * ivaConfig / 100) : 0);
                                        newOrderConcept.Cost = (stocked.Iva ? stocked.Cost + (stocked.Cost * (decimal)stocked.CurrIva / 100) : stocked.Cost);
                                        newOrderConcept.Scheduled = null;
                                        newOrderConcept.Medicname = concept.medicname;
                                        newOrderConcept.Decree = concept.decree;
                                        newOrderConcept.Created = DateTime.UtcNow;
                                        newOrderConcept.Updated = DateTime.UtcNow;
                                        newOrderConcept.Stockid = stocked.id;

                                        if (stocked.InStock >= concept.quantity)
                                        {
                                            stocked.InStock = stocked.InStock - concept.quantity;
                                            /*para saber el total, se hace el calculo de el precio por la cantidad*/
                                            //newOrderConcept.Total = realPrice * newOrderConcept.Quantity;
                                            newOrderConcept.Total = ((stocked.Concept.Iva ? realPrice + (realPrice * (decimal)stocked.CurrIva / 100) : realPrice) - newOrderConcept.Discount) * newOrderConcept.Quantity;
                                            newOrder.OrdersConcepts.Add(newOrderConcept);
                                            break;
                                        }
                                        else
                                        {
                                            concept.quantity = concept.quantity - stocked.InStock;
                                            newOrderConcept.Quantity = stocked.InStock;
                                            /*para saber el total, se hace el calculo de el precio por la cantidad*/
                                            //newOrderConcept.Total = (realPrice) * newOrderConcept.Quantity;
                                            newOrderConcept.Total = ((stocked.Concept.Iva ? realPrice + (realPrice * (decimal)stocked.CurrIva / 100) : realPrice) - newOrderConcept.Discount) * newOrderConcept.Quantity;
                                            stocked.InStock = 0;
                                            newOrder.OrdersConcepts.Add(newOrderConcept);
                                        }
                                    }
                                }
                                else
                                {
                                    EverythingInStock = false;
                                    NamesNotEnoughStock.Add(conceptDb.Name);
                                }
                            }
                            else
                            {
                                EverythingInStock = false;
                                NamesNotEnoughStock.Add(conceptDb.Name);
                            }
                        }
                        #endregion
                        #region se guardan los pagos
                        decimal totalPayed = 0;
                        decimal changeBack = 0;

                        /*revisamos que si tenga pagos*/
                        if (Payments != null && Payments.Count() > 0)
                        {
                            /*obtenemos el total pagado, que en este caso es la suma de los Amount de todos lso pagos que mando*/
                            totalPayed = Payments.Sum(p => p.Amount + p.Commission);

                            //if (Payments.Sum(p => p.Commission) > 0){
                            //    totalPayed = totalPayed + Payments.Sum(p => p.Commission);
                            //}
                            /*en caso de que los pagos realizados cubran el total de la orden, quiere decir que fue pagada por completo*/
                            /*calculamos si le deben regresar cambio*/


                            if (totalPayed >= newOrder.Total)
                            {
                                newOrder.Paid = true;
                                changeBack = totalPayed - newOrder.Total;
                            }

                            foreach (var payment in Payments)
                            {
                                Payment newPayment = new Payment();
                                /*en caso de que tenga pago en efectivo y se le tenga que regresar cambio, le restamos el cambio a el pago para que eso sea lo que se guarde*/
                                if (changeBack > 0 && payment.PaymentTypeName == "Efectivo")
                                {
                                    newPayment.Amount = payment.Amount - changeBack;
                                    /*una vez que le restamos el cambio al pago, volvemos el cambio a 0 para que no vuelva a entrar aqui*/
                                    changeBack = 0;
                                }
                                else
                                {
                                    newPayment.Amount = payment.Amount;
                                }
                                /*si el monto del pago es mayor a 0, entonces si se guarda*/
                                /*(esto es para los casos donde pusieran tipos de pagos y no le pusieran una cantidad, asi no se guardan pagos con monto 0)*/
                                if (newPayment.Amount > 0)
                                {
                                    newPayment.EmployeeId = Order.EmployeeId;
                                    newPayment.ClinicId = Order.ClinicId;
                                    newPayment.PaymentTypeId = payment.PaymentTypeId;
                                    newPayment.Commission = payment.Commission;
                                    newPayment.Created = DateTime.UtcNow;
                                    newPayment.Updated = DateTime.UtcNow;
                                    newPayment.NumOperation = payment.NumOperation;

                                    newOrder.Payment.Add(newPayment);
                                }
                            }
                        }
                        #endregion
                        #region se guardan las promociones que se aplicaron en la venta
                        if (Promotions != null)
                        {
                            foreach (var promotion in Promotions)
                            {
                                OrderPromotion newPOrderPromo = new OrderPromotion();
                                newPOrderPromo.AmountSaved = promotion.AmountSaved;
                                newPOrderPromo.ConceptId = promotion.ConceptId;
                                newPOrderPromo.TextPromotion = promotion.TextPromotion;

                                newOrder.OrderPromotion.Add(newPOrderPromo);

                                Discount discountdb = db.Discount.Where(s => s.id == promotion.PromotionId).FirstOrDefault();
                                List<DiscountConcept> discountconceptDb = db.DiscountConcept.Where(p => p.DiscountId == discountdb.id).ToList();

                                foreach (var discon in discountconceptDb)
                                {
                                    OrderPromotionDiscountApplied newPOrderPromoApp = new OrderPromotionDiscountApplied();

                                    newPOrderPromoApp.DiscountId = promotion.PromotionId;
                                    newPOrderPromoApp.ConceptId = discon.ConceptId;
                                    newPOrderPromoApp.TextPromotion = promotion.TextPromotion;
                                    newPOrderPromoApp.DiscountTypeId = discountdb.DiscountTypeId;

                                    newOrder.OrderPromotionDiscountApplied.Add(newPOrderPromoApp);
                                }
                            }

                            //foreach (var promotionApplied in PromotionsApplied)
                            //{
                            //    OrderPromotionDiscountApplied newPOrderPromoApp = new OrderPromotionDiscountApplied();
                            //    newPOrderPromoApp.AmountSaved = promotionApplied.AmountSaved;
                            //    newPOrderPromoApp.ConceptId = promotionApplied.ConceptId;
                            //    newPOrderPromoApp.TextPromotion = promotionApplied.TextPromotion;
                            //    newPOrderPromoApp.DiscountTypeId = promotionApplied.DiscountTypeId;

                            //    newOrder.OrderPromotionDiscountApplied.Add(newPOrderPromoApp);
                            //}
                        }

                        #endregion

                        db.SaveChanges();
                        result.integer_value = newOrder.id;
                        result.success = true;
                        //result.success = false;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Los siguientes productos no se encuentran en stock: " + String.Join(", ", NamesNotEnoughStock);
                    }

                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;

                    /*comos e hizo SaveChanges en la creacion de la orden, es necesario borrarla en caso de que algo salga mal*/
                    var failedOrder = db.Orders.Where(o => o.id == newOrderId).FirstOrDefault();
                    if (failedOrder != null)
                        db.Orders.Remove(failedOrder);
                }
            }
            return result;
        }

        public static OrderResult GetPendingOrders(DTParameterModel filter)
        {
            OrderResult result = new OrderResult();

            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Orders.Where(pt => !pt.Paid && !pt.CancelledPackages.Any());
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "id" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.id.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "Ticket" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Ticket.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "orderPackageAux.medic.personAux.fullName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q =>
                            (q.OrderPackage.FirstOrDefault().Employee.Person.Name
                            + " " +
                            q.OrderPackage.FirstOrDefault().Employee.Person.LastName)
                            .Contains(column.Search.Value));
                        }
                        if (column.Data == "orderPackageAux.patientAux.personAux.fullName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query
                                .Where(q => (q.Patient.Person.Name
                                + " " + q.Patient.Person.LastName)
                                .Contains(column.Search.Value));
                        }
                        if (column.Data == "Total" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Total.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "sCreated" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Created.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "orderPackageAux.clinicAux.name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.OrderPackage.FirstOrDefault().Clinic.Name.Contains(column.Search.Value));
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "id")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.id);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.id);
                            }
                        }
                        else if (orderColumn == "Ticket")
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
                        else if (orderColumn == "orderPackageAux.medic.personAux.fullName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q =>
                                (q.OrderPackage.FirstOrDefault().Employee.Person.Name
                                + " " +
                                q.OrderPackage.FirstOrDefault().Employee.Person.LastName));
                            }
                            else
                            {
                                query = query.OrderByDescending(q =>
                                (q.OrderPackage.FirstOrDefault().Employee.Person.Name
                                + " " +
                                q.OrderPackage.FirstOrDefault().Employee.Person.LastName));
                            }
                        }
                        else if (orderColumn == "orderPackageAux.patientAux.personAux.fullName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q =>
                                (q.Patient.Person.Name
                                + " " + q.Patient.Person.LastName));
                            }
                            else
                            {
                                query = query.OrderByDescending(q =>
                                (q.Patient.Person.Name
                                + " " + q.Patient.Person.LastName));
                            }
                        }
                        else if (orderColumn == "Total")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Total);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Total);
                            }
                        }
                        else if (orderColumn == "sCreated")
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
                        else if (orderColumn == "orderPackageAux.clinicAux.name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.OrderPackage.FirstOrDefault().Clinic.Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.OrderPackage.FirstOrDefault().Clinic.Name);
                            }
                        }
                    }

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (Orders Or in query.ToList())
                    {
                        OrderAux aux = new OrderAux();
                        OrderPackageAux package = new OrderPackageAux();
                        DataHelper.fill(aux, Or);
                        DataHelper.fill(package.patientAux, Or.Patient);
                        DataHelper.fill(package.patientAux.personAux, Or.Patient.Person);
                        if (Or.OrderPackage.Count > 0)
                        {
                            DataHelper.fill(package.packageAux, Or.OrderPackage.FirstOrDefault().Package);
                            DataHelper.fill(package.medic, Or.OrderPackage.FirstOrDefault().Employee);
                            DataHelper.fill(package.medic.personAux, Or.OrderPackage.FirstOrDefault().Employee.Person);
                            DataHelper.fill(package.clinicAux, Or.OrderPackage.FirstOrDefault().Clinic);
                        }
                        else if (Or.Consult.Count > 0)
                        {
                            DataHelper.fill(package.medic, Or.Consult.FirstOrDefault().Employee);
                            DataHelper.fill(package.medic.personAux, Or.Consult.FirstOrDefault().Employee.Person);
                            DataHelper.fill(package.clinicAux, Or.Consult.FirstOrDefault().Clinic);
                        }
                        DataHelper.fill(aux.clinicAux, Or.Clinic);
                        aux.orderPackageAux = package;
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

        public static vwSalesResult GetOrders(DTParameterModel filter, ReportFilter extraFilter)
        {
            vwSalesResult result = new vwSalesResult();
            if (!extraFilter.dateStart_.HasValue)
            {
                return result;
            }
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                SqlConnection conn = null;

                SqlDataReader rdr = null;
                try
                {
                    db.Database.CommandTimeout = 120;
                    conn = new SqlConnection(db.Database.Connection.ConnectionString);
                    conn.Open();
                    SqlCommand command = new SqlCommand("dbo.sp_getSales", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    if (extraFilter.dateStart_.HasValue)
                    {
                        command.Parameters.Add(new SqlParameter("DateBegin", extraFilter.dateStart_.Value.ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                    if (extraFilter.dateEnd_.HasValue)
                    {
                        command.Parameters.Add(new SqlParameter("DateEnd", extraFilter.dateEnd_.Value.ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                    #region filtros
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "Ticket" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            command.Parameters.Add(new SqlParameter("Ticket", column.Search.Value));
                        }
                        if (column.Data == "ClinicName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            command.Parameters.Add(new SqlParameter("Clinic", column.Search.Value));
                        }
                        if (column.Data == "EmployeeFullName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            command.Parameters.Add(new SqlParameter("EmployeeName", column.Search.Value.ToUpper())).SqlDbType = SqlDbType.NVarChar;
                        }
                        if (column.Data == "PatientFullName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            command.Parameters.Add(new SqlParameter("PatientName", column.Search.Value.ToUpper())).SqlDbType = SqlDbType.NVarChar;
                        }
                        if (column.Data == "Amount" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            command.Parameters.Add(new SqlParameter("Total", column.Search.Value));
                        }
                        if (column.Data == "Discount" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            command.Parameters.Add(new SqlParameter("Discount", column.Search.Value));
                        }
                        if (column.Data == "IsCanceled" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            bool containsAny = "SI".Contains(column.Search.Value.ToUpper()) || "NO".Contains(column.Search.Value.ToUpper());
                            bool containsTrue = "SI".Contains(column.Search.Value.ToUpper());
                            if (containsAny)
                            {
                                command.Parameters.Add(new SqlParameter("Cancelled", containsTrue ? "1" : "0"));
                            }
                            else
                            {
                                command.Parameters.Add(new SqlParameter("Cancelled", "2"));
                            }
                        }
                        if (column.Data == "Paid" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            bool containsAny = "SI".Contains(column.Search.Value.ToUpper()) || "NO".Contains(column.Search.Value.ToUpper());
                            bool containsTrue = "SI".Contains(column.Search.Value.ToUpper());
                            if (containsAny)
                            {
                                command.Parameters.Add(new SqlParameter("Paid", containsTrue ? "1" : "0"));
                            }
                            else
                            {
                                command.Parameters.Add(new SqlParameter("Paid", "2"));
                            }
                        }
                    }
                    #endregion


                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        string order_param = "";
                        if (orderColumn == "Ticket")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "OrderTicket ASC";
                            }
                            else
                            {
                                order_param = "OrderTicket DESC";
                            }
                        }
                        else if (orderColumn == "EmployeeFullName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "employeefullname ASC";
                            }
                            else
                            {
                                order_param = "employeefullname DESC";
                            }
                        }
                        else if (orderColumn == "ClinicName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "clinicname ASC";
                            }
                            else
                            {
                                order_param = "clinicname DESC";
                            }
                        }
                        else if (orderColumn == "PatientFullName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "patientfullname ASC";
                            }
                            else
                            {
                                order_param = "patientfullname DESC";
                            }
                        }
                        else if (orderColumn == "Amount")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "amount ASC";
                            }
                            else
                            {
                                order_param = "amount DESC";
                            }
                        }
                        else if (orderColumn == "Discount")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "discount ASC";
                            }
                            else
                            {
                                order_param = "discount DESC";
                            }
                        }
                        else if (orderColumn == "IsCanceled")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "iscanceled ASC";
                            }
                            else
                            {
                                order_param = "iscanceled DESC";
                            }
                        }
                        else if (orderColumn == "Paid")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "paid ASC";
                            }
                            else
                            {
                                order_param = "paid DESC";
                            }
                        }
                        else if (orderColumn == "createdString")
                        {

                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "created ASC";
                            }
                            else
                            {
                                order_param = "created DESC";
                            }
                        }
                        command.Parameters.Add(new SqlParameter("Order_column", order_param));
                    }
                    #endregion
                    //query = query.Skip(filter.Start).Take(filter.Length);
                    command.Parameters.Add(new SqlParameter("startRowIndex", filter.Start));
                    command.Parameters.Add(new SqlParameter("pageSize", filter.Length));

                    rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        vwSalesAux aux = new vwSalesAux()
                        {
                            OrderId = Convert.ToInt32(rdr["orderid"]),
                            OrderTicket = Convert.ToInt32(rdr["orderticket"] == DBNull.Value ? 0 : rdr["orderticket"]),
                            Ticket = rdr["ticket"].ToString(),
                            ClinicId = Convert.ToInt32(rdr["clinicId"]),
                            ClinicName = rdr["clinicname"].ToString(),
                            EmployeeFullName = rdr["employeefullname"].ToString(),
                            PatientFullName = rdr["patientfullname"].ToString(),
                            Amount = Convert.ToDecimal(rdr["amount"]),
                            Paid = Convert.ToBoolean(rdr["paid"]),
                            IsCanceled = Convert.ToBoolean(rdr["iscanceled"]),
                            PaymentCreated = Convert.ToDateTime(rdr["created"])
                        };
                        result.data_list.Add(aux);
                    }
                    rdr.Close();
                    result.total.value = Convert.ToInt32(command.Parameters["@totalCount"].Value);
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
                finally
                {
                    if (conn != null && conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (rdr != null && !rdr.IsClosed)
                    {
                        rdr.Close();
                    }
                }
            }
            return result;
        }
        public static vwSalesResult GetOrders(DTParameterModel filter, List<int> clinicIds, ReportFilter extraFilter)
        {
            vwSalesResult result = new vwSalesResult();
            if (!extraFilter.dateStart_.HasValue)
            {
                return result;
            }
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    
                    SqlConnection conn = null;

                    SqlDataReader rdr = null;

                    db.Database.CommandTimeout = 120;
                    conn = new SqlConnection(db.Database.Connection.ConnectionString);
                    conn.Open();
                    SqlCommand command = new SqlCommand("dbo.sp_getSales", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    if (clinicIds.Count > 0)
                    {
                        command.Parameters.Add(new SqlParameter("clinicIds", "(" + string.Join(",", clinicIds) + ")"));
                    }
                    if (extraFilter.dateStart_.HasValue)
                    {
                        command.Parameters.Add(new SqlParameter("DateBegin", extraFilter.dateStart_.Value.ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                    if (extraFilter.dateEnd_.HasValue)
                    {
                        command.Parameters.Add(new SqlParameter("DateEnd", extraFilter.dateEnd_.Value.ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                    #region filtros
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "Ticket" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            command.Parameters.Add(new SqlParameter("Ticket", column.Search.Value));
                        }
                        if (column.Data == "ClinicName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            command.Parameters.Add(new SqlParameter("Clinic", column.Search.Value));
                        }
                        if (column.Data == "EmployeeFullName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            command.Parameters.Add(new SqlParameter("EmployeeName", column.Search.Value.ToUpper())).SqlDbType = SqlDbType.NVarChar;
                        }
                        if (column.Data == "PatientFullName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            command.Parameters.Add(new SqlParameter("PatientName", column.Search.Value.ToUpper())).SqlDbType = SqlDbType.NVarChar;
                        }
                        if (column.Data == "Amount" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            command.Parameters.Add(new SqlParameter("Total", column.Search.Value));
                        }
                        if (column.Data == "Discount" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            command.Parameters.Add(new SqlParameter("Discount", column.Search.Value));
                        }
                        if (column.Data == "IsCanceled" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            bool containsAny = "SI".Contains(column.Search.Value.ToUpper()) || "NO".Contains(column.Search.Value.ToUpper());
                            bool containsTrue = "SI".Contains(column.Search.Value.ToUpper());
                            if (containsAny)
                            {
                                command.Parameters.Add(new SqlParameter("Cancelled", containsTrue ? "1" : "0"));
                            }
                            else
                            {
                                command.Parameters.Add(new SqlParameter("Cancelled", "2"));
                            }
                        }
                        if (column.Data == "Paid" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            bool containsAny = "SI".Contains(column.Search.Value.ToUpper()) || "NO".Contains(column.Search.Value.ToUpper());
                            bool containsTrue = "SI".Contains(column.Search.Value.ToUpper());
                            if (containsAny)
                            {
                                command.Parameters.Add(new SqlParameter("Paid", containsTrue ? "1" : "0"));
                            }
                            else
                            {
                                command.Parameters.Add(new SqlParameter("Paid", "2"));
                            }
                        }
                    }
                    #endregion


                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        string order_param = "";
                        if (orderColumn == "Ticket")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "OrderTicket ASC";
                            }
                            else
                            {
                                order_param = "OrderTicket DESC";
                            }
                        }
                        else if (orderColumn == "EmployeeFullName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "employeefullname ASC";
                            }
                            else
                            {
                                order_param = "employeefullname DESC";
                            }
                        }
                        else if (orderColumn == "ClinicName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "clinicname ASC";
                            }
                            else
                            {
                                order_param = "clinicname DESC";
                            }
                        }
                        else if (orderColumn == "PatientFullName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "patientfullname ASC";
                            }
                            else
                            {
                                order_param = "patientfullname DESC";
                            }
                        }
                        else if (orderColumn == "Amount")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "amount ASC";
                            }
                            else
                            {
                                order_param = "amount DESC";
                            }
                        }
                        else if (orderColumn == "Discount")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "discount ASC";
                            }
                            else
                            {
                                order_param = "discount DESC";
                            }
                        }
                        else if (orderColumn == "IsCanceled")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "iscanceled ASC";
                            }
                            else
                            {
                                order_param = "iscanceled DESC";
                            }
                        }
                        else if (orderColumn == "Paid")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "paid ASC";
                            }
                            else
                            {
                                order_param = "paid DESC";
                            }
                        }
                        else if (orderColumn == "createdString")
                        {

                            if (order.ToUpper() == "ASC")
                            {
                                order_param = "created ASC";
                            }
                            else
                            {
                                order_param = "created DESC";
                            }
                        }
                        command.Parameters.Add(new SqlParameter("Order_column", order_param));
                    }
                    #endregion
                    //query = query.Skip(filter.Start).Take(filter.Length);
                    command.Parameters.Add(new SqlParameter("startRowIndex", filter.Start));
                    command.Parameters.Add(new SqlParameter("pageSize", filter.Length));

                    rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        vwSalesAux aux = new vwSalesAux()
                        {
                            OrderId = Convert.ToInt32(rdr["orderid"]),
                            OrderTicket = Convert.ToInt32(rdr["orderticket"] == DBNull.Value ? 0 : rdr["orderticket"]),
                            Ticket = rdr["ticket"].ToString(),
                            ClinicId = Convert.ToInt32(rdr["clinicId"]),
                            ClinicName = rdr["clinicname"].ToString(),
                            EmployeeFullName = rdr["employeefullname"].ToString(),
                            PatientFullName = rdr["patientfullname"].ToString(),
                            Amount = Convert.ToDecimal(rdr["amount"]),
                            Paid = Convert.ToBoolean(rdr["paid"]),
                            IsCanceled = Convert.ToBoolean(rdr["iscanceled"]),
                            PaymentCreated = Convert.ToDateTime(rdr["created"])
                        };
                        result.data_list.Add(aux);
                    }
                    rdr.Close();
                    result.total.value = Convert.ToInt32(command.Parameters["@totalCount"].Value);
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

        public static CancellationsResult GetCancellations(DTParameterModel filter)
        {
            CancellationsResult result = new CancellationsResult();

            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.vwCancelledTickets.AsQueryable();
                    #region filtros
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "Ticket" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Ticket.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "total" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Total.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "created" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Created.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "employeeName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.EmployeeName.ToString().Contains(column.Search.Value));
                        }

                    }
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "Ticket")
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
                        if (orderColumn == "total")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Total);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Total);
                            }
                        }
                        if (orderColumn == "created")
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
                        if (orderColumn == "stringDateCancelled")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.DateCancelled);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.DateCancelled);
                            }
                        }
                    }
                    #endregion

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (vwCancelledTickets Or in query.ToList())
                    {
                        CancelationsAux aux = new CancelationsAux();
                        DataHelper.fill(aux, Or);
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
        public static CancellationsResult GetCancellations(DTParameterModel filter, List<int> clinicIds)
        {
            CancellationsResult result = new CancellationsResult();

            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.vwCancelledTickets.AsQueryable();
                    if (clinicIds.Count > 0)
                    {
                        query = query.Where(q => clinicIds.Contains(q.ClinicId));
                    }
                    #region filtros
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "Ticket" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Ticket.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "total" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Total.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "created" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Created.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "employeeName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.EmployeeName.ToString().Contains(column.Search.Value));
                        }

                    }
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "Ticket")
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
                        if (orderColumn == "total")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Total);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Total);
                            }
                        }
                        if (orderColumn == "created")
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
                        if (orderColumn == "stringDateCancelled")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.DateCancelled);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.DateCancelled);
                            }
                        }
                    }
                    #endregion

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (vwCancelledTickets Or in query.ToList())
                    {
                        CancelationsAux aux = new CancelationsAux();
                        DataHelper.fill(aux, Or);
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

        public static Order_ConceptResult GetRestricted(DTParameterModel filter, ReportFilter extraFilter)
        {
            Order_ConceptResult result = new Order_ConceptResult();
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    //var query = db.Product.Where(p => p.LicenseRequired).SelectMany(p => p.Concept.OrdersConcepts);
                    //#region filtros
                    //if (extraFilter.dateStart_.HasValue)
                    //{
                    //    query = query.Where(g => g.Created >= extraFilter.dateStart_.Value);
                    //}
                    //if (extraFilter.dateEnd_.HasValue)
                    //{
                    //    var date = extraFilter.dateEnd_.Value;
                    //    query = query.Where(g => g.Created <= date);
                    //}
                    //#endregion
                    //#region orden
                    //if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    //{
                    //    if (orderColumn == "medicname")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.Medicname);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.Medicname);
                    //        }
                    //    }
                    //    if (orderColumn == "decree")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.Decree);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.Decree);
                    //        }
                    //    }
                    //    if (orderColumn == "ConceptAux.name")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.Concept.Name);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.Concept.Name);
                    //        }
                    //    }
                    //    if (orderColumn == "quantity")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.Quantity);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.Quantity);
                    //        }
                    //    }
                    //    if (orderColumn == "createdString")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.Created);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.Created);
                    //        }
                    //    }
                    //}
                    //#endregion
                    //result.total.value = query.Count();

                    //query = query.Skip(filter.Start).Take(filter.Length);
                    //foreach (OrdersConcepts oc in query.ToList())
                    //{
                    //    OrderConceptAux aux = new OrderConceptAux();
                    //    DataHelper.fill(aux, oc);
                    //    DataHelper.fill(aux.ConceptAux, oc.Concept);
                    //    result.data_list.Add(aux);
                    //}
                    //result.success = true;
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
        public static Order_ConceptResult GetRestricted(DTParameterModel filter, ReportFilter extraFilter, List<int> clinicIds)
        {
            Order_ConceptResult result = new Order_ConceptResult();
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    //var query = db.Product.Where(p => p.LicenseRequired).SelectMany(p => p.Concept.OrdersConcepts);
                    //if (clinicIds.Count > 0)
                    //{
                    //    query = query.Where(q => clinicIds.Contains(q.ClinicId));
                    //}
                    //#region filtros
                    //if (extraFilter.dateStart_.HasValue)
                    //{
                    //    query = query.Where(g => g.Created >= extraFilter.dateStart_.Value);
                    //}
                    //if (extraFilter.dateEnd_.HasValue)
                    //{
                    //    var date = extraFilter.dateEnd_.Value;
                    //    query = query.Where(g => g.Created <= date);
                    //}
                    //#endregion
                    //#region orden
                    //if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    //{
                    //    if (orderColumn == "medicname")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.Medicname);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.Medicname);
                    //        }
                    //    }
                    //    if (orderColumn == "decree")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.Decree);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.Decree);
                    //        }
                    //    }
                    //    if (orderColumn == "ConceptAux.name")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.Concept.Name);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.Concept.Name);
                    //        }
                    //    }
                    //    if (orderColumn == "quantity")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.Quantity);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.Quantity);
                    //        }
                    //    }
                    //    if (orderColumn == "createdString")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.Created);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.Created);
                    //        }
                    //    }
                    //}
                    //#endregion
                    //result.total.value = query.Count();

                    //query = query.Skip(filter.Start).Take(filter.Length);
                    //foreach (OrdersConcepts oc in query.ToList())
                    //{
                    //    OrderConceptAux aux = new OrderConceptAux();
                    //    DataHelper.fill(aux, oc);
                    //    DataHelper.fill(aux.ConceptAux, oc.Concept);
                    //    result.data_list.Add(aux);
                    //}
                    //result.success = true;
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

        public static OrderResult getNewOrderDataPack(int idTicket, int idClinic, OrderAux orderAux)
        {
            OrderResult result = new OrderResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var ordersDb = db.Orders.Where(o => o.Ticket == idTicket).FirstOrDefault();
                    if (ordersDb != null)
                    {
                        DataHelper.fill(result.data, ordersDb);

                        if (ordersDb != null)
                        {
                            DataHelper.fill(result.data.patientAux, ordersDb.Patient);
                            DataHelper.fill(result.data.patientAux.personAux, ordersDb.Patient.Person);
                        }

                        if (ordersDb.Employee1 != null)
                        {
                            DataHelper.fill(result.data.EmployeeCancel, ordersDb.Employee1);
                        }
                        foreach (var PackageItem in ordersDb.OrderPackage)
                        {
                            if (ordersDb.CancelledPackages.Any(p => p.OrderPackageId == PackageItem.Id))
                            {
                                result.message = "El paquete esta cancelado";
                            }
                            else
                            {
                                foreach (var order in orderAux.orderPackageAuxList)
                                {
                                    if (order.packageSelected)
                                    {
                                        OrderPackageAux aux = new OrderPackageAux();
                                        DataHelper.fill(aux, PackageItem);
                                        DataHelper.fill(aux.packageAux, PackageItem.Package);
                                        DataHelper.fill(aux.packageAux.categoryAux, PackageItem.Package.Category);
                                        DataHelper.fill(aux.clinicAux, PackageItem.Clinic);
                                        DataHelper.fill(aux.medic, PackageItem.Employee);
                                        result.data.orderPackageAuxList.Add(aux);
                                    }
                                }
                            }
                        }


                        foreach (var PaymentItem in ordersDb.Payment)
                        {
                            PaymentAux aux = new PaymentAux();
                            DataHelper.fill(aux, PaymentItem);
                            DataHelper.fill(aux.PaymentTypeAux, PaymentItem.PaymentType);
                            result.data.paymentAux.Add(aux);
                        }

                        foreach (var OrderPromotionItem in ordersDb.OrderPromotion)
                        {
                            OrderPromotionAux aux = new OrderPromotionAux();
                            DataHelper.fill(aux, OrderPromotionItem);
                            result.data.Promotions.Add(aux);
                        }
                        result.success = true;
                    }
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

        public static OrderResult verifyPackageCancelled(int idTicket, int idClinic, OrderAux orderAux)
        {
            OrderResult result = new OrderResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var ordersDb = db.Orders.Where(o => o.Ticket == idTicket && o.ClinicId == idClinic).FirstOrDefault();
                    if (ordersDb != null)
                    {
                        DataHelper.fill(result.data, ordersDb);
                        var cancelledpack = db.CancelledPackages.Where(cp => cp.OldOrder == ordersDb.id).FirstOrDefault();
                        if (cancelledpack != null)
                        {
                            result.message = "El ticket no tiene paquetes por cancelar.";
                        }
                        else
                        {
                            result.success = true;
                            result = getPackageByTicket(idTicket, idClinic, new OrderAux());
                        }
                    }
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

        public static OrderResult getPackageByTicket(int idTicket, int idClinic, OrderAux orderAux)
        {
            OrderResult result = new OrderResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var ordersDb = db.Orders.Where(o => o.Ticket == idTicket && o.ClinicId == idClinic).FirstOrDefault();
                    if (ordersDb != null)
                    {
                        DataHelper.fill(result.data, ordersDb);

                        if (ordersDb != null)
                        {
                            DataHelper.fill(result.data.patientAux, ordersDb.Patient);
                            DataHelper.fill(result.data.patientAux.personAux, ordersDb.Patient.Person);
                            DataHelper.fill(result.data.clinicAux, ordersDb.Clinic);
                            DataHelper.fill(result.data.clinicAux.addressAux, ordersDb.Clinic.Address);
                        }

                        if (ordersDb.Employee1 != null)
                        {
                            DataHelper.fill(result.data.EmployeeCancel, ordersDb.Employee1);
                        }
                        foreach (var PackageItem in ordersDb.OrderPackage)
                        {

                            if (ordersDb.CancelledPackages.Any(p => p.OrderPackageId == PackageItem.Id))
                            {
                                result.message = "El paquete esta cancelado";
                            }
                            else
                            {
                                OrderPackageAux aux = new OrderPackageAux();
                                DataHelper.fill(aux, PackageItem);
                                DataHelper.fill(aux.packageAux, PackageItem.Package);
                                DataHelper.fill(aux.packageAux.categoryAux, PackageItem.Package.Category);
                                DataHelper.fill(aux.clinicAux, PackageItem.Clinic);
                                DataHelper.fill(aux.clinicAux.addressAux, PackageItem.Clinic.Address);
                                DataHelper.fill(aux.medic, PackageItem.Employee);
                                result.data.orderPackageAuxList.Add(aux);
                            }
                        }
                        foreach (var PaymentItem in ordersDb.Payment)
                        {
                            PaymentAux aux = new PaymentAux();
                            DataHelper.fill(aux, PaymentItem);
                            DataHelper.fill(aux.PaymentTypeAux, PaymentItem.PaymentType);
                            result.data.paymentAux.Add(aux);
                        }

                        foreach (var OrderPromotionItem in ordersDb.OrderPromotion)
                        {
                            OrderPromotionAux aux = new OrderPromotionAux();
                            DataHelper.fill(aux, OrderPromotionItem);
                            result.data.Promotions.Add(aux);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.message = "No se encontro la orden.";
                    }
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


        public static OrderResult cancelPackage(int idOrder, int EmployeeCancelId, OrderAux orderAux)
        {
            OrderResult result = new OrderResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var orderDb = db.Orders.Where(o => o.id == idOrder).FirstOrDefault();

                    if (orderDb != null)
                    {
                        foreach (var order in orderAux.orderPackageAuxList)
                        {
                            if (order.packageSelected)
                            {
                                CancelledPackages cp = db.CancelledPackages.Create();
                                cp.OldOrder = idOrder;
                                cp.OrderPackageId = order.id;
                                cp.Reason = order.reason;
                                cp.Created = DateTime.UtcNow;
                                cp.PackageId = order.packageAux.id;
                                cp.ClinicId = orderDb.ClinicId;
                                cp.IsCanceled = true;
                                cp.Refund = orderAux.paymentAmmount;

                                db.CancelledPackages.Add(cp);
                            }
                        }

                        db.SaveChanges();
                        result.success = true;
                        result = getPackageByTicket(orderAux.Ticket.Value, orderAux.ClinicId, new OrderAux());
                    }
                    else
                    {
                        result.message = "No se encontro la orden.";
                    }
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

        public static GenericResult SaveOrderPackage(OrderAux Order, List<OrderConceptAux> Concepts, List<PaymentAux> Payments, OrderAux DataOldOrder)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                Orders newOrder = new Orders();
                int newOrderId = 0;
                try
                {
                    if (Order != null)
                    {
                        newOrder.EmployeeId = Order.EmployeeId;
                        newOrder.PatientId = Order.PatientId;
                        newOrder.Discount = Order.Discount;
                        newOrder.Total = Order.Total;
                        newOrder.Iva = Order.Iva;
                        newOrder.Donation = Order.Donation;
                        newOrder.Rounding = Order.Rounding;
                        newOrder.Paid = Order.Paid;
                        newOrder.Pack = Order.Pack;
                        newOrder.ClinicId = Order.ClinicId;
                        newOrder.Created = DateTime.UtcNow;
                        newOrder.Updated = DateTime.UtcNow;
                        db.Orders.Add(newOrder);

                        foreach (var concept in Concepts.Where(c => c.categoryName != "Productos"))
                        {
                            if (concept.categoryName == "Paquetes")
                            {
                                Package packageDb = db.Package.Where(p => p.id == concept.conceptId.Value).FirstOrDefault();
                                OrderPackage newOrderPackage = new OrderPackage();

                                newOrderPackage.PackageId = concept.conceptId.Value;
                                if (concept.medicId.HasValue)
                                {
                                    newOrderPackage.MedicId = concept.medicId.Value;
                                }
                                newOrderPackage.ClinicId = concept.ClinicId;
                                newOrderPackage.Scheduled = concept.scheduled;
                                newOrderPackage.Medicname = concept.medicname;
                                newOrderPackage.Created = DateTime.UtcNow;
                                newOrderPackage.Updated = DateTime.UtcNow;
                                newOrder.OrderPackage.Add(newOrderPackage);
                            }
                        }
                        decimal totalPayed = 0;
                        decimal changeBack = 0;

                        if (Payments != null && Payments.Count() > 0)
                        {
                            totalPayed = Payments.Sum(p => p.Amount + p.Commission);

                            if (totalPayed >= newOrder.Total)
                            {
                                newOrder.Paid = true;
                                changeBack = totalPayed - newOrder.Total;
                            }

                            foreach (var payment in Payments)
                            {
                                Payment newPayment = new Payment();
                                if (changeBack > 0 && payment.PaymentTypeName == "Efectivo")
                                {
                                    newPayment.Amount = payment.Amount - changeBack;
                                    changeBack = 0;
                                }
                                else
                                {
                                    newPayment.Amount = payment.Amount;
                                }

                                if (newPayment.Amount > 0)
                                {
                                    newPayment.EmployeeId = Order.EmployeeId;
                                    newPayment.ClinicId = Order.ClinicId;
                                    newPayment.PaymentTypeId = payment.PaymentTypeId;
                                    newPayment.Commission = payment.Commission;
                                    newPayment.Created = DateTime.UtcNow;
                                    newPayment.Updated = DateTime.UtcNow;
                                    newPayment.NumOperation = payment.NumOperation;

                                    newOrder.Payment.Add(newPayment);
                                }
                            }
                        }
                        db.SaveChanges();
                        result.integer_value = newOrder.id;
                        result.success = true;
                        foreach (var paymentid in Payments)
                        {
                            var cp = db.CancelledPackages.Where(c => c.OldOrder == paymentid.OrderId);

                            foreach (var cancelpack in cp)
                            {
                                cancelpack.NewOrder = newOrder.id;
                            }
                            db.SaveChanges();
                            result.success = true;
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Error paquete";
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;

                    var failedOrder = db.Orders.Where(o => o.id == newOrderId).FirstOrDefault();
                    if (failedOrder != null)
                        db.Orders.Remove(failedOrder);
                }
            }
            return result;
        }
    }
}
