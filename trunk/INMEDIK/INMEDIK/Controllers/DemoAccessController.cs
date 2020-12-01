using INMEDIK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using INMEDIK.Models.Helpers;

namespace INMEDIK.Controllers
{
    public class DemoAccessController : Controller
    {
        [PSAuthorize]
        [MenuData]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowCrossSiteJsonAttribute]
        public ActionResult CreateDemoRequest(DemoRequestAux data)
        {
            GenericResult result = new GenericResult();
            result = DemoRequestHelper.SaveDemoRequest(data);
            return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        [HttpPost]
        public JsonResult ReplyDemoRequest(DemoRequestAux data)
        {
            GenericResult result = new GenericResult();
            result = DemoRequestHelper.ReplyDemoRequest(data);
            return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        [HttpPost]
        public JsonResult GetDemoRequest(int id)
        {
            DemoRequestResult result = new DemoRequestResult();
            result = DemoRequestHelper.GetDemoRequest(id);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        [HttpPost]
        public JsonResult GetDemoRequests(DTParameterModel model)
        {
            DemoRequestResult result = DemoRequestHelper.GetDemoRequests(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        [HttpPost]
        public JsonResult GetDemoRequestsConfirmed(DTParameterModel model)
        {
            DemoRequestResult result = DemoRequestHelper.GetDemoRequests(model, confirmed:true);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        [HttpPost]
        public JsonResult GetDemoRequestsRejected(DTParameterModel model)
        {
            DemoRequestResult result = DemoRequestHelper.GetDemoRequests(model, rejected: true);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
    }
}