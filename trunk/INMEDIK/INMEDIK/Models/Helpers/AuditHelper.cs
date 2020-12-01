using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Entity;
using System.Globalization;

namespace INMEDIK.Models.Helpers
{
    public class AuditAux
    {
        public int id { get; set; }
        public int employeeId { get; set; }
        public EmployeeAux employeeAux { get; set; }
        public int clinicId { get; set; }
        public ClinicAux clinicAux { get; set; }
        public string comment { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
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
        public string sUpdated
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(updated.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public List<AuditDetailAux> details { get; set; }
        public bool nurse { get; set; }

        public AuditAux()
        {
            this.employeeAux = new EmployeeAux();
            this.clinicAux = new ClinicAux();
            this.details = new List<AuditDetailAux>();
        }

        public void dbFill(ref Audit dbAudit)
        {
            dbAudit.Comment = this.comment;
            dbAudit.Updated = this.updated;
            dbAudit.Created = this.created;
            dbAudit.EmployeeId = this.employeeId;
            dbAudit.ClinicId = this.clinicId;
            dbAudit.Nurse = this.nurse;
        }
    }
    public class AuditDetailAux
    {
        public int id { get; set; }
        public int auditId { get; set; }
        public int conceptId { get; set; }
        public ConceptAux conceptAux { get; set; }
        public int inStock { get; set; }
        public int quantity { get; set; }
        public DateTime? created { get; set; }
        public DateTime? updated { get; set; }
        public string sCreated
        {
            get
            {
                return created.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(created.Value.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX")) : "";
            }
        }
        public string sUpdated
        {
            get
            {
                return updated.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(updated.Value.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX")) : "";
            }
        }

        public AuditDetailAux()
        {
            this.conceptAux = new ConceptAux();
        }

        public void dbFill(ref AuditDetail dbAuditDetail)
        {
            dbAuditDetail.AuditId = this.auditId;
            dbAuditDetail.ConceptId = this.conceptId;
            dbAuditDetail.InStock = this.inStock;
            dbAuditDetail.Quantity = this.quantity;
            dbAuditDetail.Created = this.created;
            dbAuditDetail.Updated = this.updated;
        }
    }
    public class AuditResult : Result
    {
        public AuditAux data { get; set; }
        public List<AuditAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public AuditResult()
        {
            this.data = new AuditAux();
            this.data_list = new List<AuditAux>();
            this.total = new NumericResult();
        }
    }
    public class AuditHelper
    {
        public static AuditResult GetAudit(int id)
        {
            AuditResult result = new AuditResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Audit dbAudit = db.Audit.Where(a => a.id == id).FirstOrDefault();
                    if (dbAudit != null)
                    {
                        DataHelper.fill(result.data, dbAudit);
                        DataHelper.fill(result.data.employeeAux, dbAudit.Employee);
                        DataHelper.fill(result.data.employeeAux.personAux, dbAudit.Employee.Person);
                        DataHelper.fill(result.data.clinicAux, dbAudit.Clinic);
                        foreach (AuditDetail dbDetail in dbAudit.AuditDetail)
                        {
                            AuditDetailAux detail = new AuditDetailAux();
                            DataHelper.fill(detail, dbDetail);
                            DataHelper.fill(detail.conceptAux, dbDetail.Concept);
                            result.data.details.Add(detail);
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.message = "No se encontró la auditoría buscada.";
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
        public static AuditResult GetAudits(DTParameterModel filter, int ClinicId, bool isNurse)
        {
            AuditResult result = new AuditResult();
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
                    IQueryable<Audit> query = db.Audit;
                    query = isNurse ? query.Where(q => q.ClinicId == ClinicId && q.Nurse == isNurse) : query.Where(q => q.ClinicId == ClinicId && q.Nurse != true);

                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "employeeAux.personAux.fullName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => (q.Employee.Person.Name + " " + q.Employee.Person.LastName).ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "clinicAux.name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Clinic.Name.ToString().Contains(column.Search.Value));
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "employeeAux.personAux.fullName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => (q.Employee.Person.Name + " " + q.Employee.Person.LastName));
                            }
                            else
                            {
                                query = query.OrderByDescending(q => (q.Employee.Person.Name + " " + q.Employee.Person.LastName));
                            }
                        }
                        if (orderColumn == "clinicAux.name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Clinic.Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Clinic.Name);
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
                        if (orderColumn == "sUpdated")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Updated);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Updated);
                            }
                        }
                    }

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (Audit dbAudit in query.ToList())
                    {
                        AuditAux aux = new AuditAux();
                        DataHelper.fill(aux, dbAudit);
                        DataHelper.fill(aux.employeeAux, dbAudit.Employee);
                        DataHelper.fill(aux.employeeAux.personAux, dbAudit.Employee.Person);
                        DataHelper.fill(aux.clinicAux, dbAudit.Clinic);

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
        public static AuditResult CreateAudit(AuditAux newAudit)
        {
            AuditResult result = new AuditResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Audit dbAudit = db.Audit.Create();
                    newAudit.created = DateTime.UtcNow;
                    newAudit.updated = DateTime.UtcNow;
                    newAudit.dbFill(ref dbAudit);
                    foreach (AuditDetailAux detail in newAudit.details)
                    {
                        AuditDetail dbDetail = new AuditDetail();
                        detail.created = DateTime.UtcNow;
                        detail.updated = DateTime.UtcNow;
                        detail.dbFill(ref dbDetail);
                        dbAudit.AuditDetail.Add(dbDetail);
                    }
                    db.Audit.Add(dbAudit);
                    db.SaveChanges();
                    result = GetAudit(dbAudit.id);
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
        public static GenericResult SaveAuditComment(string comment, int id)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Audit dbAudit = db.Audit.Where(a => a.id == id).FirstOrDefault();
                    if (dbAudit != null)
                    {
                        dbAudit.Comment = comment;
                        db.SaveChanges();
                    }
                    else
                    {
                        result.success = false;
                        result.message = "No se encontró la auditoría buscada.";
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