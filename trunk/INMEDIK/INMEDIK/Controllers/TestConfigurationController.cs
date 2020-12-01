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
    public class TestConfigurationController : Controller
    {
        // GET: TestConfiguration
        [PSAuthorize]
        [MenuData]
        public ActionResult Index()
        {
            return View();
        }
        [PSAuthorize]
        public JsonResult GetFieldTypes()
        {
            FieldTypeResult fieldTypes = FormHelper.GetFieldTypeSelect();
            return Json(new { data = fieldTypes.data_list, success = fieldTypes.success, message = fieldTypes.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetForms(DTParameterModel model)
        {
            vwFormResult result = FormHelper.GetForms(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetModules(DTParameterModel model)
        {
            vwModuleResult result = FormHelper.SearchModules(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetFields(DTParameterModel model)
        {
            vwFieldResult result = FormHelper.SearchFields(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetForm(int id)
        {
            FormResult result = FormHelper.GetForm(id);
            return Json(new { success = result.success, message = result.message, data = result.data }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetModule(int id)
        {
            ModuleResult result = FormHelper.GetModule(id);
            return Json(new { success = result.success, message = result.message, data = result.data }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetField(int id)
        {
            FieldResult result = FormHelper.GetField(id);
            return Json(new { success = result.success, message = result.message, data = result.data }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult SaveField(FieldAux field)
        {
            UserResult currentUser = UserHelper.GetUser(User.Identity.Name);
            field.modifiedById = currentUser.Id;
            GenericResult result = FormHelper.SaveField(field);
            return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult SaveModule(ModuleAux module)
        {
            UserResult currentUser = UserHelper.GetUser(User.Identity.Name);
            module.modifiedById = currentUser.Id;
            GenericResult result = FormHelper.SaveModule(module);
            return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult SaveForm(FormAux form)
        {
            UserResult currentUser = UserHelper.GetUser(User.Identity.Name);
            form.modifiedById = currentUser.Id;
            GenericResult result = FormHelper.SaveForm(form);
            return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [PSAuthorize]
        public JsonResult DeleteField(int id)
        {
            UserResult currentUser = UserHelper.GetUser(User.Identity.Name);
            GenericResult result = FormHelper.DeleteField(id, currentUser.Id);
            return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [PSAuthorize]
        public JsonResult DeleteModule(int id)
        {
            UserResult currentUser = UserHelper.GetUser(User.Identity.Name);
            GenericResult result = FormHelper.DeleteModule(id, currentUser.Id);
            return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [PSAuthorize]
        public JsonResult DeleteForm(int id)
        {
            UserResult currentUser = UserHelper.GetUser(User.Identity.Name);
            GenericResult result = FormHelper.DeleteForm(id, currentUser.Id);
            return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
    }
}