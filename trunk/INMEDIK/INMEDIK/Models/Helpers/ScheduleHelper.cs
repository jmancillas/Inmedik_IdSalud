using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Configuration;
using System.Web.Hosting;

namespace INMEDIK.Models.Helpers
{
    public class ParentScheduleAux
    {
        public int id { get; set; }
        public int OrderId { get; set; }
        public int PatientId { get; set; }
        public int ClinicId { get; set; }
        public int MedicId { get; set; }
        public int StatusId { get; set; }
        public int ConceptId { get; set; }
        public string Age { get; set; }
        public int? NurseId { get; set; }
        public DateTime? Updated { get; set; }

        public OrderAux OrderAux { get; set; }
        public PatientAux PatientAux { get; set; }
        public EmployeeAux MedicAux { get; set; }
        public StatusAux StatusAux { get; set; }
        public ConceptAux ConceptAux { get; set; }
        public EmployeeAux NurseAux { get; set; }

        public ParentScheduleAux()
        {
            OrderAux = new OrderAux();
            PatientAux = new PatientAux();
            MedicAux = new EmployeeAux();
            StatusAux = new StatusAux();
            ConceptAux = new ConceptAux();
        }
    }
    public class ExamAux : ParentScheduleAux
    {
        public string Notes { get; set; }
        public string Department { get; set; }
        public ExamAux()
        {
        }
    }
    public class ServiceAux : ParentScheduleAux
    {
        public string Notes { get; set; }
        public ServiceAux()
        {
        }
    }
    public class ConsultAux : ParentScheduleAux
    {
        public DateTime Scheduled { get; set; }
        public string Temperature { get; set; }
        public string BloodPressure { get; set; }
        public string HeartRate { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public string Alergics { get; set; }
        public string AHF { get; set; }
        public string APP { get; set; }
        public string APNP { get; set; }
        public string AGO { get; set; }
        public string PA { get; set; }
        public string PhisicalExploration { get; set; }
        public string Treatment { get; set; }


        public List<ChronicDiseaseAux> ChronicDiseaseAux { get; set; }
        public List<DiseaseAux> DiseaseAux { get; set; }

        public string sUpdated
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                    new DateTime(Updated.Value.Ticks, DateTimeKind.Utc),
                    TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                    ).ToString("dd/MMMM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }

        public ConsultAux()
        {
            ChronicDiseaseAux = new List<ChronicDiseaseAux>();
            DiseaseAux = new List<DiseaseAux>();
            NurseAux = new EmployeeAux();
        }
    }
    public class vwScheduleAux : ParentScheduleAux
    {
        public DateTime? Scheduled { get; set; }
        public string stringScheduled
        {
            get
            {
                return Scheduled.HasValue ?
                    TimeZoneInfo.ConvertTimeFromUtc(
                    new DateTime(Scheduled.Value.Ticks, DateTimeKind.Utc),
                    TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                    ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"))
                    : "";
            }
        }
        public string PatientName { get; set; }
        public string ClinicName { get; set; }
        public string MedicName { get; set; }
        public string StatusName { get; set; }
        public string ConceptName { get; set; }
        public string CategoryName { get; set; }
        public DateTime OrderCreated { get; set; }
        public string stringOrderCreated
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                    new DateTime(OrderCreated.Ticks, DateTimeKind.Utc),
                    TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                    ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public DateTime PatientBirthDate { get; set; }
        public string sPatientBirthDate { get; set; }
        public vwScheduleAux()
        {
        }
    }

