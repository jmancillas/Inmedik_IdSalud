using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace INMEDIK.Controllers
{
    [Authorize]
    [PSAuthorize]
    public class ScheduleController : Controller
    {
        [MenuData]
        public ActionResult Index()
        {
            var resultChronicDisease = ChronicDiseaseHelper.GetChronicDiseases(false);
            var resultDisease = DiseaseHelper.GetDiseases(false);
            var resultMedics = EmployeeHelper.GetEmployeesByRol("Medic");
            var resultStatus = StatusHelper.getStatus();
            var RefreshTime = ParameterHelper.GetRefreshTimeAgenda();

            ViewBag.Disease = JsonUtility.ObjectToJsonJSEncode(resultDisease.data_list);
            ViewBag.ChronicDisease = JsonUtility.ObjectToJsonJSEncode(resultChronicDisease.data_list);
            ViewBag.Medics = JsonUtility.ObjectToJsonJSEncode(resultMedics.data_list);
            ViewBag.Status = JsonUtility.ObjectToJsonJSEncode(resultStatus.data_list);

            ViewBag.RefreshTime = RefreshTime.value;
            ViewBag.PatientId = Request.Params["PatientId"];
            return View();
        }
        //public JsonResult GetSchedule(DTParameterModel model)
        //{
        //    ClinicResult CurrentClinic = new ClinicResult();
        //    var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
        //    var currentEmployee = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);
        //    string tabClicked = Request.Params["tabClicked"];
        //    string PatientId = Request.Params["PatientId"];
        //    var Sysdate = TimeZoneInfo.ConvertTimeFromUtc(
        //            DateTime.UtcNow,
        //            TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
        //            ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
        //    vwScheduleResult result = ScheduleHelper.GetSchedule(model, tabClicked, PatientId, CurrentUser.User.rolAux.name, CurrentUser.User.rolAux.id, currentEmployee.data.id, CurrentClinic.data.id);
        //    return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value, SysDate = Sysdate }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        //}

        public JsonResult GetPreviousConsults(DTParameterModel model)
        {
            string PatientId = Request.Params["PatientId"];
            PreviousConsultResult result = ScheduleHelper.GetPreviousResult(model,PatientId);
                return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult LoadPreviousConsult(int id)
        {
            LoadPreviousConsultResult LoadPreviousConsultResult = ScheduleHelper.LoadPreviousConsult(id);
            return Json(new { data = LoadPreviousConsultResult.data, success = LoadPreviousConsultResult.success, message = LoadPreviousConsultResult.message });
        }

        public JsonResult LoadDetail(string CategoryName, int id)
        {
            switch (CategoryName)
            {
                case "Consultas":
                    ConsultResult ConsultResult = ScheduleHelper.LoadDetailConsult(id);
                    return Json(new { data = ConsultResult.data, success = ConsultResult.success, message = ConsultResult.message }, JsonRequestBehavior.DenyGet);
                    //ViewBag.DenominationsList = JsonUtility.ObjectToJsonJSEncode(result.data_list.Where(d => d.Name != "CC" && d.Name != "Vales"));
                    break;
                case "Servicios":
                    ServiceResult ServiceResult = ScheduleHelper.LoadDetailService(id);
                    return Json(new { data = ServiceResult.data, success = ServiceResult.success, message = ServiceResult.message }, JsonRequestBehavior.DenyGet);
                    break;
                case "Exámenes":
                    ExamResult ExamResult = ScheduleHelper.LoadDetailExams(id);
                    return Json(new { data = ExamResult.data_list, success = ExamResult.success, message = ExamResult.message }, JsonRequestBehavior.DenyGet);
                    //return Json(new { success = false, message = "No se encontro la categoría." }, JsonRequestBehavior.DenyGet);
                    break;
                default:
                    ConsultResult result = ScheduleHelper.LoadDetailConsult(id);
                    return Json(new { success = false, message = "No se encontro la categoría." }, JsonRequestBehavior.DenyGet);
                    break;
            }
        }
        public JsonResult SaveConsult(ConsultAux ConsultAux)
        {
            Result result = new Result();
            EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(UserHelper.GetUser(User.Identity.Name).Id);
            ConsultAux.NurseId = EmployeeRess.data.id;
            result = ScheduleHelper.SaveConsult(ConsultAux, EmployeeRess.data.userAux.rolAux.name);
            return Json(new { success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        public JsonResult SaveService(ServiceAux ServiceAux)
        {
            Result result = new Result();
            EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(UserHelper.GetUser(User.Identity.Name).Id);
            ServiceAux.NurseId = EmployeeRess.data.id;
            result = ScheduleHelper.SaveService(ServiceAux);
            return Json(new { success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        public JsonResult SaveExam(List<ExamAux> ExamAux)
        {
            Result result = new Result();
            EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(UserHelper.GetUser(User.Identity.Name).Id);
            
            result = ScheduleHelper.SaveExams(ExamAux, EmployeeRess.data.id);
            return Json(new { success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        public FileResult GetAdImage(int adId)
        {
            FileDbResult FileRes = AdsHelper.GetFileDbByAdId(adId);

            var dir = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files/");
            return File(Path.Combine(dir + FileRes.data.Name), FileRes.data.ContentType);
        }

        public FileStreamResult BuildPrenscription(int consultId, int noteid)
        { 
            string userName = HttpContext.User.Identity.Name;
            string htmlPrenscription = ScheduleHelper.BuildPrenscription(consultId,userName, noteid).string_value;
            byte[] byteArray = PdfManager.HtmlToPdFNoMargin(htmlPrenscription);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }
    }
}