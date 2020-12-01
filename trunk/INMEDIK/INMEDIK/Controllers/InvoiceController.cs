using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace INMEDIK.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        // GET: Invoice
        [MenuData]
        [PSAuthorize]
        public ActionResult Index()
        {
            NumericResult result = ParameterHelper.GetTax();
            ViewBag.tax = result.IntValue;
            return View();
        }
        [PSAuthorize]
        public JsonResult GetInvoices(DTParameterModel model)
        {
            InvoiceResult result = new InvoiceResult();
            if (Request.Cookies["_Authdata"] != null && !string.IsNullOrEmpty(Request.Cookies["_Authdata"].Value))
            {
                UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);
                if (CurrentUser.clinicId > -1)
                {
                    result = InvoiceHelper.GetInvoices(model, CurrentUser.clinicId);
                }
                else
                {
                    result.message = "La sesión ha finalizado.";
                    result.success = false;
                }
            }
            else
            {
                result.message = "La sesión ha finalizado.";
                result.success = false;
            }
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult GetConceptsOfCategory(string data = "")
        {
            Hashtable dataHash = JsonConvert.DeserializeObject<Hashtable>(data);
            ClinicResult CurrentClinic = new ClinicResult();
            ConceptResult res = new ConceptResult();

            string typed = dataHash["typed"].ToString();
            string CategoryName = dataHash["CategoryName"].ToString();

            UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            int ClinicId = CurrentClinic.data.id.Value;
            if (CurrentClinic.data.id.HasValue)
            {
                res = ConceptHelper.GetConceptsOfCategoryNoDiscount(typed, CategoryName, ClinicId);
            }
            else
            {
                res.message = "No se encontro clinica.";
            }
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
        [PSAuthorize]
        public JsonResult SaveInvoice(InvoiceAux invoice)
        {
            Result result = InvoiceHelper.SaveInvoice(invoice);
            return Json(new { success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult EditData(InvoiceAux invoice, List<InvoiceMovementAux> deleteData)
        {
            Result result = InvoiceHelper.EditInvoice(invoice,deleteData);
            return Json(new { success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        [HttpPost]
        public JsonResult GetInvoice(int id)
        {
            InvoiceResult res = InvoiceHelper.GetInvoice(id);
            return Json(new { success = res.success, message = res.message, data = res.data }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        [HttpPost]
        public JsonResult CancelInvoice(int InvoiceId)
        {
            InvoiceResult res = InvoiceHelper.CancelInvoice(InvoiceId);
            return Json(new { success = res.success, message = res.message, data = res.data }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        [HttpPost]
        public JsonResult GetRecordInvoiceVersion(int id)
        {
            RecordInvoiceResult result = RecordInvoiceHelper.GetRecordInvoiceVersion(id);
            return Json(new { data = result.data_list, success = result.success, message = result.message });
        }
    }
}