    public class vwScheduleResult : Result
    {
        public vwScheduleAux data { get; set; }
        public List<vwScheduleAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public vwScheduleResult()
        {
            this.data = new vwScheduleAux();
            this.data_list = new List<vwScheduleAux>();
            this.total = new NumericResult();
        }
    }
    public class ConsultResult : Result
    {
        public ConsultAux data { get; set; }
        public List<ConsultAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public ConsultResult()
        {
            this.data = new ConsultAux();
            this.data_list = new List<ConsultAux>();
            this.total = new NumericResult();
        }
    }
    public class ServiceResult : Result
    {
        public ServiceAux data { get; set; }
        public List<ServiceAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public ServiceResult()
        {
            this.data = new ServiceAux();
            this.data_list = new List<ServiceAux>();
            this.total = new NumericResult();
        }
    }
    public class ExamResult : Result
    {
        public ExamAux data { get; set; }
        public List<ExamAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public ExamResult()
        {
            this.data = new ExamAux();
            this.data_list = new List<ExamAux>();
            this.total = new NumericResult();
        }
    }

    class ScheduleHelper
    {
        public static vwScheduleResult GetSchedule(DTParameterModel filter, string tabClicked, string PatientId, string RolName, int roleId, int employeeId, int? clinicId = null)
        {
            vwScheduleResult result = new vwScheduleResult();

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
                    //var query = db.vwSchedule.AsQueryable();
                    //result.total.value = query.Count();
                    //if (!string.IsNullOrEmpty(tabClicked))
                    //{
                    //    DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
                    //    if (tabClicked == "Consult")
                    //    {
                    //        // query = query.Where(q => q.Scheduled.Value <= currentDateTime || q.Scheduled == null);
                    //    }
                    //    else
                    //    {
                    //        query = query.Where(q => q.Scheduled > currentDateTime);
                    //    }
                    //}
                    #region filtros extra que no provienen de columnas

                    /*si se manda el id de paciente, es para traer el historial, en este caso, se ignora la clinica y el rol para que se vean todas sus consultas*/
                    //if (!string.IsNullOrEmpty(PatientId))
                    //{
                    //    query = query.Where(q => q.PatientId.ToString() == PatientId);
                    //}
                    //else
                    //{
                    //    if (RolName == "Nurse")
                    //    {
                    //        query = query.Where(q => q.StatusName == "Enfermeria" || q.StatusName == "En proceso" || q.StatusName == "Medico");
                    //    }
                    //    if (RolName == "Medic")
                    //    {
                    //        query = query.Where(q => q.StatusName != "Enfermeria" && q.MedicId.HasValue && q.MedicId.Value == employeeId);
                    //    }

                    //    if (clinicId != null)
                    //    {
                    //        query = query.Where(q => q.ClinicId == clinicId);
                    //    }
                    //}
                    //result.total.value = query.Count();
                    //#endregion
                    //#region filtros
                    //foreach (DTColumn column in filter.Columns)
                    //{
                    //    if (column.Data == "PatientId" && !String.IsNullOrEmpty(column.Search.Value))
                    //    {
                    //        query = query.Where(q => q.PatientId.ToString() == column.Search.Value);
                    //    }
                    //    if (column.Data == "PatientName" && !String.IsNullOrEmpty(column.Search.Value))
                    //    {
                    //        query = query.Where(q => q.PatientName.Contains(column.Search.Value));
                    //    }
                    //    if (column.Data == "MedicName" && !String.IsNullOrEmpty(column.Search.Value))
                    //    {
                    //        query = query.Where(q => q.MedicName.Contains(column.Search.Value));
                    //    }

                    //    if (column.Data == "StatusName" && !String.IsNullOrEmpty(column.Search.Value))
                    //    {
                    //        query = query.Where(q => q.StatusName.Contains(column.Search.Value));
                    //    }
                    //    if (column.Data == "CategoryName" && !String.IsNullOrEmpty(column.Search.Value))
                    //    {
                    //        query = query.Where(q => q.CategoryName.Contains(column.Search.Value));
                    //    }
                    //    if (column.Data == "ConceptName" && !String.IsNullOrEmpty(column.Search.Value))
                    //    {
                    //        query = query.Where(q => q.ConceptName.Contains(column.Search.Value));
                    //    }

                    //}
                    //result.total.value = query.Count();
                    //#endregion
                    //#region orden
                    //if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    //{
                    //    if (orderColumn == "PatientId")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.PatientId);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.PatientId);
                    //        }
                    //    }
                    //    if (orderColumn == "PatientName")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.PatientName);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.PatientName);
                    //        }
                    //    }
                    //    if (orderColumn == "MedicName")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.MedicName);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.MedicName);
                    //        }
                    //    }
                    //    if (orderColumn == "stringOrderCreated")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.OrderCreated);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.OrderCreated);
                    //        }
                    //    }
                    //    if (orderColumn == "StatusName")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.StatusName);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.StatusName);
                    //        }
                    //    }
                    //    if (orderColumn == "CategoryName")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.CategoryName);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.CategoryName);
                    //        }
                    //    }
                    //    if (orderColumn == "ConceptName")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.ConceptName);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.ConceptName);
                    //        }
                    //    }
                    //    if (orderColumn == "stringScheduled")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.Scheduled);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.Scheduled);
                    //        }
                    //    }
                    //}
                    #endregion

                    //result.total.value = query.Count();

                    //query = query.Skip(filter.Start).Take(filter.Length);
                    //foreach (var scheduleDb in query.ToList())
                    //{
                    //    vwScheduleAux aux = new vwScheduleAux();
                    //    DataHelper.fill(aux, scheduleDb);
                    //    result.data_list.Add(aux);
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

        public static ConsultResult LoadDetailConsult(int id)
        {
            ConsultResult result = new ConsultResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var consultDb = db.Consult.Where(s => s.id == id).FirstOrDefault();
                    if (consultDb != null)
                    {
                        DataHelper.fill(result.data, consultDb);
                        DataHelper.fill(result.data.ConceptAux, consultDb.Concept);
                        DataHelper.fill(result.data.OrderAux, consultDb.Orders);
                        DataHelper.fill(result.data.StatusAux, consultDb.Status);
                        DataHelper.fill(result.data.PatientAux, consultDb.Patient);
                        DataHelper.fill(result.data.PatientAux.personAux, consultDb.Patient.Person);

                        DataHelper.fill(result.data.MedicAux, consultDb.Employee);
                        DataHelper.fill(result.data.MedicAux.personAux, consultDb.Employee.Person);

                        GenericResult signature =
                                FileHelper.GetFile(
                                    consultDb.Employee.id + "_Signature.jpg",
                                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Images")
                                );
                        if (signature.success)
                        {
                            result.data.MedicAux.signatureBytes = signature.string_value;
                        }

                        if (consultDb.Employee1 != null)
                        {
                            DataHelper.fill(result.data.NurseAux, consultDb.Employee1);
                            DataHelper.fill(result.data.NurseAux.personAux, consultDb.Employee1.Person);
                        }
                        //foreach (var diseaseDb in consultDb.Disease)
                        //{
                        //    var aux = new DiseaseAux();
                        //    DataHelper.fill(aux, diseaseDb);
                        //    result.data.DiseaseAux.Add(aux);
                        //}
                        //foreach (var chronicDiseaseDb in consultDb.ChronicDisease)
                        //{
                        //    var aux = new ChronicDiseaseAux();
                        //    DataHelper.fill(aux, chronicDiseaseDb);
                        //    result.data.ChronicDiseaseAux.Add(aux);
                        //}

                        if (string.IsNullOrEmpty(result.data.Age))
                        {
                            result.data.Age = PatientHelper.GetPatientAge(consultDb.PatientId).string_value;
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.message = "No se encontró el registro";
                        result.success = false;
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
        public static ConsultResult SaveConsult(ConsultAux ConsultAux, string roleName)
        {
            ConsultResult result = new ConsultResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var consultDb = db.Consult.Where(s => s.id == ConsultAux.id).FirstOrDefault();
                    if (consultDb != null)
                    {
                        //consultDb.ChronicDisease.Clear();
                        foreach (var cdItem in ConsultAux.ChronicDiseaseAux)
                        {
                            ChronicDisease cdItemDb = db.ChronicDisease.Where(c => c.id == cdItem.id).FirstOrDefault();
                            //consultDb.ChronicDisease.Add(cdItemDb);
                        }

                        switch (consultDb.Status.Name)
                        {
                            case "Enfermeria":
                                consultDb.Age = ConsultAux.Age;
                                consultDb.Temperature = ConsultAux.Temperature;
                                consultDb.HeartRate = ConsultAux.HeartRate;
                                consultDb.BloodPressure = ConsultAux.BloodPressure;
                                consultDb.Weight = ConsultAux.Weight;
                                consultDb.Height = ConsultAux.Height;
                                consultDb.Alergics = ConsultAux.Alergics;
                                consultDb.NurseId = ConsultAux.NurseId;
                                consultDb.MedicId = ConsultAux.MedicId;

                                consultDb.Updated = DateTime.UtcNow;

                                var statusMedic = db.Status.Where(s => s.Name == "Medico").FirstOrDefault();
                                consultDb.StatusId = statusMedic.id;

                                if (consultDb.Scheduled > DateTime.UtcNow)
                                {
                                    consultDb.Scheduled = DateTime.UtcNow;
                                }
                                db.SaveChanges();

                                result.success = true;
                                break;
                            case "Medico":
                                if (roleName != "Nurse")
                                {
                                    consultDb.AHF = ConsultAux.AHF;
                                    consultDb.APP = ConsultAux.APP;
                                    consultDb.APNP = ConsultAux.APNP;
                                    consultDb.AGO = ConsultAux.AGO;
                                    consultDb.PA = ConsultAux.PA;
                                    consultDb.PhisicalExploration = ConsultAux.PhisicalExploration;
                                    consultDb.Treatment = ConsultAux.Treatment;

                                    //consultDb.Disease.Clear();
                                    foreach (var dItem in ConsultAux.DiseaseAux)
                                    {
                                        Disease cdItemDb = db.Disease.Where(c => c.id == dItem.id).FirstOrDefault();
                                        //consultDb.Disease.Add(cdItemDb);
                                    }

                                    var statusFinished = db.Status.Where(s => s.Name == "Terminado").FirstOrDefault();
                                    consultDb.StatusId = statusFinished.id;
                                }
                                else
                                {
                                    consultDb.NurseId = ConsultAux.NurseId;
                                    consultDb.MedicId = ConsultAux.MedicId;
                                }

                                db.SaveChanges();
                                result.success = true;
                                break;
                            case "Terminado":
                                result.success = false;
                                result.message = "Consulta en estatus 'terminado' no puede ser modificada.";
                                break;
                            default:
                                result.success = false;
                                result.message = "Estatus de la consulta no encontrado.";
                                break;

                        }
                    }
                    else
                    {
                        result.message = "No se encontró el registro";
                        result.success = false;
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
        public static ServiceResult LoadDetailService(int id)
        {
            ServiceResult result = new ServiceResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var serviceDb = db.Service.Where(s => s.id == id).FirstOrDefault();
                    if (serviceDb != null)
                    {
                        DataHelper.fill(result.data, serviceDb);
                        DataHelper.fill(result.data.ConceptAux, serviceDb.Concept);
                        //DataHelper.fill(result.data.OrderAux, serviceDb.Orders);
                        DataHelper.fill(result.data.StatusAux, serviceDb.Status);
                        DataHelper.fill(result.data.PatientAux, serviceDb.Patient);
                        DataHelper.fill(result.data.PatientAux.personAux, serviceDb.Patient.Person);

                        DataHelper.fill(result.data.MedicAux, serviceDb.Employee);
                        DataHelper.fill(result.data.MedicAux.personAux, serviceDb.Employee.Person);

                        if (string.IsNullOrEmpty(result.data.Age))
                        {
                            result.data.Age = PatientHelper.GetPatientAge(serviceDb.PatientId).string_value;
                        }

                        result.success = true;
                    }
                    else
                    {
                        result.message = "No se encontró el registro";
                        result.success = false;
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
        public static ServiceResult SaveService(ServiceAux ServiceAux)
        {
            ServiceResult result = new ServiceResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var ServiceDb = db.Service.Where(s => s.id == ServiceAux.id).FirstOrDefault();
                    if (ServiceDb != null)
                    {
                        switch (ServiceDb.Status.Name)
                        {
                            case "Enfermeria":
                                ServiceDb.Notes = ServiceAux.Notes;
                                //ServiceDb.MedicId = ServiceAux.MedicId;
                                //ServiceDb.NurseId = ServiceAux.NurseId;
                                //ServiceDb.Updated = DateTime.UtcNow;
                                ServiceDb.Age = ServiceAux.Age;
                                ServiceDb.StatusId = ServiceAux.StatusId;

                                db.SaveChanges();

                                result.success = true;
                                break;
                            case "Terminado":
                                result.success = false;
                                result.message = "Servicios en estatus 'terminado' no puede ser modificada.";
                                break;
                            default:
                                result.success = false;
                                result.message = "Estatus de la consulta no encontrado.";
                                break;

                        }
                    }
                    else
                    {
                        result.message = "No se encontró el registro";
                        result.success = false;
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
        public static ExamResult LoadDetailExam(int id)
        {
            ExamResult result = new ExamResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var serviceDb = db.Exam.Where(s => s.id == id).FirstOrDefault();
                    if (serviceDb != null)
                    {
                        DataHelper.fill(result.data, serviceDb);
                        DataHelper.fill(result.data.ConceptAux, serviceDb.Concept);
                        DataHelper.fill(result.data.ConceptAux.testAux, serviceDb.Concept.Test.FirstOrDefault());
                        //DataHelper.fill(result.data.OrderAux, serviceDb.Orders);
                        DataHelper.fill(result.data.StatusAux, serviceDb.Status);
                        DataHelper.fill(result.data.PatientAux, serviceDb.Patient);
                        DataHelper.fill(result.data.PatientAux.personAux, serviceDb.Patient.Person);
                        if (serviceDb.Employee != null)
                        {
                            DataHelper.fill(result.data.MedicAux, serviceDb.Employee);
                            DataHelper.fill(result.data.MedicAux.personAux, serviceDb.Employee.Person);
                        }
                        if (string.IsNullOrEmpty(result.data.Age))
                        {
                            result.data.Age = PatientHelper.GetPatientAge(serviceDb.PatientId).string_value;
                        }

                        result.success = true;
                    }
                    else
                    {
                        result.message = "No se encontró el registro";
                        result.success = false;
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
        public static ExamResult LoadDetailExams(int id)
        {
            ExamResult result = new ExamResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var serviceDb = db.Exam.Where(s => s.id == id).FirstOrDefault();
                    if (serviceDb != null)
                    {
                        //var exams = db.Exam.Where(e => e.OrderId == serviceDb.OrderId);

                        //foreach (var exam in exams)
                        //{
                        //    var temp = new ExamAux();

                        //    DataHelper.fill(temp, exam);
                        //    DataHelper.fill(temp.ConceptAux, exam.Concept);
                        //    DataHelper.fill(temp.ConceptAux.testAux, exam.Concept.Test.FirstOrDefault());
                        //    DataHelper.fill(temp.OrderAux, exam.Orders);
                        //    DataHelper.fill(temp.StatusAux, exam.Status);
                        //    DataHelper.fill(temp.PatientAux, exam.Patient);
                        //    DataHelper.fill(temp.PatientAux.personAux, exam.Patient.Person);
                        //    if (exam.Employee != null)
                        //    {
                        //        DataHelper.fill(temp.MedicAux, exam.Employee);
                        //        DataHelper.fill(temp.MedicAux.personAux, exam.Employee.Person);
                        //    }
                        //    if (string.IsNullOrEmpty(temp.Age))
                        //    {
                        //        temp.Age = PatientHelper.GetPatientAge(exam.PatientId).string_value;
                        //    }
                        //    result.data_list.Add(temp);
                        //}

                        result.success = true;
                    }
                    else
                    {
                        result.message = "No se encontró el registro";
                        result.success = false;
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

        public static ExamResult SaveExam(ExamAux ExamAux)
        {
            ExamResult result = new ExamResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var ExamDb = db.Exam.Where(s => s.id == ExamAux.id).FirstOrDefault();
                    if (ExamDb != null)
                    {
                        switch (ExamDb.Status.Name)
                        {
                            case "Enfermeria":
                            case "En proceso":
                                //ExamDb.Notes = ExamAux.Notes;
                                ExamDb.MedicId = ExamAux.MedicId;
                                //ExamDb.NurseId = ExamAux.NurseId;
                                //ExamDb.Updated = DateTime.UtcNow;
                                ExamDb.Age = ExamAux.Age;
                                ExamDb.StatusId = ExamAux.StatusId;

                                var StatusByUser = db.Status.Where(s => s.id == ExamAux.StatusId).FirstOrDefault();
                                /*en caso de que ponga un examen como terminado, lo guardamos en la tabla de examenes correspondiente (Laboratory/Xray)*/
                                if (StatusByUser != null && StatusByUser.Name == "Terminado")
                                {
                                    var TestDb = ExamDb.Concept.Test.FirstOrDefault();
                                    var statusPend = db.Status.Where(s => s.Name == "Pendiente").FirstOrDefault();
                                    if (TestDb.XRay)
                                    {
                                        var xrayDb = new Xray();
                                        xrayDb.PatientId = ExamDb.PatientId;
                                        //xrayDb.OrderConceptId = ExamDb.OrderConceptId;
                                        xrayDb.Age = PatientHelper.GetPatientAge(ExamDb.PatientId).string_value;
                                        xrayDb.Comment = string.Empty;
                                        xrayDb.StatusId = statusPend.id;
                                        xrayDb.Updated = DateTime.UtcNow;
                                        xrayDb.Created = DateTime.UtcNow;
                                        xrayDb.ExamId = ExamDb.id;

                                        db.Xray.Add(xrayDb);
                                    }
                                    else
                                    {
                                        if (TestDb.Laboratory)
                                        {
                                            var laboratoryDb = new Laboratory();
                                            laboratoryDb.PatientId = ExamDb.PatientId;
                                            //laboratoryDb.OrderConceptId = ExamDb.OrderConceptId;
                                            laboratoryDb.Age = PatientHelper.GetPatientAge(ExamDb.PatientId).string_value;
                                            laboratoryDb.Comment = string.Empty;
                                            laboratoryDb.StatusId = statusPend.id;
                                            laboratoryDb.Updated = DateTime.UtcNow;
                                            laboratoryDb.Created = DateTime.UtcNow;
                                            laboratoryDb.ExamId = ExamDb.id;

                                            db.Laboratory.Add(laboratoryDb);
                                        }
                                    }
                                }

                                db.SaveChanges();
                                result.success = true;
                                break;
                            case "Terminado":
                                result.success = false;
                                result.message = "Exámenes en estatus 'terminado' no puede ser modificada.";
                                break;
                            default:
                                result.success = false;
                                result.message = "Estatus de la consulta no encontrado.";
                                break;

                        }
                    }
                    else
                    {
                        result.message = "No se encontró el registro";
                        result.success = false;
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
        public static ExamResult SaveExams(List<ExamAux> ExamAux, int NurseId)
        {
            ExamResult result = new ExamResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    int statusAux = ExamAux[0].StatusId;
                    string notesAux = ExamAux[0].Notes;

                    foreach (var exam in ExamAux)
                    {
                        var ExamDb = db.Exam.Where(s => s.id == exam.id).FirstOrDefault();

                        if (ExamDb != null)
                        {
                            switch (ExamDb.Status.Name)
                            {
                                case "Enfermeria":
                                case "En proceso":

                                    //var exams = db.Exam.Where(e => e.OrderId == ExamDb.OrderId);

                                    //ExamDb.Notes = notesAux;
                                    ExamDb.MedicId = exam.MedicId;
                                    //ExamDb.NurseId = NurseId;
                                    //ExamDb.Updated = DateTime.UtcNow;
                                    ExamDb.Age = exam.Age;
                                    ExamDb.StatusId = statusAux;

                                    var StatusByUser = db.Status.Where(s => s.id == statusAux).FirstOrDefault();
                                    /*en caso de que ponga un examen como terminado, lo guardamos en la tabla de examenes correspondiente (Laboratory/Xray)*/
                                    if (StatusByUser != null && StatusByUser.Name == "Terminado")
                                    {
                                        var TestDb = ExamDb.Concept.Test.FirstOrDefault();
                                        var statusPend = db.Status.Where(s => s.Name == "Pendiente").FirstOrDefault();
                                        if (TestDb.XRay)
                                        {
                                            var xrayDb = new Xray();
                                            xrayDb.PatientId = ExamDb.PatientId;
                                            //xrayDb.OrderConceptId = ExamDb.OrderConceptId;
                                            xrayDb.Age = PatientHelper.GetPatientAge(ExamDb.PatientId).string_value;
                                            xrayDb.Comment = string.Empty;
                                            xrayDb.StatusId = statusPend.id;
                                            xrayDb.Updated = DateTime.UtcNow;
                                            xrayDb.Created = DateTime.UtcNow;
                                            xrayDb.ExamId = ExamDb.id;

                                            db.Xray.Add(xrayDb);
                                        }
                                        else
                                        {
                                            if (TestDb.Laboratory)
                                            {
                                                var laboratoryDb = new Laboratory();
                                                laboratoryDb.PatientId = ExamDb.PatientId;
                                                //laboratoryDb.OrderConceptId = ExamDb.OrderConceptId;
                                                laboratoryDb.Age = PatientHelper.GetPatientAge(ExamDb.PatientId).string_value;
                                                laboratoryDb.Comment = string.Empty;
                                                laboratoryDb.StatusId = statusPend.id;
                                                laboratoryDb.Updated = DateTime.UtcNow;
                                                laboratoryDb.Created = DateTime.UtcNow;
                                                laboratoryDb.ExamId = ExamDb.id;

                                                db.Laboratory.Add(laboratoryDb);
                                            }
                                        }
                                    }

                                    db.SaveChanges();
                                    result.success = true;
                                    break;
                                case "Terminado":
                                    result.success = false;
                                    result.message = "Exámenes en estatus 'terminado' no puede ser modificada.";
                                    break;
                                default:
                                    result.success = false;
                                    result.message = "Estatus de la consulta no encontrado.";
                                    break;

                            }
                        }
                        else
                        {
                            result.message = "No se encontró el registro";
                            result.success = false;
                        }
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

        public static GenericResult BuildPrenscription(int consultId, string userName, int noteid)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    //RECETA EXPRESS
                    int ElectronicFileId = consultId;
                    var ElectronicFile = db.ElectronicFile.FirstOrDefault(e => e.Id == ElectronicFileId);
                    var User = db.User.FirstOrDefault(u => u.UserAccount == userName);
                    var Patient = db.Patient.FirstOrDefault(p => p.id == ElectronicFile.PatientId);
                    var Recipedb = db.Recipe.FirstOrDefault(e => e.Id == noteid);
                    var Speciality = db.Specialty.FirstOrDefault(s => s.id == ElectronicFile.Employee.SpecialtyId);
                    var Date = DateTime.UtcNow;
                    if (User != null)
                    {
                        if (ElectronicFile != null)
                        {
                            PersonAux personAux = new PersonAux();
                            DataHelper.fill(personAux, ElectronicFile.Employee.Person);
                            string specialityDoctor;
                            string templateName = "RecetaExp";
                            string doctorName = ElectronicFile.Employee.Person.Name + " " + ElectronicFile.Employee.Person.LastName;
                            string licenseDoctor = ElectronicFile.Employee.Person.License;
                            string universityDoctor = ElectronicFile.Employee.Person.University;
                            if (Speciality != null) specialityDoctor = Speciality.Name; specialityDoctor = "Admin";
                            string patientName = Patient.Person.Name + " " + Patient.Person.LastName;
                            string consultDate = Date.ToString("dd/MM/yyyy", new CultureInfo("es-MX"));

                            string clinicName = ElectronicFile.Clinic.Name;
                            string patientExp = Patient.Person.id.ToString();
                            string patientAge = PatientHelper.GetPatientAge(ElectronicFile.PatientId).string_value;

                            string treatment = Recipedb.RecipeText;
                            List<string> treatmentList = new List<string>();

                            string clinicCounty = ElectronicFile.Clinic.Address.County.Name;
                            string clinicAdressLine = ElectronicFile.Clinic.Address.AddressLine;
                            string clinicAdress = clinicAdressLine + " Col. " + clinicCounty;
                            string clinicEmail = ElectronicFile.Clinic.Email;
                            string clinicPhone = ElectronicFile.Clinic.PhoneNumber;
                            string logoName = "varaEsculapio.png";

                            var template = db.Template.Where(tem => tem.TemplateID == 8);
                            List<KeyValuePair<string, string>> ListKey_Value = new List<KeyValuePair<string, string>>();
                            ListKey_Value.Add(new KeyValuePair<string, string>("$logo", HostingEnvironment.MapPath("~/Content/Images/" + logoName)));
                            ListKey_Value.Add(new KeyValuePair<string, string>("$doctorName", doctorName));
                            ListKey_Value.Add(new KeyValuePair<string, string>("$doctorLicense", licenseDoctor));
                            ListKey_Value.Add(new KeyValuePair<string, string>("$doctorUniversity", universityDoctor));
                            ListKey_Value.Add(new KeyValuePair<string, string>("$doctorSpeciality", specialityDoctor));
                            ListKey_Value.Add(new KeyValuePair<string, string>("$patientName", patientName));
                            ListKey_Value.Add(new KeyValuePair<string, string>("$consultDate", consultDate));
                            ListKey_Value.Add(new KeyValuePair<string, string>("$patientExp", patientExp));
                            ListKey_Value.Add(new KeyValuePair<string, string>("$patienAge", patientAge));
                            ListKey_Value.Add(new KeyValuePair<string, string>("$treatment", treatment));
                            ListKey_Value.Add(new KeyValuePair<string, string>("$clinicName", clinicName));
                            ListKey_Value.Add(new KeyValuePair<string, string>("$clinicAdress", clinicAdress));
                            ListKey_Value.Add(new KeyValuePair<string, string>("$clinicEmail", clinicEmail));
                            ListKey_Value.Add(new KeyValuePair<string, string>("$clinicPhone", clinicPhone));

                            result.string_value = BuildTemplateHelper.GenerateTemplate(templateName, ListKey_Value).string_value;
                            result.success = true;

                        }
                        else
                        {
                            result.string_value = BuildTemplateHelper.GenerateTemplateError("No se puede acceder a esta información").string_value;
                            return result;
                        }
                    }
                    else
                    {
                        throw new Exception("La sesión ha caducado");
                    }
                }

                catch (Exception error)
                {
                    result.exception = error;
                    result.string_value = error.Message;
                }
            }
            return result;
        }
    }
}
