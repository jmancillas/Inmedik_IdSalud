using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using INMEDIK.Common;

namespace INMEDIK.Controllers
{
    [Authorize]
    [PSAuthorize]
    public class ReportController : Controller
    {
        // GET: Report
        [MenuData]
        public ActionResult Index()
        {
            UserResult currentUser = UserHelper.GetUser(User.Identity.Name);
            ViewBag.currentRole = currentUser.User.rolAux.name;
            return View();
        }

        public JsonResult GetProductivityReport(DTParameterModel model, ReportFilter filter)
        {
            ProductivityReportResult result = ReportHelper.GetProductivityReport(model, filter);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetPharmacyProductivityReport(DTParameterModel model, ReportFilter filter)
        {
            ProductivityReportResult result = ReportHelper.GetPharmacyProductivityReport(model, filter);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetConceptProductivityReport(DTParameterModel model, ReportFilter filter)
        {
            ProductivityReportResult result = ReportHelper.GetConceptProductivityReport(model, filter);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetConceptProductivityPaymentReport(DTParameterModel model, ReportFilter filter)
        {
            PaymentReportResult result = ReportHelper.GetPaymentReport(model, filter);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetRoundingReport(DTParameterModel model, ReportFilter filter)
        {
            RoundingReportResult result = ReportHelper.GetRoundingReport(model, filter);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetCardPaymentReport(DTParameterModel model, ReportFilter filter)
        {
            CardPaymentReportResult result = ReportHelper.GetCardPaymentReport(model, filter);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetCountyServicesReport(DTParameterModel model, ReportFilter filter)
        {
            CountyServicesResult result = ReportHelper.GetCountyServicesReport(model, filter);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetPatientsReport(DTParameterModel model, ReportFilter filter)
        {
            PatientDiseaseResult result = ReportHelper.GetPatientsReport(model, filter);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetHealthReport(ReportFilter filter)
        {
            HealthReportResult result = ReportHelper.GetHealthReport(filter);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetCashClosingReport(DTParameterModel model, ReportFilter filter)
        {
            CashClosingReportResult result = ReportHelper.GetCashClosingReport(model,filter);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetCashClosingDetail(int id)
        {
            CashClosingResult result = CashClosingHelper.getCashClosing(id);
            return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetDonationsReport(DTParameterModel model, ReportFilter filter)
        {
            DonationsReportResult result = ReportHelper.GetDonationsReport(model, filter);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetWaitingTimesReport(DTParameterModel model, ReportFilter filter)
        {
            GetWaitingTimesReportResult result = ReportHelper.GetWaitingTimesReport(model, filter);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        public JsonResult GetWaitingTimesGraph(ReportFilter filter)
        {
            GetWaitingTimesGraphResult result = ReportHelper.GetWaitingTimesGraph(filter);
            return Json(new { data = result.data, recordsTotal = result.total.value, recordsFiltered = result.total.value, success = result.success }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
    }
}