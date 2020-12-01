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
    public class SupplierController : Controller
    {
        // GET: Supplier
        [MenuData]
        [PSAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [PSAuthorize]
        public JsonResult SaveSupplier(SupplierAux Supplier)
        {
            Result res = SupplierHelper.SaveSupplier(Supplier);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult DeleteSupplier(SupplierAux Supplier)
        {
            Result res = SupplierHelper.DeleteSupplier(Supplier);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetSupplier(int id)
        {
            SupplierResult res = SupplierHelper.GetSupplier(id);
            return Json(new { success = res.success, message = res.message, data = res.data }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetSuppliers(DTParameterModel model)
        {
            SupplierResult result = SupplierHelper.GetSuppliers(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetSuppliersSelect()
        {
            SupplierResult res = SupplierHelper.GetSuppliersSelect();
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }

    }
}