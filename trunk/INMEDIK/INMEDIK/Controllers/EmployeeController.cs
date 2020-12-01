using System;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INMEDIK.Models.Helpers;
using System.Text;
using INMEDIK.Common;

namespace INMEDIK.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: Employee
        [MenuData]
        [PSAuthorize]
        public ActionResult Index()
        {
            UserResult currentUser = UserHelper.GetUser(User.Identity.Name);
            ViewBag.isDemo = currentUser.User.isDemo.HasValue ? currentUser.User.isDemo : false;
            return View();
        }
        [PSAuthorize]
        public JsonResult DeleteEmployee(EmployeeAux employee)
        {
            GenericResult res = EmployeeHelper.DeleteEmployee(employee.id);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetEmployee(int id)
        {
            EmployeeResult res = EmployeeHelper.GetEmployee(id);
            return Json(new { success = res.success, message = res.message, data = res.data }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetEmployees(DTParameterModel model)
        {
            EmployeeResult result = EmployeeHelper.GetEmployees(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetMedicsSelect()
        {
            EmployeeResult res = EmployeeHelper.GetEmployeesByRol("Medic");
            return Json(new { data = res.data_list, message = res.message, success = res.success }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult GetEmployeesSelect()
        {
            EmployeeResult res = EmployeeHelper.GetEmployeesSelect();
            return Json(new { data = res.data_list, message = res.message, success = res.success }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        public JsonResult GetClinicsSelect()
        {
            ClinicResult result = ClinicHelper.GetClinicsSelect();
            return Json(new { data = result.data_list, message = result.message, success = result.success }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetSpecialtiesSelect()
        {
            SpecialtyResult result = SpecialtyHelper.GetSpecialties();
            return Json(new { data = result.data_list, message = result.message, success = result.success }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetRolesSelect()
        {
            RolResult result = RolHelper.GetAllRols();
            return Json(new { data = result.data_list, message = result.message, success = result.success }, JsonRequestBehavior.AllowGet);
        }
        [PSAuthorize]
        public JsonResult ToggleEmployee(EmployeeAux employee)
        {
            GenericResult res = EmployeeHelper.ToggleEmployee(employee.id);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        public JsonResult GetEmployeeViews(int id)
        {
            EmployeeResult res = EmployeeHelper.GetEmployeeViews(id);
            return Json(new { success = res.success, message = res.message, data = res.data }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult SaveEmployeeViews(EmployeeAux employee)
        {
            GenericResult res = EmployeeHelper.SaveEmployeeViews(employee);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult SaveEmployee(EmployeeAux employee)
        {
            Result res = EmployeeHelper.SaveEmployee(employee);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetEmployeeSchedule(int id)
        {
            ScheduleResult res = EmployeeHelper.GetEmployeeSchedule(id);
            return Json(new { success = res.success, message = res.message, data = res.data_list }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult SaveEmployeeSchedule(int IdEmployee, List<ScheduleData> schedule)
        {
            Result res = EmployeeHelper.SaveEmployeeSchedule(IdEmployee, schedule);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        public JsonResult GetMedicsBySpecialty(int idSpecialty)
        {
            EmployeeResult result = EmployeeHelper.GetMedicsBySpecialty(idSpecialty);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetMedicsBySpecialtyList(string datas = "")
        {
            Hashtable dataHash = JsonConvert.DeserializeObject<Hashtable>(datas);

            string typed = dataHash["typed"].ToString();
            int specialtyId = Convert.ToInt32(dataHash["specialtyId"]);

            EmployeeResult result = EmployeeHelper.GetMedicsBySpecialtyList(typed, specialtyId);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
    }
}