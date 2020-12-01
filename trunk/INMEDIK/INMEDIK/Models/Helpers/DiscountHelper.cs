using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class DiscountAux
    {
        public int id { get; set; }
        public int discountTypeId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string sStartDate
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                  new DateTime(startDate.Ticks, DateTimeKind.Utc),
                  TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                  ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public string sEndDate
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                  new DateTime(endDate.Ticks, DateTimeKind.Utc),
                  TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                  ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string sCreated
        {
            get
            {
                return Created.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(Created.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"))
                : "";
            }
        }
        public string sUpdated
        {
            get
            {
                return Updated.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(Updated.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"))
                : "";
            }
        }
        public bool deleted { get; set; }

        public List<ClinicAux> clinicList { get; set; }
        public DiscountTypeAux discountTypeAux { get; set; }
        public List<DiscountConceptAux> discountConceptAux { get; set; }

        public DiscountAux()
        {
            this.clinicList = new List<ClinicAux>();
            this.discountTypeAux = new DiscountTypeAux();
            this.discountConceptAux = new List<DiscountConceptAux>();
        }

        public void dbfill(ref Discount dbDiscount)
        {
            dbDiscount.DiscountTypeId = this.discountTypeId;
            dbDiscount.Name = this.name;
            dbDiscount.Description = this.description;
            dbDiscount.StartDate = this.startDate;
            dbDiscount.EndDate = this.endDate;
        }

    }
    public class DiscountResult : Result
    {
        public DiscountAux data { get; set; }
        public List<DiscountAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public DiscountResult()
        {
            this.data = new DiscountAux();
            this.data_list = new List<DiscountAux>();
            this.total = new NumericResult();
        }
    }

    public class DiscountTypeAux
    {
        public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
    }

    public class DiscountTypeResult : Result
    {
        public DiscountTypeAux data { get; set; }
        public List<DiscountTypeAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public DiscountTypeResult()
        {
            this.data = new DiscountTypeAux();
            this.data_list = new List<DiscountTypeAux>();
            this.total = new NumericResult();
        }
    }

    public class DiscountConceptAux
    {
        public int id { get; set; }
        public int discountId { get; set; }
        public int conceptId { get; set; }
        public ConceptAux conceptAux { get; set; }
        public decimal percentage { get; set; }

        public DiscountConceptAux()
        {
            this.conceptAux = new ConceptAux();
        }
    }
    public class vwDiscountClinicResult : Result
    {
        public vwDiscountClinicAux data { get; set; }
        public List<vwDiscountClinicAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public vwDiscountClinicResult()
        {
            this.data = new vwDiscountClinicAux();
            this.data_list = new List<vwDiscountClinicAux>();
            this.total = new NumericResult();
        }
    }

    public class vwDiscountClinicAux
    {
        public int id { get; set; }
        public string DiscountName { get; set; }
        public string DiscountDescription { get; set; }
        public string DiscountTypeDescription { get; set; }
        public string ClinicName { get; set; }
        public string Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Created { get; set; }
        public string sStartDate
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                  new DateTime(StartDate.Ticks, DateTimeKind.Utc),
                  TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                  ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public string sEndDate
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                  new DateTime(EndDate.Ticks, DateTimeKind.Utc),
                  TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                  ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
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

        public bool Deleted { get; set; }

    }

    public class DiscountHelper
    {
        public static vwDiscountClinicResult GetDiscounts(DTParameterModel filter)
        {
            vwDiscountClinicResult result = new vwDiscountClinicResult();
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
                    IQueryable<vwDiscountClinic> query = db.vwDiscountClinic.Where(d => d.Deleted == false);
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "DiscountName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.DiscountName.Contains(column.Search.Value));
                        }
                        if (column.Data == "DiscountDescription" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.DiscountDescription.Contains(column.Search.Value));
                        }
                        if (column.Data == "DiscountTypeDescription" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.DiscountTypeDescription.Contains(column.Search.Value));
                        }
                        if (column.Data == "ClinicName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.ClinicName.Contains(column.Search.Value));
                        }
                        if (column.Data == "Percentage" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Percentage.Contains(column.Search.Value));
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "DiscountName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.DiscountName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.DiscountName);
                            }
                        }
                        if (orderColumn == "DiscountDescription")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.DiscountDescription);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.DiscountDescription);
                            }
                        }
                        if (orderColumn == "DiscountTypeDescription")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.DiscountTypeDescription);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.DiscountTypeDescription);
                            }
                        }
                        if (orderColumn == "ClinicName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.ClinicName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.ClinicName);
                            }
                        }
                        if (orderColumn == "Percentage")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Percentage);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Percentage);
                            }
                        }
                        if (orderColumn == "sStartDate")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.StartDate);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.StartDate);
                            }
                        }
                        if (orderColumn == "sEndDate")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.EndDate);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.EndDate);
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
                    }

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (vwDiscountClinic dbDiscount in query.ToList())
                    {
                        vwDiscountClinicAux aux = new vwDiscountClinicAux();
                        DataHelper.fill(aux, dbDiscount);
                        //DataHelper.fill(aux.discountTypeAux, dbDiscount.DiscountType);
                        //foreach (DiscountConcept dbDisConcept in dbDiscount.DiscountConcept)
                        //{
                        //    DiscountConceptAux tempDiscon = new DiscountConceptAux();
                        //    DataHelper.fill(tempDiscon, dbDisConcept);
                        //    DataHelper.fill(tempDiscon.conceptAux, dbDisConcept.Concept);
                        //    aux.discountConceptAux.Add(tempDiscon);
                        //}
                        //foreach (Clinic dbClinic in dbDiscount.Clinic)
                        //{
                        //    ClinicAux tempClinic = new ClinicAux();
                        //    DataHelper.fill(tempClinic, dbClinic);
                        //    aux.clinicList.Add(tempClinic);
                        //}
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
        public static DiscountResult GetDiscount(int id)
        {
            DiscountResult result = new DiscountResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Discount dbDiscount = db.Discount.Where(d => d.id == id).FirstOrDefault();
                    if (dbDiscount != null)
                    {
                        DataHelper.fill(result.data, dbDiscount);
                        result.data.startDate = DateTimeHelper.GetDateTimeFromDateTimeUTC((DateTime)result.data.startDate);
                        result.data.endDate = DateTimeHelper.GetDateTimeFromDateTimeUTC((DateTime)result.data.endDate);
                        DataHelper.fill(result.data.discountTypeAux, dbDiscount.DiscountType);
                        List<int> clinicIds = dbDiscount.Clinic.Select(c => c.id).ToList();
                        foreach (DiscountConcept dbDisConcept in dbDiscount.DiscountConcept)
                        {
                            DiscountConceptAux tempDiscon = new DiscountConceptAux();
                            DataHelper.fill(tempDiscon, dbDisConcept);
                            DataHelper.fill(tempDiscon.conceptAux, dbDisConcept.Concept);
                            DiscountConcept toApplyDisc = null;
                            if (dbDiscount.DiscountType.Code != "Direct")
                            {
                                toApplyDisc = db.DiscountConcept.FirstOrDefault(
                                    d => !d.Discount.Deleted
                                    &&
                                    d.Discount.DiscountType.Code == "Direct"
                                    &&
                                    d.ConceptId == dbDisConcept.ConceptId
                                    &&
                                    d.Discount.Clinic.Any(c => clinicIds.Contains(c.id))
                                    &&
                                    dbDiscount.StartDate >= d.Discount.StartDate
                                    &&
                                    dbDiscount.EndDate <= d.Discount.EndDate
                                    );
                                if (toApplyDisc != null)
                                {
                                    tempDiscon.conceptAux.price = tempDiscon.conceptAux.price - (tempDiscon.conceptAux.price * toApplyDisc.Percentage / 100);
                                }
                            }
                            result.data.discountConceptAux.Add(tempDiscon);
                        }
                        foreach (Clinic dbClinic in dbDiscount.Clinic)
                        {
                            ClinicAux tempClinic = new ClinicAux();
                            DataHelper.fill(tempClinic, dbClinic);
                            result.data.clinicList.Add(tempClinic);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Descuento no encontrado.";
                        return result;
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
        public static DiscountResult DeleteDiscount(int id)
        {
            DiscountResult result = new DiscountResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Discount dbDiscount = db.Discount.Where(d => d.id == id).FirstOrDefault();
                    if (dbDiscount != null)
                    {
                        dbDiscount.Deleted = true;
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Descuento no encontrado.";
                        return result;
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
        public static DiscountResult SaveDiscount(DiscountAux discount)
        {
            DiscountResult result = new DiscountResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Discount dbDiscount;
                    if (discount.discountTypeAux.code == "Direct")
                    {
                        discount.startDate = DateTime.UtcNow;
                        discount.endDate = DateTime.UtcNow.AddYears(15);
                    }

                    discount.startDate = discount.startDate.Date;
                    discount.endDate = discount.endDate.Date.AddDays(1).AddSeconds(-1);
                    if (discount.discountTypeAux.code != "Direct")
                    {
                        discount.startDate = discount.startDate.ToUniversalTime();
                        discount.endDate = discount.endDate.ToUniversalTime();
                    }
                    if (discount.id > 0)
                    {
                        dbDiscount = db.Discount.Where(d => d.id == discount.id).FirstOrDefault();
                        if (dbDiscount != null)
                        {
                            discount.dbfill(ref dbDiscount);
                            dbDiscount.DiscountTypeId = discount.discountTypeAux.id;
                            dbDiscount.Clinic.Clear();
                            foreach (ClinicAux discClinic in discount.clinicList)
                            {
                                dbDiscount.Clinic.Add(db.Clinic.FirstOrDefault(c => c.id == discClinic.id));
                            }
                            db.DiscountConcept.RemoveRange(dbDiscount.DiscountConcept);
                            foreach (DiscountConceptAux discConcept in discount.discountConceptAux)
                            {
                                DiscountConcept dbDiscountConcept = new DiscountConcept();
                                //Se utiliza el id porque el objeto a guardar es un conceptAux parseado a un
                                //discountconcept
                                dbDiscountConcept.Concept = db.Concept.First(c => c.id == discConcept.conceptAux.id);
                                dbDiscountConcept.Percentage = discConcept.percentage;
                                dbDiscount.DiscountConcept.Add(dbDiscountConcept);
                            }
                        }
                        else
                        {
                            result.success = false;
                            result.message = "El descuento no fue encontrado.";
                        }
                    }
                    else
                    {
                        dbDiscount = db.Discount.Create();
                        discount.dbfill(ref dbDiscount);
                        dbDiscount.DiscountTypeId = discount.discountTypeAux.id;

                        foreach (ClinicAux discClinic in discount.clinicList)
                        {
                            dbDiscount.Clinic.Add(db.Clinic.First(c => c.id == discClinic.id));
                        }
                        foreach (DiscountConceptAux discConcept in discount.discountConceptAux)
                        {
                            DiscountConcept dbDiscountConcept = new DiscountConcept();
                            //Se utiliza el id porque el objeto a guardar es un conceptAux parseado a un
                            //discountconcept
                            dbDiscountConcept.Concept = db.Concept.First(c => c.id == discConcept.conceptAux.id);
                            dbDiscountConcept.Percentage = discConcept.percentage;
                            dbDiscount.DiscountConcept.Add(dbDiscountConcept);
                        }
                        db.Discount.Add(dbDiscount);
                    }
                    db.SaveChanges();
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
        public static DiscountTypeResult GetDiscountTypeSelect()
        {
            DiscountTypeResult result = new DiscountTypeResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.DiscountType.OrderBy(p => p.Description);
                    foreach (DiscountType tipo in query.ToList())
                    {
                        DiscountTypeAux aux = new DiscountTypeAux();
                        DataHelper.fill(aux, tipo);
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
        public static DiscountTypeResult GetDiscountsSelect()
        {
            DiscountTypeResult result = new DiscountTypeResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.DiscountType.AsQueryable();
                    foreach (DiscountType type in query.ToList())
                    {
                        if (type.id != 2)
                        {
                            DiscountTypeAux aux = new DiscountTypeAux();
                            DataHelper.fill(aux, type);
                            result.data_list.Add(aux);
                        }
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
            //DiscountTypeResult result = new DiscountTypeResult();
            //using (dbINMEDIK db = new dbINMEDIK())
            //{
            //    try
            //    {
            //        var query = db.DiscountType.AsQueryable();
            //        foreach (DiscountTypeResult discountType in query.ToList())
            //        {
            //            DiscountTypeAux aux = new DiscountTypeAux();
            //            DataHelper.fill(aux, discountType);
            //            result.data_list.Add(aux);
            //        }
            //        result.success = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        result.success = false;
            //        result.exception = ex;
            //        result.message = "Ocurrió un error inesperado. " + result.exception_message;
            //    }
            //}
            //return result;
        }
    
    }
}