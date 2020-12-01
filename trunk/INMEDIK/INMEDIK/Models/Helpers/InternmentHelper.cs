using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Entity;
using System.Globalization;

namespace INMEDIK.Models.Helpers
{

    public class NurseryMeasurementAux
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class MedicalNoteAux
    {
        public DateTime created { get; set; }
        public string note { get; set; }

        public string created_str
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

    public class MedicalNoteResult : Result
    {
        public MedicalNoteAux data { get; set; }
        public List<MedicalNoteAux> data_list { get; set; }

        public MedicalNoteResult()
        {
            this.data = new MedicalNoteAux();
            this.data_list = new List<MedicalNoteAux>();
        }
    }

    public class SummaryHistory
    {
        public DateTime date { get; set; }
        public List<NurserySummaryAux> summaryElements { get; set; }

        public string date_str
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(date.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public SummaryHistory()
        {
            this.summaryElements = new List<NurserySummaryAux>();
        }
    }

    public class SummaryHistoryResult : Result
    {
        public SummaryHistory data { get; set; }
        public List<SummaryHistory> data_list { get; set; }

        public SummaryHistoryResult()
        {
            this.data = new SummaryHistory();
            this.data_list = new List<SummaryHistory>();
        }
    }

    public class InternmentResult : Result
    {
        public InternmentAux data { get; set; }
        public List<InternmentAux> data_list { get; set; }

        public InternmentResult()
        {
            data = new InternmentAux();
            data_list = new List<InternmentAux>();
        }
    }

    public class NurserySummaryAux
    {
        public int id { get; set; }
        public DateTime? date { get; set; }
        public string value { get; set; }
        public NurseryMeasurementAux measurement { get; set; }
        public string date_str
        {
            get
            {
                return date.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(date.Value.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX")) : "Nunca";
            }
        }

        public NurserySummaryAux()
        {
            this.measurement = new NurseryMeasurementAux();
        }
    }
    public class ApplicationAux
    {
        public DateTime applicationDate { get; set; }
        public List<MedicalIndicationAux> indications { get; set; }
        public string applicationDateString
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(this.applicationDate.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public ApplicationAux()
        {
            this.indications = new List<MedicalIndicationAux>();
        }
    }

    public class ApplicationResult : Result
    {
        public ApplicationAux data { get; set; }
        public List<ApplicationAux> data_list { get; set; }
        public ApplicationResult()
        {
            this.data = new ApplicationAux();
            this.data_list = new List<ApplicationAux>();
        }
    }

    public class MedicalIndicationAux
    {
        public int id { get; set; }
        public string concept { get; set; }
        public DateTime? lastApplication { get; set; }
        public bool apply { get; set; }
        public string lastApplicationString
        {
            get
            {
                return this.lastApplication.HasValue ?
                    TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(lastApplication.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX")) : "Nunca";
            }
        }

        public bool deleted { get; set; } = false;
    }

    public class InternmentAux
    {
        public int id { get; set; }
        public OrderAux orderAux { get; set; }
        public PatientAux patientAux { get; set; }
        public PackageAux packageAux { get; set; }
        public DateTime internmentDate { get; set; }
        public List<MedicalIndicationAux> medicalIndicationAux { get; set; }
        public List<NurserySummaryAux> nurserySummaryAux { get; set; }
        public string room { get; set; }
        public string internmentDateString
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(internmentDate.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }

        public InternmentAux()
        {
            orderAux = new OrderAux();
            patientAux = new PatientAux();
            packageAux = new PackageAux();
            nurserySummaryAux = new List<NurserySummaryAux>();
            medicalIndicationAux = new List<MedicalIndicationAux>();
        }
    }

    public class vwInternmentAux
    {
        public int id { get; set; }
        public int EmployeeId { get; set; }
        public int PatientId { get; set; }
        public decimal Total { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int ClinicId { get; set; }
        public string PatientName { get; set; }
        public string MedicName { get; set; }
        public string Name { get; set; }
        public string sCreated { get; set; }
        public string sUpdated { get; set; }

        public vwInternmentAux()
        {
        }
    }
    public class vwInternmentResult : Result
    {
        public vwInternmentAux data { get; set; }
        public List<vwInternmentAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public vwInternmentResult()
        {
            this.data = new vwInternmentAux();
            this.data_list = new List<vwInternmentAux>();
            this.total = new NumericResult();
        }
    }

    public class InternmentMaterialAux
    {
        public int id { get; set; }
        public int idInternment { get; set; }
        public int ConceptId { get; set; }
        public int MedicId { get; set; }
        public int ClinicId { get; set; }
        public int Quantity { get; set; }
        public string Medicname { get; set; }
        public string Decree { get; set; }
        public DateTime Created { get; set; }
        public int EmployeeCreatedId { get; set; }
        public int StatusId { get; set; }
        public DateTime Updated { get; set; }

        public EmployeeAux EmployeeAux { get; set; }
        public ConceptAux ConceptAux { get; set; }
        public StatusAux StatusAux { get; set; }
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
        public InternmentMaterialAux()
        {
            ConceptAux = new ConceptAux();
            EmployeeAux = new EmployeeAux();
            StatusAux = new StatusAux();
        }
    }
    public class InternmentMaterialResult : Result
    {
        public InternmentMaterialAux data { get; set; }
        public List<InternmentMaterialAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public InternmentMaterialResult()
        {
            this.data = new InternmentMaterialAux();
            this.data_list = new List<InternmentMaterialAux>();
            this.total = new NumericResult();
        }
    }


    public class RequestMaterialAux
    {
        public int id { get; set; }
        public int StatusId { get; set; }
        public DateTime Created { get; set; }
        public int EmployeeCreatedId { get; set; }
        public DateTime Updated { get; set; }
        public int EmployeeUpdatedId { get; set; }

        public EmployeeAux EmployeeCreatedAux { get; set; }
        public EmployeeAux EmployeeUpdatedAux { get; set; }
        public StatusAux StatusAux { get; set; }
        public List<RequestedMaterialAux> RequestedMaterialAux { get; set; }
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
        public RequestMaterialAux()
        {
            EmployeeCreatedAux = new EmployeeAux();
            EmployeeUpdatedAux = new EmployeeAux();
            StatusAux = new StatusAux();
            RequestedMaterialAux = new List<RequestedMaterialAux>();
        }
    }
    public class RequestMaterialResult : Result
    {
        public RequestMaterialAux data { get; set; }
        public List<RequestMaterialAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public RequestMaterialResult()
        {
            this.data = new RequestMaterialAux();
            this.data_list = new List<RequestMaterialAux>();
            this.total = new NumericResult();
        }
    }

    public class RequestedMaterialAux
    {
        public int id { get; set; }
        public int RequestId { get; set; }
        public int ConceptId { get; set; }
        public int QuantityRequested { get; set; }
        public int QuantityRestoked { get; set; }

        public ConceptAux ConceptAux { get; set; }
        public RequestedMaterialAux()
        {
            ConceptAux = new ConceptAux();
        }
    }
    public class RequestedMaterialResult : Result
    {
        public RequestedMaterialAux data { get; set; }
        public List<RequestedMaterialAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public RequestedMaterialResult()
        {
            this.data = new RequestedMaterialAux();
            this.data_list = new List<RequestedMaterialAux>();
            this.total = new NumericResult();
        }
    }
    public class InternmentHelper
    {
        public static InternmentResult GetInternment(int orderId)
        {
            InternmentResult result = new InternmentResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var order = db.Orders.Where(or => or.id == orderId).FirstOrDefault();
                    if (order != null)
                    {
                        var internment = order.Internment.FirstOrDefault();
                        InternmentAux aux = new InternmentAux();
                        DataHelper.fill(aux, internment);
                        DataHelper.fill(aux.orderAux, order);
                        DataHelper.fill(aux.packageAux, internment.Package);
                        DataHelper.fill(aux.patientAux, internment.Patient);
                        DataHelper.fill(aux.patientAux.personAux, internment.Patient.Person);

                        foreach (var measurement in db.NurseryMeasurements)
                        {
                            NurserySummaryAux summary = new NurserySummaryAux();
                            DataHelper.fill(summary.measurement, measurement);
                            if (internment.NurserySummary.Where(s => s.NurseryMeasurementId == measurement.id).Any())
                            {
                                summary.date = internment.NurserySummary.Where(s => s.NurseryMeasurementId == measurement.id).OrderByDescending(ns => ns.Date).Select(ns => ns.Date).FirstOrDefault();
                            }
                            aux.nurserySummaryAux.Add(summary);
                        }

                        foreach (var medicalIndication in internment.MedicalIndication.Where(mi => !mi.Deleted))
                        {
                            MedicalIndicationAux indAux = new MedicalIndicationAux();
                            DataHelper.fill(indAux, medicalIndication);
                            if (medicalIndication.Applications.Any())
                            {
                                indAux.lastApplication = medicalIndication.Applications.OrderByDescending(a => a.ApplicationDate).Select(a => a.ApplicationDate).FirstOrDefault();
                            }
                            aux.medicalIndicationAux.Add(indAux);
                        }
                        result.data = aux;
                        result.success = true;
                    }
                    else
                    {
                        result.success = true;
                        result.message = "Internamiento no encontrado.";
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

        public static Result SaveNurserySummary(int internment, List<NurserySummaryAux> summary)
        {
            Result result = new Result();
            DateTime now = DateTime.UtcNow;
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    foreach (var summaryElement in summary)
                    {
                        if (!string.IsNullOrEmpty(summaryElement.value))
                        {
                            NurserySummary newSummary = db.NurserySummary.Create();
                            newSummary.Date = now;
                            newSummary.InternmentId = internment;
                            newSummary.NurseryMeasurementId = summaryElement.measurement.id;
                            newSummary.Value = summaryElement.value;
                            db.NurserySummary.Add(newSummary);
                        }
                    }
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

        public static Result SaveIndications(int internment, List<MedicalIndicationAux> indications)
        {
            Result result = new Result();
            DateTime now = DateTime.UtcNow;
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (db.Internment.Where(i => i.id == internment).Any())
                    {
                        Applications app = null;
                        if (indications.Where(i => i.apply).Any())
                        {
                            app = db.Applications.Create();
                            app.ApplicationDate = now;
                            db.Applications.Add(app);
                        }

                        foreach (MedicalIndicationAux indication in indications)
                        {
                            MedicalIndication dbMedicalIndication;
                            if (indication.id == 0)
                            {
                                //Si no tiene Id creamos una nueva indicacion
                                dbMedicalIndication = db.MedicalIndication.Create();
                                dbMedicalIndication.InternmentId = internment;
                                dbMedicalIndication.Concept = indication.concept;
                                dbMedicalIndication.Created = now;
                                dbMedicalIndication.Updated = now;
                                db.MedicalIndication.Add(dbMedicalIndication);

                            }
                            else
                            {
                                //Si tiene ID validamos que sea edicion o borrado
                                dbMedicalIndication = db.MedicalIndication.Where(i => i.id == indication.id).FirstOrDefault();
                                if (indication.deleted)
                                {
                                    //Borrado
                                    if (dbMedicalIndication != null)
                                    {
                                        dbMedicalIndication.Deleted = true;
                                        dbMedicalIndication.Updated = now;

                                    }
                                }
                                else
                                {
                                    //Edición - En edición se borra el registro anterior y se genera uno nuevo
                                    if (dbMedicalIndication.Concept != indication.concept)
                                    {
                                        //Borrado
                                        dbMedicalIndication.Deleted = true;
                                        dbMedicalIndication.Updated = now;

                                        db.SaveChanges();
                                        //Creación nuevo registro
                                        dbMedicalIndication = db.MedicalIndication.Create();
                                        dbMedicalIndication.InternmentId = internment;
                                        dbMedicalIndication.Concept = indication.concept;
                                        dbMedicalIndication.Created = now;
                                        dbMedicalIndication.Updated = now;
                                        db.MedicalIndication.Add(dbMedicalIndication);


                                    }
                                }
                            }
                            //Verificamos si hay que crear un registro de aplicado
                            if (app != null && indication.apply)
                            {
                                app.MedicalIndication.Add(dbMedicalIndication);
                            }

                        }
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Ocurrió un error al intentar conectarse a la base de datos. vuelva a intentar más tarde.";
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

        public static Result SaveMedicalNotes(int internment, string note)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var medicalNote = db.MedicalNotes.Create();
                    medicalNote.InternmentId = internment;
                    medicalNote.Note = note;
                    medicalNote.Created = DateTime.UtcNow;
                    db.MedicalNotes.Add(medicalNote);
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

        public static ApplicationResult GetIndicationsForInternment(int internmentId)
        {
            ApplicationResult result = new ApplicationResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var internment = db.Internment.Where(i => i.id == internmentId).FirstOrDefault();
                    if (internment != null)
                    {
                        //Obtenemos todas las aplicaciones realizadas
                        var applications = internment.MedicalIndication.SelectMany(mi => mi.Applications).Distinct();
                        foreach (Applications application in applications)
                        {
                            ApplicationAux aux = new ApplicationAux();
                            DataHelper.fill(aux, application);
                            foreach (MedicalIndication indication in application.MedicalIndication)
                            {
                                MedicalIndicationAux indicationAux = new MedicalIndicationAux();
                                DataHelper.fill(indicationAux, indication);
                                aux.indications.Add(indicationAux);
                            }
                            result.data_list.Add(aux);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.message = "Internamiento no encontrado.";
                        result.success = false;
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

        public static MedicalNoteResult GetMedicalNotes(int internment)
        {
            MedicalNoteResult result = new MedicalNoteResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (db.Internment.Where(i => i.id == internment).Any())
                    {
                        var query = db.MedicalNotes.Where(mn => mn.InternmentId == internment).OrderByDescending(mn => mn.Created);
                        foreach (var medicalNote in query)
                        {
                            MedicalNoteAux aux = new MedicalNoteAux();
                            DataHelper.fill(aux, medicalNote);
                            result.data_list.Add(aux);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Internamiento no encontrado.";
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

        public static SummaryHistoryResult GetNurserySummaryHistory(int internment)
        {
            SummaryHistoryResult result = new SummaryHistoryResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (db.Internment.Where(i => i.id == internment).Any())
                    {
                        List<DateTime> dates = db.NurserySummary.Where(mn => mn.InternmentId == internment).OrderByDescending(mn => mn.Date).GroupBy(mn => mn.Date).Select(gp => gp.FirstOrDefault()).Select(mn => mn.Date).ToList();
                        foreach (DateTime date in dates)
                        {
                            SummaryHistory history = new SummaryHistory();
                            history.date = date;
                            foreach (var NurserySummary in db.NurserySummary.Where(mn => mn.InternmentId == internment && mn.Date == date))
                            {
                                NurserySummaryAux aux = new NurserySummaryAux();
                                DataHelper.fill(aux, NurserySummary);
                                DataHelper.fill(aux.measurement, NurserySummary.NurseryMeasurements);
                                history.summaryElements.Add(aux);
                            }
                            result.data_list.Add(history);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Internamiento no encontrado.";
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

        public static Result SaveInternmentMaterial(List<OrderConceptAux> AddedConcepts, int OrderId, int CurentEmployee)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var InternmentDb = db.Internment.Where(i => i.OrderId == OrderId).FirstOrDefault();
                    if (InternmentDb != null)
                    {
                        if (AddedConcepts != null)
                        {
                            var IVAConfig = ParameterHelper.GetTax();
                            if (IVAConfig.success)
                            {
                                decimal TotalNewCharge = 0;
                                foreach (var itemConcept in AddedConcepts)
                                {
                                    #region revicion para ver ver si se le cobrara extra o no por el concepto agregado
                                    /*buscamos la cantidad de ese concepto ah sido usada en el intenamiento hasta ahorita*/
                                    /*le sumamos la cantidad que se le va a agregar ahorita*/
                                    int QuantityUsed =
                                        InternmentDb
                                        .InternmentMaterial
                                        .Where(i => i.ConceptId == itemConcept.conceptId.Value)
                                        .Sum(i => i.Quantity) + itemConcept.quantity;
                                    /*buscamos la cantidad de ese concepto que esta incluida en el paquete*/
                                    int QuantityIncluded =
                                        InternmentDb.Package.PackageConcept
                                        .Where(o => o.ConceptId == itemConcept.conceptId.Value)
                                        .Select(i => i.Quantity).FirstOrDefault();
                                    /*buscamos la cantidad de ese producto que se ah cobrado hasta ahorita*/
                                    int QuantityCharged =
                                        InternmentDb.Orders.OrdersConcepts
                                        .Where(o => o.ConceptId == itemConcept.conceptId.Value)
                                        .Sum(o => o.Quantity);
                                    /*si la cantidad usada supera a la cantidad que se incluye en el paquete*/
                                    if (QuantityUsed > QuantityIncluded)
                                    {
                                        /*Buscamos el concepto para saber su costo y precio*/
                                        var ConceptDb = db.Concept.Where(c => c.id == itemConcept.conceptId.Value).FirstOrDefault();

                                        /*la cantidad que se le va a cobrar ahora es la que se ah acumulado menos la incluida menos la que ya se ah cobrado*/
                                        int NewQuantityToCharge = QuantityUsed - QuantityIncluded - QuantityCharged;
                                        var newOrderConcept = new OrdersConcepts();
                                        /*Ejemplo, 400(10/100)  ya que el descuento es en porcentaje*/
                                        decimal Discount = 0;
                                        if (ConceptDb.Discount > 0)
                                        {
                                            Discount = ConceptDb.Price * (ConceptDb.Discount / 100);
                                        }

                                        /*Ejemplo, (400-Discount)*(16/100) */
                                        decimal ConceptIVA = 0;
                                        if (ConceptDb.Iva)
                                        {
                                            ConceptIVA = (ConceptDb.Price - Discount) * (IVAConfig.value / 100);
                                        }

                                        newOrderConcept.ConceptId = itemConcept.conceptId.Value;
                                        newOrderConcept.MedicId = itemConcept.medicId;
                                        newOrderConcept.ClinicId = itemConcept.ClinicId;
                                        newOrderConcept.Quantity = NewQuantityToCharge;
                                        newOrderConcept.Discount = itemConcept.discount;
                                        newOrderConcept.Price = ConceptDb.Price;
                                        newOrderConcept.Cost = ConceptDb.Cost;
                                        newOrderConcept.Total = (ConceptDb.Price - Discount + ConceptIVA) * NewQuantityToCharge;
                                        //newOrderConcept.Scheduled = concept.scheduled;
                                        newOrderConcept.Medicname = itemConcept.medicname;
                                        newOrderConcept.Decree = itemConcept.decree;
                                        newOrderConcept.Created = DateTime.UtcNow;
                                        newOrderConcept.Updated = DateTime.UtcNow;

                                        TotalNewCharge = TotalNewCharge + newOrderConcept.Total;
                                        InternmentDb.Orders.Paid = false;
                                        InternmentDb.Orders.OrdersConcepts.Add(newOrderConcept);
                                    }
                                    #endregion

                                    var newMaterialDb = new InternmentMaterial();
                                    newMaterialDb.ConceptId = itemConcept.conceptId.Value;
                                    newMaterialDb.MedicId = itemConcept.medicId;
                                    newMaterialDb.ClinicId = itemConcept.ClinicId;
                                    newMaterialDb.Quantity = itemConcept.quantity;
                                    newMaterialDb.Medicname = itemConcept.medicname;
                                    newMaterialDb.Decree = itemConcept.decree;
                                    newMaterialDb.Created = DateTime.UtcNow;
                                    newMaterialDb.EmployeeCreatedId = CurentEmployee;

                                    InternmentDb.InternmentMaterial.Add(newMaterialDb);
                                }
                                InternmentDb.Orders.Total = InternmentDb.Orders.Total + TotalNewCharge;
                                db.SaveChanges();
                                result.success = true;
                            }
                            result.message = "Parametro IVA no encontrado.";
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Internamiento no encontrado.";
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

        public static InternmentMaterialResult GetInternmentMaterial(int OrderId)
        {
            InternmentMaterialResult result = new InternmentMaterialResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var InternmentDb = db.Internment.Where(i => i.OrderId == OrderId).FirstOrDefault();
                    if (InternmentDb != null)
                    {
                        if (InternmentDb.InternmentMaterial != null)
                        {
                            foreach (var InternmentMaterialDb in InternmentDb.InternmentMaterial)
                            {
                                var itemAux = new InternmentMaterialAux();
                                DataHelper.fill(itemAux, InternmentMaterialDb);
                                DataHelper.fill(itemAux.ConceptAux, InternmentMaterialDb.Concept);
                                DataHelper.fill(itemAux.ConceptAux.categoryAux, InternmentMaterialDb.Concept.Category);
                                DataHelper.fill(itemAux.EmployeeAux, InternmentMaterialDb.Employee);
                                DataHelper.fill(itemAux.EmployeeAux.personAux, InternmentMaterialDb.Employee.Person);
                                result.data_list.Add(itemAux);
                            }
                            result.success = true;
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Internamiento no encontrado.";
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

        public static RequestMaterialResult GetRequest(DTParameterModel filter, int ClinicId)
        {
            RequestMaterialResult result = new RequestMaterialResult();

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
                    var query = db.RequestMaterial.AsQueryable();
                    query = query.Where(q => q.ClinicId == ClinicId);

                    #region filtros
                    /*No se usan filtros en esta tabla*/
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "id")
                        {
                            query = query.OrderByDescending(q => q.id);
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
                        if (orderColumn == "StatusAux.Name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Status.Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Status.Name);
                            }
                        }
                    }
                    #endregion

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (var itemDb in query.ToList())
                    {
                        var aux = new RequestMaterialAux();
                        DataHelper.fill(aux, itemDb);
                        DataHelper.fill(aux.StatusAux, itemDb.Status);
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

        public static RequestMaterialResult GetRequestedMaterial(int ClinicId, int? RequestId = null)
        {
            RequestMaterialResult result = new RequestMaterialResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (RequestId.HasValue)
                    {
                        /*en caso de que se envie un id request, lo buscamos por ese Id*/
                        var requestDb = db.RequestMaterial.Where(m => m.id == RequestId.Value).FirstOrDefault();
                        if (requestDb != null)
                        {
                            DataHelper.fill(result.data, requestDb);
                            DataHelper.fill(result.data.StatusAux, requestDb.Status);
                            foreach (var itemDb in requestDb.RequestedMaterial)
                            {
                                var aux = new RequestedMaterialAux();
                                DataHelper.fill(aux, itemDb);
                                DataHelper.fill(aux.ConceptAux, itemDb.Concept);
                                result.data.RequestedMaterialAux.Add(aux);
                            }
                        }
                        else
                        {
                            result.message = "Solicitud no encontrada.";
                        }
                    }
                    else
                    {
                        /*en caso de que el id sea null, obtenemos los conceptos que no */
                        var materialForRequestDb = db.vwMaterialForRequest.Where(m => m.ClinicId == ClinicId);
                        foreach (var itemDb in materialForRequestDb)
                        {
                            var aux = new RequestedMaterialAux();
                            aux.QuantityRequested = itemDb.Quantity.Value;
                            aux.ConceptId = itemDb.ConceptId;
                            aux.ConceptAux.name = itemDb.Name;
                            result.data.RequestedMaterialAux.Add(aux);
                        }

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

        public static RequestedMaterialResult SendRequest(int ClinicId, int employeeId)
        {
            RequestedMaterialResult result = new RequestedMaterialResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var materialForRequestDb = db.vwMaterialForRequest.Where(m => m.ClinicId == ClinicId);
                    var InternmentMaterialDb = db.InternmentMaterial.Where(m => m.ClinicId == ClinicId && m.Requested == false);

                    var statusSol = db.Status.Where(s => s.Name == "Solicitado").FirstOrDefault();
                    if (statusSol != null)
                    {
                        var requestDb = new RequestMaterial();
                        requestDb.StatusId = statusSol.id;
                        requestDb.Created = DateTime.UtcNow;
                        requestDb.EmployeeCreatedId = employeeId;
                        requestDb.Updated = DateTime.UtcNow;
                        requestDb.EmployeeUpdatedId = employeeId;
                        requestDb.ClinicId = ClinicId;

                        foreach (var itemDb in materialForRequestDb)
                        {
                            var aux = new RequestedMaterial();
                            aux.ConceptId = itemDb.ConceptId;
                            aux.QuantityRequested = itemDb.Quantity.Value;

                            requestDb.RequestedMaterial.Add(aux);
                        }

                        foreach (var itemDb in InternmentMaterialDb)
                        {
                            itemDb.Requested = true;
                        }

                        db.RequestMaterial.Add(requestDb);
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.message = "Estatus 'Solicitado' no encontrado.";
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

        public static Result RestockRequest(RequestMaterialAux RequestAux)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var requestDb = db.RequestMaterial.Where(r => r.id == RequestAux.id).FirstOrDefault();

                    var statusTerm = db.Status.Where(s => s.Name == "Terminado").FirstOrDefault();
                    if (statusTerm != null && requestDb != null)
                    {
                        requestDb.EmployeeUpdatedId = RequestAux.EmployeeUpdatedId;
                        requestDb.Updated = DateTime.UtcNow;
                        requestDb.StatusId = statusTerm.id;

                        foreach (var materialAux in RequestAux.RequestedMaterialAux)
                        {
                            var materialDb = db.RequestedMaterial.Where(m => m.id == materialAux.id).First();
                            if (materialDb != null)
                            {
                                materialDb.QuantityRestoked = materialAux.QuantityRestoked;
                            }
                        }

                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.message = "Estatus 'Terminado' no encontrado.";
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