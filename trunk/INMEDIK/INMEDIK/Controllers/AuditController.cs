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
    public class AuditController : Controller
    {
        // GET: Audit
        [Authorize]
        [PSAuthorize]
        [MenuData]
        public ActionResult Index()
       {
            ClinicResult CurrentClinic = new ClinicResult();
            UserResult CurrentUser = new UserResult();
            vwStockResult stocks = new vwStockResult();
            vwStockResult stocksNurse = new vwStockResult();
            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            stocks = StockHelper.GetStocks(CurrentClinic.data.id.Value, false);
            stocksNurse = StockHelper.GetStocks(CurrentClinic.data.id.Value, true);
            ViewBag.stocks = JsonUtility.ObjectToJsonJSEncode(stocks.data_list);
            ViewBag.stocksNurse = JsonUtility.ObjectToJsonJSEncode(stocksNurse.data_list);

            return View();
        }

        [Authorize]
        public JsonResult SaveAudit(AuditAux audit)
        {
            ClinicResult CurrentClinic = new ClinicResult();
            UserResult CurrentUser = new UserResult();
            AuditResult result = new AuditResult();
            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentUser.success)
            {
                audit.clinicId = CurrentClinic.data.id.Value;
                audit.employeeId = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id).data.id;
                result = AuditHelper.CreateAudit(audit);
                return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { success = CurrentUser.success, message = CurrentUser.message }, JsonRequestBehavior.DenyGet);
            }
        }
        [Authorize]
        public JsonResult SaveComment(int auditId, string comment)
        {
            ClinicResult CurrentClinic = new ClinicResult();
            UserResult CurrentUser = new UserResult();
            vwStockResult stocks = new vwStockResult();
            vwStockResult stocksNurse = new vwStockResult();
            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);

            GenericResult result = AuditHelper.SaveAuditComment(comment, auditId);
            stocks = StockHelper.GetStocks(CurrentClinic.data.id.Value, false);
            stocksNurse = StockHelper.GetStocks(CurrentClinic.data.id.Value, true);
            return Json(new { data = stocks.data_list, dataNurse = stocksNurse.data_list ,success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        [Authorize]
        public JsonResult GetAudit(int id)
        {
            AuditResult result = AuditHelper.GetAudit(id);
            return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        [Authorize]
        public JsonResult GetAudits(DTParameterModel model, bool isNurse)
        {
            ClinicResult CurrentClinic = new ClinicResult();
            AuditResult result = new AuditResult();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentClinic.data.id.HasValue)
            {
                result = AuditHelper.GetAudits(model, CurrentClinic.data.id.Value, isNurse);
            }
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [Authorize]
        public JsonResult GetWarehouseStocks(DTParameterModel model)
        {
            ClinicResult CurrentClinic = new ClinicResult();
            WarehouseStockResult result = new WarehouseStockResult();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentClinic.data.id.HasValue)
            {
                result = WarehouseStockHelper.GetWarehouseStocks(model,CurrentClinic.data.id.Value);
            }
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [Authorize]
        public JsonResult GetAllWarehouseStocks()
        {
            ClinicResult CurrentClinic = new ClinicResult();
            WarehouseStockResult result = new WarehouseStockResult();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentClinic.data.id.HasValue)
            {
                result = WarehouseStockHelper.GetWarehouseStocks(CurrentClinic.data.id.Value);
            }
            return Json(new { data = result.data_list, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
    }
}