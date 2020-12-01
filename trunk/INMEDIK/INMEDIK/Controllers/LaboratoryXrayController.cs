using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace INMEDIK.Controllers
{
    public class LaboratoryXrayController : Controller
    {
        [Authorize]
        [PSAuthorize]
        public JsonResult GetExamByDepartment(DTParameterModel model)
        {
            string ExamDepartment = Request.Params["ExamDepartment"];
            vwLaboratoryXrayResult result = new vwLaboratoryXrayResult();
            ClinicResult CurrentClinic = new ClinicResult();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);

            if (CurrentUser.success)
            {
                result = LaboratoryXrayHelper.GetExamByDepartment(model, CurrentClinic.data.id.Value, ExamDepartment);
            }
            
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value, SysDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")).ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("es-MX")) }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        #region Laboratory
        [MenuData]
        [Authorize]
        [PSAuthorize]
        public ActionResult Laboratory()
        {
            ClinicResult CurrentClinic = new ClinicResult();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            var resultStatus = StatusHelper.getStatus();
            //TODO: Meter nuevo permiso para configurar laboratorios.
            ViewBag.Rol = CurrentUser.User.rolAux.name;
            ViewBag.Status = JsonUtility.ObjectToJsonJSEncode(resultStatus.data_list);
            return View();
        }
        [Authorize]
        [PSAuthorize]
        public JsonResult GetLaboratoryById(int id)
        {
            LaboratoryResult result = new LaboratoryResult();
            result = LaboratoryXrayHelper.LoadLaboratoryById(id);
            return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        [Authorize]
        [PSAuthorize]
        public JsonResult SaveLaboratory(LaboratoryAux LaboratoryAux)
        {
            UserResult currentUser = UserHelper.GetUser(User.Identity.Name);
            LaboratoryAux.formDataAux.userId = currentUser.Id;
            LaboratoryResult result = new LaboratoryResult();
            result = LaboratoryXrayHelper.SaveLaboratory(LaboratoryAux);
            return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetForms(DTParameterModel model)
        {
            vwFormResult result = FormHelper.GetForms(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetForm(int id)
        {
            FormResult result = FormHelper.GetForm(id);
            return Json(new { success = result.success, message = result.message, data = result.data }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        #endregion
        #region Xray
        [MenuData]
        [Authorize]
        [PSAuthorize]
        public ActionResult Xray()
        {
            var resultStatus = StatusHelper.getStatus();
            ViewBag.Status = JsonUtility.ObjectToJsonJSEncode(resultStatus.data_list);
            return View();
        }
        [Authorize]
        [PSAuthorize]
        public JsonResult GetXrayById(int id)
        {
            XrayResult result = new XrayResult();
            result = LaboratoryXrayHelper.LoadXrayById(id);
            return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        [Authorize]
        [PSAuthorize]
        public JsonResult SaveXray(XrayAux XrayAux)
        {
            XrayResult result = new XrayResult();
            result = LaboratoryXrayHelper.SaveXray(XrayAux);
            return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        #endregion

        [Authorize]
        [PSAuthorize]
        public JsonResult getTemplatesLabList(DTParameterModel model)
        {
            TemplateLabResult result = new TemplateLabResult();
            result = LaboratoryXrayHelper.getTemplatesLabList(model);


            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value, SysDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")).ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("es-MX")) }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        #region resultados de laboratorio de clientes
        public ActionResult PatientResults()
        {
            ClinicResult ClinicRes = ClinicHelper.GetClinicsSelect();
            ViewBag.ClinicRes = JsonUtility.ObjectToJsonJSEncode(ClinicRes.data_list);
            return View();
        }
        //public JsonResult loadPatientExams(int id, int Ticket, int Clinica)
        //{
        //    PatientResult result = new PatientResult();
        //    result = LaboratoryXrayHelper.loadPatientExams(id, Ticket, Clinica);
        //    return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        //}

        public JsonResult GetPatientExamForm(int id)
        {
            FormDataResult result = new FormDataResult();
            result = FormHelper.GetFormDataByExam(id);
            return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        #endregion
    }
}