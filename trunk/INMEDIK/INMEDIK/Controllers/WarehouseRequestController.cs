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
    [PSAuthorize]
    public class WarehouseRequestController : Controller
    {
        // GET: WarehouseRequest
        [MenuData]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetBranchRequestSend(DTParameterModel model)
        {
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);

            vwBranchRequestResult result = WarehouseRequestHelper.GetBranchRequestSendDT(model, CurrentClinic.data.id.Value);
            return Json(new { success = result.success, data = result.data_list, message = result.message, recordsTotal = result.total.value, recordsFiltered = result.total.value, draw = model.Draw }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetBranchRequestOnHold(DTParameterModel model)
        {
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);

            vwBranchRequestResponseResult result = WarehouseRequestHelper.GetBranchRequestOnHoldDT(model, CurrentClinic.data.id.Value);
            return Json(new { success = result.success, data = result.data_list, message = result.message, recordsTotal = result.total.value, recordsFiltered = result.total.value, draw = model.Draw }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetBranchRequestConfirmed(DTParameterModel model)
        {
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            vwBranchRequestConfirmedResult result = WarehouseRequestHelper.GetBranchRequestConfirmedDT(model, CurrentClinic.data.id.Value);
            return Json(new { success = result.success, data = result.data_list, message = result.message, recordsTotal = result.total.value, recordsFiltered = result.total.value, draw = model.Draw }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetBranchRequestRejected(DTParameterModel model)
        {
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            vwBranchRequestRejectedResult result = WarehouseRequestHelper.GetBranchRequestRejectedDT(model, CurrentClinic.data.id.Value);
            return Json(new { success = result.success, data = result.data_list, message = result.message, recordsTotal = result.total.value, recordsFiltered = result.total.value, draw = model.Draw }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetBranchRequestToDelivery(DTParameterModel model)
        {
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            vwAlmBranchRequestToDeliveryResult result = WarehouseRequestHelper.GetBranchRequestToDeliveryDT(model, CurrentClinic.data.id.Value);
            return Json(new { success = result.success, data = result.data_list, message = result.message, recordsTotal = result.total.value, recordsFiltered = result.total.value, draw = model.Draw }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetBranchRequestDelivered(DTParameterModel model)
        {
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            vwAlmBranchRequestDeliveredResult result = WarehouseRequestHelper.GetBranchRequestDeliveredDT(model, CurrentClinic.data.id.Value);
            return Json(new { success = result.success, data = result.data_list, message = result.message, recordsTotal = result.total.value, recordsFiltered = result.total.value, draw = model.Draw }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetWarehouseRequestDetail(int wareHouseRequestId)
        {
            AlmBranchRequestResult result = WarehouseRequestHelper.GetAlmBranchRequestDetail(wareHouseRequestId);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetBranchRequestResponseDetail(int wareHouseRequestId)
        {
            AlmBranchRequestResponseResult result = WarehouseRequestHelper.GetAlmBranchRequestResponseDetail(wareHouseRequestId);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetWarehouseRequestConfirmedDetail(int wareHouseRequestId)
        {
            AlmBranchRequestConfirmedResult result = WarehouseRequestHelper.GetAlmBranchRequestConfirmedDetail(wareHouseRequestId);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetWarehouseRequestRejectedDetail(int wareHouseRequestId)
        {
            AlmBranchRequestRejectedResult result = WarehouseRequestHelper.GetAlmBranchRequestRejectedDetail(wareHouseRequestId);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetWarehouseRequestToDeliveryDetail(int wareHouseRequestId)
        {
            AlmBranchRequestToDeliveryResult result = WarehouseRequestHelper.GetWarehouseRequestToDeliveryDetail(wareHouseRequestId);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetWarehouseRequestDeliveredDetail(int wareHouseRequestId)
        {
            AlmBranchRequestDeliveredResult result = WarehouseRequestHelper.GetWarehouseRequestDeliveredDetail(wareHouseRequestId);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetWarehouseRequestCounter()
        {
            WarehouseRequestResult result = WarehouseRequestHelper.GetWarehouseRequestCounter();
            return Json(new { data = result.data, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetPredictedWarehouseRequest()
        {
            UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);
            PredictedProductRequestResult result = WarehouseRequestHelper.GetPredictedWarehouseRequest(CurrentUser.clinicId);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult GetConceptsTypeaheadForWarehouseRequest(string typed = "")
        {
            UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);
            vwAllProductsResult res = ConceptHelper.GetProductsTypeahead(typed, CurrentUser.clinicId);
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveRequestResponse(AlmBranchRequestResponseAux almBranchRequestResponse)
        {
            AlmBranchRequestResponseResult result = WarehouseRequestHelper.SaveAlmBranchRequestResponse(almBranchRequestResponse);
            return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult SaveRequestToDelivery(AlmBranchRequestToDeliveryAux requestToDelivery)
        {
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();
            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            AlmBranchRequestToDeliveryResult result = WarehouseRequestHelper.SaveAlmBranchRequestToDelivery(requestToDelivery, CurrentClinic.data.id.Value, CurrentUser.Id);
            return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult SaveWarehouseRequest(ConceptAux Concept)
        {
            Result validRequest = ConceptHelper.IsValidWarehouseRequest(Concept);
            if (validRequest.success)
            {
                UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);
                Result res = WarehouseRequestHelper.SaveWarehouseRequest(Concept, CurrentUser.clinicId);

                return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { success = validRequest.success, errors = validRequest.errors }, JsonRequestBehavior.DenyGet);
        }
    }
}