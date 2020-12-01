using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace INMEDIK.Controllers
{
    [Authorize]
    public class ClinicController : Controller
    {
        // GET: Clinic
        [MenuData]
        [PSAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [PSAuthorize]
        public JsonResult SaveClinic(ClinicAux Clinic)
        {
            Result res = ClinicHelper.SaveClinic(Clinic);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult DeleteClinic(ClinicAux Clinic)
        {
            Result res = ClinicHelper.DeleteClinic(Clinic);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetClinic(int id)
        {
            ClinicResult res = ClinicHelper.GetClinic(id);
            return Json(new { success = res.success, message = res.message, data = res.data }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetClinics(DTParameterModel model)
        {
            ClinicResult result = ClinicHelper.GetClinics(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetClinicsSelect()
        {
            ClinicResult res = ClinicHelper.GetClinicsSelect();
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetClinicsSelectCurrentUser()
        {

            UserResult currentUser = UserHelper.GetUser(User.Identity.Name);
            List<ClinicAux> clinics = new List<ClinicAux>();
            string message = "";
            bool success = false;
            if (currentUser.User.rolAux.name != "Admin")
            {
                EmployeeResult currentEmployee = EmployeeHelper.GetEmployeeByIdUser(currentUser.Id);
                message = currentEmployee.message;
                success = currentEmployee.success;
                clinics = currentEmployee.data.clinicAux;
            }
            else
            {
                ClinicResult res = ClinicHelper.GetClinicsSelect();
                message = res.message;
                success = res.success;
                clinics = res.data_list;
            }

            return Json(new { data = clinics, message = message, success = success }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetClinicsSelectForRol()
        {
            ClinicResult res = new ClinicResult();
            if (Request.Cookies["_Authdata"] != null && !string.IsNullOrEmpty(Request.Cookies["_Authdata"].Value))
            {
                UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);
                if (CurrentUser.clinicId > -1)
                {
                    res = ClinicHelper.GetClinicsSelectForRol(CurrentUser.clinicId);
                }
                else
                {
                    res.message = "La sesión ha finalizado.";
                    res.success = false;
                }
            }
            else
            {
                res.message = "La sesión ha finalizado.";
                res.success = false;
            }
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
    }
}