using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INMEDIK.Models.Helpers;
using System.Text;
using INMEDIK.Common;

namespace INMEDIK.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        // GET: Patient
        [PSAuthorize]
        [MenuData]
        public ActionResult Index()
        {
            NationalityResult NationalityRes = NationalityHelper.GetNationalitySelect();

            ViewBag.NationalityRes = JsonUtility.ObjectToJsonJSEncode(NationalityRes.data_list);
            return View();
        }
        [PSAuthorize]
        public JsonResult SavePatient(PatientAux patient)
        {
            PatientResult res = PatientHelper.SavePatient(patient);
            return Json(new { success = res.success, message = res.message, patient = res.data }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult SaveBulkPatients(List<PatientLoadAux> patients)
        {
            GenericResult res = BulkLoad.UploadPatients(patients);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult DeletePatient(PatientAux patient)
        {
            GenericResult res = PatientHelper.DeletePatient(patient.id);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetPatient(int id)
        {
            PatientResult res = PatientHelper.GetPatient(id);
            return Json(new { success = res.success, message = res.message, data = res.data }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetPatients(DTParameterModel model)
        {
            PatientResult result = PatientHelper.GetPatients(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult GetPatientsList(string data)
        {
            PatientResult result = PatientHelper.GetPatientsList(data);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult GetPatientsListFile(string data)
        {
            UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);
            if (CurrentUser.clinicId > -1)
            {
                PatientResult result = PatientHelper.GetPatientsListF(data, CurrentUser.clinicId);
                return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { message = "No se encontraron resultados" }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }
        }
        [PSAuthorize]
        public JsonResult GetPersons(DTParameterModel model)
        {
            PersonResult result = PersonHelper.GetPersons(model, false);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
    }
}