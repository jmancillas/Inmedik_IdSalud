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
    public class DiscountController : Controller
    {
        // GET: Discount
        [MenuData]
        [PSAuthorize]
        public ActionResult Index()
        {
            DiscountTypeResult result = DiscountHelper.GetDiscountTypeSelect();
            ClinicResult ClinicRes = ClinicHelper.GetClinicsSelect();
            NumericResult Tax = ParameterHelper.GetTax();
            ViewBag.discountTypes = JsonUtility.ObjectToJsonJSEncode(result.data_list);
            ViewBag.clinics = JsonUtility.ObjectToJsonJSEncode(ClinicRes.data_list);
            ViewBag.Parameters = JsonUtility.ObjectToJsonJSEncode(new
            {
                Tax = Tax.value,
            });
            return View();
        }
        [PSAuthorize]
        public JsonResult SaveDiscount(DiscountAux discount)
        {
            Result res = DiscountHelper.SaveDiscount(discount);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult DeleteDiscount(int id)
        {
            Result res = DiscountHelper.DeleteDiscount(id);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetDiscount(int id)
        {
            DiscountResult res = DiscountHelper.GetDiscount(id);
            
            return Json(new { success = res.success, message = res.message, data = Newtonsoft.Json.JsonConvert.SerializeObject(res.data) }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        [HttpPost]
        public JsonResult GetConceptsOfProducts(string data = "")
        {
            Hashtable dataHash = JsonConvert.DeserializeObject<Hashtable>(data);
            ClinicResult CurrentClinic = new ClinicResult();
            ConceptResult res = new ConceptResult();

            string typed = dataHash["typed"].ToString();

            UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentClinic.data.id.HasValue)
            {
                res = ConceptHelper.GetConceptsOfProductsWithDiscounts(typed, "Direct");
            }
            else
            {
                res.message = "No se encontró clínica.";
            }
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
        [PSAuthorize]
        public JsonResult GetDiscounts(DTParameterModel model)
        {
            vwDiscountClinicResult result = DiscountHelper.GetDiscounts(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult GetDiscountsSelect()
        {
            DiscountTypeResult res = DiscountHelper.GetDiscountsSelect();
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
    }
}