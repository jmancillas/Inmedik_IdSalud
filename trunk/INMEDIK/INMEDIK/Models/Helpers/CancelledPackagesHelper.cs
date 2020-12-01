using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class CancelledPackagesAux
    {
        public int id { get; set; }
        public int orderPackageId { get; set; }
        public int packageId { get; set; }
        public int userId { get; set; }
        public int newOrder { get; set; }
        public int oldOrder { get; set; }
        public int newTicket { get; set; }
        public int oldTicket { get; set; }
        public string reason { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public int ClinicId { get; set; }
        public bool isCanceled { get; set; }
        public decimal refund { get; set; }


        public UserAux userAux { get; set; }
        public CancelationsAux cancelationsAux { get; set; }
        public EmployeeAux employeeAux { get; set; }
        public EmployeeAux EmployeeCancel { get; set; }
        public ClinicAux clinicAux { get; set; }
        public List<OrderPackageAux> orderPackageAux { get; set; }
        public List<PaymentAux> paymentAux { get; set; }
        public List<OrderPromotionAux> Promotions { get; set; }
        public List<OrderConceptAux> OrderConceptAux { get; set; }
        public List<PackageConceptAux> PackageConceptAux { get; set; }
        public List<PackageAux> packageAux { get; set; }

        public string sCreated
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(created.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }


        public CancelledPackagesAux()
        {
            userAux = new UserAux();
            cancelationsAux = new CancelationsAux();
            employeeAux = new EmployeeAux();
            EmployeeCancel = new EmployeeAux();
            clinicAux = new ClinicAux();
            paymentAux = new List<PaymentAux>();
            Promotions = new List<OrderPromotionAux>();
            OrderConceptAux = new List<OrderConceptAux>();
            PackageConceptAux = new List<PackageConceptAux>();
            orderPackageAux = new List<OrderPackageAux>();
            packageAux = new List<PackageAux>();
        }
    }


    public class CancelledPackageResult : Result
    {
        public CancelledPackagesAux data { get; set; }
        public List<CancelledPackagesAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public CancelledPackageResult()
        {
            this.data = new CancelledPackagesAux();
            this.data_list = new List<CancelledPackagesAux>();
            this.total = new NumericResult();
        }
    }

    public class CancelledPackagesHelper
    {


        public static CancelledPackageResult GetCancelledPackages(DTParameterModel filter)
        {
            CancelledPackageResult result = new CancelledPackageResult();
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
                    IQueryable<vwCancelledPackages> query = db.vwCancelledPackages;
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "newTicket" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.newticket.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "oldTicket" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.oldticket.ToString().Contains(column.Search.Value));
                        }
                        
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "newTicket")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.NewOrder);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.NewOrder);
                            }
                        }
                        if (orderColumn == "oldTicket")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.OldOrder);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.OldOrder);
                            }
                        }
                        if ( orderColumn == "sCreated")
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

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (vwCancelledPackages cp in query.ToList())
                    {
                        CancelledPackagesAux aux = new CancelledPackagesAux();
                        DataHelper.fill(aux, cp);

                        result.data_list.Add(aux);

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

        public static CancelledPackageResult getPackageByTicket(int idTicket, int idClinic)
        {
            CancelledPackageResult result = new CancelledPackageResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var ordersDb = db.Orders.Where(o => o.Ticket == idTicket && o.ClinicId == idClinic).FirstOrDefault();
                    if (ordersDb != null)
                    {
                        DataHelper.fill(result.data, ordersDb);

                        if (ordersDb.Employee1 != null)
                        {
                            DataHelper.fill(result.data.EmployeeCancel, ordersDb.Employee1);
                        }
                        foreach (var PackageItem in ordersDb.OrderPackage)
                        {
                            OrderPackageAux aux = new OrderPackageAux();
                            DataHelper.fill(aux, PackageItem);
                            DataHelper.fill(aux.packageAux, PackageItem.Package);
                            DataHelper.fill(aux.packageAux.categoryAux, PackageItem.Package.Category);
                            DataHelper.fill(aux.clinicAux, PackageItem.Clinic);
                            DataHelper.fill(aux.medic, PackageItem.Employee);
                            result.data.orderPackageAux.Add(aux);
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

        public static Result cancelPackage(int idOrder, int packageSelected)
        {
            CancelledPackageResult result = new CancelledPackageResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var orderDb = db.Orders.Where(o => o.id == idOrder).FirstOrDefault();
                    var packageDb = db.OrderPackage.Where(o => o.OrderId == idOrder);
                    if (packageDb != null)
                    {

                        if (packageSelected == 0)
                        {
                           foreach (var cp in orderDb.CancelledPackages)
                            {
                                if (cp != null)
                                {
                                    //cp.OrderPackageId =
                                    cp.NewOrder = idOrder;
                                    cp.Created = DateTime.UtcNow;
                                    cp.Updated = DateTime.UtcNow;
                                }
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
    }

}   