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
    public class InternmentController : Controller
    {
        [MenuData]
        [PSAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [MenuData]
        [PSAuthorize]
        public ActionResult Surgeries()
        {
            return View();
        }

        [MenuData]
        [PSAuthorize]
        public ActionResult ViewInternment(int OrderId)
        {
            EmployeeResult res = EmployeeHelper.GetEmployeesByRol("Medic");
            SpecialtyResult result = SpecialtyHelper.GetSpecialties();
            ClinicResult ClinicRes = ClinicHelper.GetClinicsSelect();
            NumericResult CardCommission = ParameterHelper.GetCardCommission();
            NumericResult Tax = ParameterHelper.GetTax();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);

            ViewBag.MedicList = JsonUtility.ObjectToJsonJSEncode(res.data_list);
            ViewBag.SpecialtiesList = JsonUtility.ObjectToJsonJSEncode(result.data_list);
            ViewBag.ClinicRes = JsonUtility.ObjectToJsonJSEncode(ClinicRes.data_list);
            ViewBag.Parameters = JsonUtility.ObjectToJsonJSEncode(new
            {
                CardCommission = CardCommission.value,
                Tax = Tax.value,
                clinicId = CurrentClinic.data.id
            });

            ViewBag.OrderId = OrderId;
            return View();
        }

        [MenuData]
        [PSAuthorize]
        public ActionResult RequestMaterial()
        {
            return View();
        }

        [MenuData]
        [PSAuthorize]
        public ActionResult RestockMaterial()
        {
            var resultStatus = StatusHelper.getStatus();
            ViewBag.Status = JsonUtility.ObjectToJsonJSEncode(resultStatus.data_list);
            return View();
        }
        [PSAuthorize]
        public JsonResult GetInternment(int OrderId)
        {
            InternmentResult res = InternmentHelper.GetInternment(OrderId);
            return Json(new { success = res.success, message = res.message, data = res.data }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetSurgeries(DTParameterModel model)
        {
            OrderResult result = new OrderResult();
            ClinicResult CurrentClinic = new ClinicResult();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentClinic.data.id.HasValue)
            {
                result = OrderHelper.GetOrderedPackages(model, CurrentClinic.data.id.Value);
            }
            
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetInternments(DTParameterModel model)
        {
            ClinicResult CurrentClinic = new ClinicResult();
            vwInternmentResult result = new vwInternmentResult();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentClinic.data.id.HasValue)
            {
                result = OrderHelper.GetInternments(model, CurrentClinic.data.id.Value);
            }
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult ConfirmInternment(int idOrder)
        {
            Result res = OrderHelper.ConfirmInternment(idOrder);
            return Json(new { success = res.success, message = res.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult SaveIndications(int internment, List<MedicalIndicationAux> Indications)
        {
            Result res = InternmentHelper.SaveIndications(internment, Indications);
            return Json(new { success = res.success, message = res.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult SaveMedicalNotes(int internment, string note)
        {
            Result res = InternmentHelper.SaveMedicalNotes(internment, note);
            return Json(new { success = res.success, message = res.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult SaveNurserySummary(int internment, List<NurserySummaryAux> summary)
        {
            Result res = InternmentHelper.SaveNurserySummary(internment, summary);
            return Json(new { success = res.success, message = res.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetIndicationsForInternment(int internment)
        {
            ApplicationResult res = InternmentHelper.GetIndicationsForInternment(internment);
            return Json(new { success = res.success, message = res.message, data = res.data_list }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetMedicalNotes(int internment)
        {
            MedicalNoteResult res = InternmentHelper.GetMedicalNotes(internment);
            return Json(new { success = res.success, message = res.message, data = res.data_list }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetNurserySummaryHistory(int internment)
        {
            SummaryHistoryResult res = InternmentHelper.GetNurserySummaryHistory(internment);
            return Json(new { success = res.success, message = res.message, data = res.data_list }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult SaveInternmentMaterial(List<OrderConceptAux> AddedConcepts, int OrderId)
        {
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            var EmployeeResult = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);
            if (EmployeeResult.success)
            {

                Result result = InternmentHelper.SaveInternmentMaterial(AddedConcepts, OrderId, EmployeeResult.data.id);
                return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { success = EmployeeResult.success, message = EmployeeResult.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }
        }
        [PSAuthorize]
        public JsonResult GetInternmentMaterial(int OrderId)
        {
            InternmentMaterialResult result = new InternmentMaterialResult();
            result = InternmentHelper.GetInternmentMaterial(OrderId);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        public JsonResult GetRequest(DTParameterModel model)
        {
            ClinicResult CurrentClinic = new ClinicResult();
            RequestMaterialResult result = new RequestMaterialResult();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentClinic.data.id.HasValue)
            {
                result = InternmentHelper.GetRequest(model, CurrentClinic.data.id.Value);
            }
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        public JsonResult GetRequestedMaterial(int? RequestId)
        {
            RequestMaterialResult result = new RequestMaterialResult();
            ClinicResult CurrentClinic = new ClinicResult();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentClinic.data.id.HasValue)
            {
                result = InternmentHelper.GetRequestedMaterial(CurrentClinic.data.id.Value, RequestId);
            }
            return Json(new { data = result.data, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult SendRequest()
        {
            RequestedMaterialResult result = new RequestedMaterialResult();
            ClinicResult CurrentClinic = new ClinicResult();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentClinic.data.id.HasValue)
            {
                EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);
                result = InternmentHelper.SendRequest(CurrentClinic.data.id.Value, EmployeeRess.data.id);
            }
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult RestockRequest(RequestMaterialAux RequestAux)
        {
            Result result = new Result();
            ClinicResult CurrentClinic = new ClinicResult();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentClinic.data.id.HasValue)
            {
                EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);
                RequestAux.EmployeeUpdatedId = EmployeeRess.data.id;
                result = InternmentHelper.RestockRequest(RequestAux);
            }
            return Json(new {  success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
    }
}