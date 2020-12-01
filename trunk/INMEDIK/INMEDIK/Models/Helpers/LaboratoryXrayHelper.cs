using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMEDIK.Models.Helpers
{

    public class ParentExamClass
    {
        public int id { get; set; }
        public int PatientId { get; set; }
        public int OrderConceptId { get; set; }
        public string Age { get; set; }
        public string Comment { get; set; }
        public int StatusId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public ExamAux ExamAux { get; set; }
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
        public ParentExamClass()
        {
            StatusAux = new StatusAux();
            ExamAux = new ExamAux();
        }
    }
    public class LaboratoryAux : ParentExamClass
    {
        public List<FileDbAux> FileDbAux { get; set; }
        public FormDataAux formDataAux { get; set; }
        public bool hasFormData { get; set; }
        public LaboratoryAux()
        {
            FileDbAux = new List<FileDbAux>();
            formDataAux = new FormDataAux();
        }
    }
    public class LaboratoryResult : Result
    {
        public LaboratoryAux data { get; set; }
        public List<LaboratoryAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public LaboratoryResult()
        {
            this.data = new LaboratoryAux();
            this.data_list = new List<LaboratoryAux>();
            this.total = new NumericResult();
        }
    }

    public class vwLaboratoryXrayAux : ParentExamClass
    {
        public string PatientName { get; set; }
        public string MedicName { get; set; }
        public string StatusName { get; set; }
        public string ConceptName { get; set; }
        public string Notes { get; set; }
        public int ExamId { get; set; }
        public string sCreated { get; set; }
        public string ExamDepartment { get; set; }
        public string sUpdated { get; set; }
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }

    }
    public class vwLaboratoryXrayResult : Result
    {
        public vwLaboratoryXrayAux data { get; set; }
        public List<vwLaboratoryXrayAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public vwLaboratoryXrayResult()
        {
            this.data = new vwLaboratoryXrayAux();
            this.data_list = new List<vwLaboratoryXrayAux>();
            this.total = new NumericResult();
        }
    }
    public class XrayAux : ParentExamClass
    {
        public XrayAux()
        {
        }
    }
    public class XrayResult : Result
    {
        public XrayAux data { get; set; }
        public List<XrayAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public XrayResult()
        {
            this.data = new XrayAux();
            this.data_list = new List<XrayAux>();
            this.total = new NumericResult();
        }
    }

    public class TemplateLabAux
    {
        public string Name { get; set; }
        public TemplateLabAux()
        {
        }
        public TemplateLabAux(string Name)
        {
            this.Name = Name;
        }
    }
    public class TemplateLabResult : Result
    {
        public TemplateLabAux data { get; set; }
        public List<TemplateLabAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public TemplateLabResult()
        {
            this.data = new TemplateLabAux();
            this.data_list = new List<TemplateLabAux>();
            this.total = new NumericResult();
        }
    }

    class LaboratoryXrayHelper
    {
        public static vwLaboratoryXrayResult GetExamByDepartment(DTParameterModel filter, int ClinicId, string ExamDepartment)
        {
            vwLaboratoryXrayResult result = new vwLaboratoryXrayResult();

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
                    //var query = db.vwLaboratoryXray.AsQueryable();
                    //query = query.Where(q => q.ExamDepartment == ExamDepartment);
                    //query = query.Where(q => q.ClinicId == ClinicId);

                    //#region filtros
                    //foreach (DTColumn column in filter.Columns)
                    //{
                    //    if (column.Data == "MedicName" && !String.IsNullOrEmpty(column.Search.Value))
                    //    {
                    //        query = query.Where(q => q.MedicName.Contains(column.Search.Value));
                    //    }
                    //    if (column.Data == "PatientName" && !String.IsNullOrEmpty(column.Search.Value))
                    //    {
                    //        query = query.Where(q => q.PatientName.Contains(column.Search.Value));
                    //    }
                    //    if (column.Data == "sUpdated" && !String.IsNullOrEmpty(column.Search.Value))
                    //    {
                    //        query = query.Where(q => q.sUpdated.Contains(column.Search.Value));
                    //    }
                    //    if (column.Data == "StatusName" && !String.IsNullOrEmpty(column.Search.Value))
                    //    {
                    //        query = query.Where(q => q.StatusName.Contains(column.Search.Value));
                    //    }
                    //    if (column.Data == "ConceptName" && !String.IsNullOrEmpty(column.Search.Value))
                    //    {
                    //        query = query.Where(q => q.ConceptName.Contains(column.Search.Value));
                    //    }
                    //}
                    //#endregion
                    //#region orden
                    //if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    //{
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
                    //    if (orderColumn == "sUpdated")
                    //    {
                    //        if (order.ToUpper() == "ASC")
                    //        {
                    //            query = query.OrderBy(q => q.Updated);
                    //        }
                    //        else
                    //        {
                    //            query = query.OrderByDescending(q => q.Updated);
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
                    //}
                    //#endregion

                    //result.total.value = query.Count();

                    //query = query.Skip(filter.Start).Take(filter.Length);
                    //foreach (var itemDb in query.ToList())
                    //{
                    //    vwLaboratoryXrayAux aux = new vwLaboratoryXrayAux();
                    //    DataHelper.fill(aux, itemDb);
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
        #region Laboratory
        public static LaboratoryResult LoadLaboratoryById(int id)
        {
            LaboratoryResult result = new LaboratoryResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var labDb = db.Laboratory.Where(l => l.id == id).FirstOrDefault();
                    if (labDb != null)
                    {
                        DataHelper.fill(result.data, labDb);
                        DataHelper.fill(result.data.StatusAux, labDb.Status);
                        if (labDb.Exam.FormData.Any())
                        {
                            DataHelper.fill(result.data.formDataAux, labDb.Exam.FormData.FirstOrDefault());
                        }
                        DataHelper.fill(result.data.ExamAux, labDb.Exam);
                        DataHelper.fill(result.data.ExamAux.ConceptAux, labDb.Exam.Concept);
                        DataHelper.fill(result.data.ExamAux.PatientAux, labDb.Exam.Patient);
                        DataHelper.fill(result.data.ExamAux.PatientAux.personAux, labDb.Exam.Patient.Person);
                        //DataHelper.fill(result.data.ExamAux.MedicAux, labDb.Exam.Employee1);
                        //DataHelper.fill(result.data.ExamAux.MedicAux.personAux, labDb.Exam.Employee1.Person);

                        foreach (var itemFileDb in labDb.FileDb)
                        {
                            var aux = new FileDbAux();
                            DataHelper.fill(aux, itemFileDb);
                            result.data.FileDbAux.Add(aux);
                        }

                        result.success = true;
                    }
                    else
                    {
                        result.message = "Registro no encontrado.";
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
        public static LaboratoryResult SaveLaboratory(LaboratoryAux LaboratoryAux)
        {
            LaboratoryResult result = new LaboratoryResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var labDb = db.Laboratory.Where(l => l.id == LaboratoryAux.id).FirstOrDefault();
                    var statTermDb = db.Status.Where(s => s.Name == "Terminado").FirstOrDefault();
                    if (labDb != null)
                    {
                        
                            labDb.Comment = LaboratoryAux.Comment ?? string.Empty;
                            labDb.Updated = DateTime.UtcNow;

                            if (labDb.Exam.FormData.Any())
                            {
                                FormData temp = labDb.Exam.FormData.FirstOrDefault();
                                temp.UserId = LaboratoryAux.formDataAux.userId;
                                temp.JsonString = LaboratoryAux.formDataAux.JsonString;
                            }
                            else
                            {
                                labDb.Exam.FormData.Add(new FormData()
                                {
                                    JsonString = LaboratoryAux.formDataAux.JsonString,
                                    UserId = LaboratoryAux.formDataAux.userId
                                });
                            }

                            if (LaboratoryAux.StatusId == statTermDb.id)
                            {
                                labDb.StatusId = statTermDb.id;
                            }

                            db.SaveChanges();
                            result.success = true;
                        

                    }
                    else
                    {
                        result.message = "Registro no encontrado.";
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
        public static LaboratoryResult AssignFileToLab(int id, string FileName, string OriginalName, string ContentType)
        {
            LaboratoryResult result = new LaboratoryResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var labDb = db.Laboratory.Where(l => l.id == id).FirstOrDefault();
                    if (labDb != null)
                    {
                        FileDb FileDb = new FileDb();
                        FileDb.Name = FileName;
                        FileDb.OriginalName = OriginalName;
                        FileDb.ContentType = ContentType;
                        FileDb.Created = DateTime.UtcNow;
                        labDb.FileDb.Add(FileDb);
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.message = "Registro no encontrado.";
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
        public static Result RemoveAssignFileToLab(FileDbAux FileDbAux, int id)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var labDb = db.Laboratory.Where(l => l.id == id).FirstOrDefault();
                    if (labDb != null)
                    {
                        FileDb FileDb = db.FileDb.Where(f => f.id == FileDbAux.id).FirstOrDefault();
                        if (FileDb != null)
                        {
                            labDb.FileDb.Remove(FileDb); //borramos la relacion
                            db.FileDb.Remove(FileDb); // borramos de la tabla de archivos
                            db.SaveChanges();
                            result.success = true;
                        }
                        else
                        {
                            result.message = "Registro de archivo no encontrado.";
                        }
                    }
                    else
                    {
                        result.message = "Registro de laboratorio no encontrado.";
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
        #endregion
        #region Xray
        public static XrayResult LoadXrayById(int id)
        {
            XrayResult result = new XrayResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var XrayDb = db.Xray.Where(l => l.id == id).FirstOrDefault();
                    if (XrayDb != null)
                    {
                        DataHelper.fill(result.data, XrayDb);
                        DataHelper.fill(result.data.ExamAux, XrayDb.Exam);
                        DataHelper.fill(result.data.ExamAux.ConceptAux, XrayDb.Exam.Concept);
                        DataHelper.fill(result.data.ExamAux.PatientAux, XrayDb.Exam.Patient);
                        DataHelper.fill(result.data.ExamAux.PatientAux.personAux, XrayDb.Exam.Patient.Person);
                        //DataHelper.fill(result.data.ExamAux.MedicAux, XrayDb.Exam.Employee1);
                        //DataHelper.fill(result.data.ExamAux.MedicAux.personAux, XrayDb.Exam.Employee1.Person);

                        result.success = true;
                    }
                    else
                    {
                        result.message = "Registro no encontrado.";
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
        public static XrayResult SaveXray(XrayAux XrayAux)
        {
            XrayResult result = new XrayResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var XrayDb = db.Xray.Where(l => l.id == XrayAux.id).FirstOrDefault();
                    if (XrayDb != null)
                    {
                        var StatTermDb = db.Status.Where(s => s.Name == "Terminado").FirstOrDefault();
                        if (StatTermDb != null)
                        {
                            XrayDb.StatusId = StatTermDb.id;
                            XrayDb.Comment = XrayAux.Comment ?? string.Empty;
                            XrayDb.Updated = DateTime.UtcNow;
                            db.SaveChanges();
                            result.success = true;
                        }
                        else
                        {
                            result.message = "Status 'Terminado' no encontrado.";

                        }
                    }
                    else
                    {
                        result.message = "Registro no encontrado.";
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
        #endregion
        public static TemplateLabResult getTemplatesLabList(DTParameterModel filter)
        {
            TemplateLabResult result = new TemplateLabResult();

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
                    //var query = db.vwLaboratoryXray.AsQueryable();
                    #region agregado de los nombres de los archivos
                    List<TemplateLabAux> query = new List<TemplateLabAux>();
                    query.Add(new TemplateLabAux("AMILASA SERICA.ods"));
                    query.Add(new TemplateLabAux("ANTIDOPING.ods"));
                    query.Add(new TemplateLabAux("ANTIGENO PROSTATICO.ods"));
                    query.Add(new TemplateLabAux("BH COPRO.ods"));
                    query.Add(new TemplateLabAux("BH EGO PHEP.ods"));
                    query.Add(new TemplateLabAux("BH EGO.ods"));
                    query.Add(new TemplateLabAux("BH QS EGO VDRL GPO.ods"));
                    query.Add(new TemplateLabAux("BH QS EGO.ods"));
                    query.Add(new TemplateLabAux("BH QS P LIPIDOS.ods"));
                    query.Add(new TemplateLabAux("BH QS P REUMATICO.ods"));
                    query.Add(new TemplateLabAux("BH QS PHEP.ods"));
                    query.Add(new TemplateLabAux("BH QS RF.ods"));
                    query.Add(new TemplateLabAux("BH QS RXFEB EGO VDRL GPO.ods"));
                    query.Add(new TemplateLabAux("BH QS RXFEB.ods"));
                    query.Add(new TemplateLabAux("BH QS.ods"));
                    query.Add(new TemplateLabAux("BH RXFEB COPRO.ods"));
                    query.Add(new TemplateLabAux("BH RXFEB EGO.ods"));
                    query.Add(new TemplateLabAux("BH RXFEB.ods"));
                    query.Add(new TemplateLabAux("BH TP QS.ods"));
                    query.Add(new TemplateLabAux("BH TP.ods"));
                    query.Add(new TemplateLabAux("BH VDRL GPO.ods"));
                    query.Add(new TemplateLabAux("BH.ods"));
                    query.Add(new TemplateLabAux("C. FARINGEO.xlr"));
                    query.Add(new TemplateLabAux("CARTA PRENUPCIALES.odt"));
                    query.Add(new TemplateLabAux("CITO QUIMICO.ods"));
                    query.Add(new TemplateLabAux("CITOLOGIA VAGINAL LAB. DE LEON.ods"));
                    query.Add(new TemplateLabAux("COPROCULTIVO.ods"));
                    query.Add(new TemplateLabAux("COPROLOGICO RX FEB.ods"));
                    query.Add(new TemplateLabAux("COPROLOGICO.ods"));
                    query.Add(new TemplateLabAux("CUANTIFICACION DE HGC.ods"));
                    query.Add(new TemplateLabAux("CUL. FARINGEO.xlr"));
                    query.Add(new TemplateLabAux("CULTIVO VAGINAL.xlr"));
                    query.Add(new TemplateLabAux("CURVA. TOLERANCIA.ods"));
                    query.Add(new TemplateLabAux("DEP. CREATININA.ods"));
                    query.Add(new TemplateLabAux("EGO COPRO.ods"));
                    query.Add(new TemplateLabAux("EGO..ods"));
                    query.Add(new TemplateLabAux("ELECTROLITOS.ods"));
                    query.Add(new TemplateLabAux("ENZIMAS CARDIACAS.ods"));
                    query.Add(new TemplateLabAux("ESPERMOGRAMA EGO GLU.ods"));
                    query.Add(new TemplateLabAux("HB. GLICOSILADA.ods"));
                    query.Add(new TemplateLabAux("HORMONA FOLICULO ESTIMULANTE (FSH).ods"));
                    query.Add(new TemplateLabAux("IGE.ods"));
                    query.Add(new TemplateLabAux("P TIROIDEO.ods"));
                    query.Add(new TemplateLabAux("P. BIOQ BH EGO.ods"));
                    query.Add(new TemplateLabAux("P. CARDIACO.ods"));
                    query.Add(new TemplateLabAux("P. COAGULACION.ods"));
                    query.Add(new TemplateLabAux("P. HEPATICO.ods"));
                    query.Add(new TemplateLabAux("P. HORMONAL.ods"));
                    query.Add(new TemplateLabAux("PER REUMATICO QS.ods"));
                    query.Add(new TemplateLabAux("PER REUMATICO RX FEB.ods"));
                    query.Add(new TemplateLabAux("PER. LIPIDOS P. HEP.ods"));
                    query.Add(new TemplateLabAux("PER. LIPIDOS.ods"));
                    query.Add(new TemplateLabAux("PER. PRENATAL.ods"));
                    query.Add(new TemplateLabAux("PIE.ods"));
                    query.Add(new TemplateLabAux("PRENUPCIALES.ods"));
                    query.Add(new TemplateLabAux("PROLACTINA.ods"));
                    query.Add(new TemplateLabAux("PROTEINAS EN ORINA 24 HRS.ods"));
                    query.Add(new TemplateLabAux("PRUEBA DE LA INFLUENZA.ods"));
                    query.Add(new TemplateLabAux("PSA TOTAL Y LIBRE.ods"));
                    query.Add(new TemplateLabAux("QS COL TRIG.ods"));
                    query.Add(new TemplateLabAux("QS EGO RXFEB.ods"));
                    query.Add(new TemplateLabAux("QS EGO.ods"));
                    query.Add(new TemplateLabAux("QS P LIPIDOS.ods"));
                    query.Add(new TemplateLabAux("QS RXFEB EGO.ods"));
                    query.Add(new TemplateLabAux("QS RXFEB.ods"));
                    query.Add(new TemplateLabAux("RXBEB COPRO.ods"));
                    query.Add(new TemplateLabAux("RXFEB EGO.ods"));
                    query.Add(new TemplateLabAux("RXFEB PIE.ods"));
                    query.Add(new TemplateLabAux("RXFEB.ods"));
                    query.Add(new TemplateLabAux("TP.ods"));
                    query.Add(new TemplateLabAux("TSH NEONATAL.ods"));
                    query.Add(new TemplateLabAux("UROCULTIVO.xlr"));
                    #endregion

                    #region filtros
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "Name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Name.Contains(column.Search.Value.ToUpper())).ToList();
                        }
                    }
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "Name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Name).ToList();
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Name).ToList();
                            }
                        }
                    }
                    #endregion

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length).ToList();
                    foreach (var itemDb in query.ToList())
                    {
                        TemplateLabAux aux = new TemplateLabAux();
                        DataHelper.fill(aux, itemDb);
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
        //public static PatientResult loadPatientExams(int id, int Ticket, int Clinica)
        //{
        //    PatientResult result = new PatientResult();
        //    using (dbINMEDIK db = new dbINMEDIK())
        //    {
        //        try
        //        {
        //            var PatientDb = db.Patient.Where(p => p.id == id).FirstOrDefault();
        //            if (PatientDb != null)
        //            {
        //                result = PatientHelper.GetPatient(id);
        //                /*Recorremos los laboratios ya que es lo que (por ahora)  es lo unico que se sube archivoz*/
        //                foreach (var labDb in PatientDb.Laboratory.Where(o => o.Exam.Orders.Ticket == Ticket && o.Exam.ClinicId == Clinica))
        //                {
        //                    var auxLab = new LaboratoryAux();
        //                    DataHelper.fill(auxLab, labDb);
        //                    DataHelper.fill(auxLab.StatusAux, labDb.Status);
        //                    DataHelper.fill(auxLab.ExamAux, labDb.Exam);
        //                    DataHelper.fill(auxLab.ExamAux.ConceptAux, labDb.Exam.Concept);
        //                    DataHelper.fill(auxLab.ExamAux.MedicAux, labDb.Exam.Employee1);
        //                    DataHelper.fill(auxLab.ExamAux.MedicAux.personAux, labDb.Exam.Employee1.Person);

        //                    if (labDb.Exam.FormData.Any())
        //                    {
        //                        auxLab.hasFormData = true;
        //                    }

        //                    foreach (var itemFileDb in labDb.FileDb)
        //                    {
        //                        var aux = new FileDbAux();
        //                        DataHelper.fill(aux, itemFileDb);
        //                        auxLab.FileDbAux.Add(aux);
        //                    }
        //                    result.data.LaboratoryAux.Add(auxLab);
        //                }
        //                if (result.data.LaboratoryAux.Count > 0)
        //                {
        //                    result.success = true;
        //                }
        //                else
        //                {
        //                    result.success = false;
        //                    result.message = "Registro no encontrado.";
        //                    result.data = null;
        //                }
        //            }
        //            else
        //            {
        //                result.message = "Registro no encontrado.";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            result.success = false;
        //            result.exception = ex;
        //            result.message = "Ocurrió un error inesperado. " + result.exception_message;
        //        }
        //    }
        //    return result;
        //}
        public static PatientResult loadPatientExams(int id)
        {
            PatientResult result = new PatientResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var PatientDb = db.Patient.Where(p => p.id == id).FirstOrDefault();
                    if (PatientDb != null)
                    {
                        result = PatientHelper.GetPatient(id);
                        /*Recorremos los laboratios ya que es lo que (por ahora)  es lo unico que se sube archivoz*/
                        foreach (var labDb in PatientDb.Laboratory)
                        {
                            var auxLab = new LaboratoryAux();
                            DataHelper.fill(auxLab, labDb);
                            DataHelper.fill(auxLab.StatusAux, labDb.Status);
                            DataHelper.fill(auxLab.ExamAux, labDb.Exam);
                            DataHelper.fill(auxLab.ExamAux.ConceptAux, labDb.Exam.Concept);
                            //DataHelper.fill(auxLab.ExamAux.MedicAux, labDb.Exam.Employee1);
                            //DataHelper.fill(auxLab.ExamAux.MedicAux.personAux, labDb.Exam.Employee1.Person);

                            if (labDb.Exam.FormData.Any())
                            {
                                auxLab.hasFormData = true;
                            }

                            foreach (var itemFileDb in labDb.FileDb)
                            {
                                var aux = new FileDbAux();
                                DataHelper.fill(aux, itemFileDb);
                                auxLab.FileDbAux.Add(aux);
                            }
                            result.data.LaboratoryAux.Add(auxLab);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.message = "Registro no encontrado.";
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
    }
}
