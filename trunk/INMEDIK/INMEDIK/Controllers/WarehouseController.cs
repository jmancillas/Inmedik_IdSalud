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
    [Authorize]
    public class WarehouseController : Controller
    {
        // GET: Warehouse
        [MenuData]
        [PSAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [PSAuthorize]
        public JsonResult SaveWarehouse(WarehouseAux Warehouse)
        {
            Result res = WarehouseHelper.SaveWarehouse(Warehouse);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult DeleteWarehouse(WarehouseAux Warehouse)
        {
            Result res = WarehouseHelper.DeleteWarehouse(Warehouse);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetWarehouse(int id)
        {
            WarehouseResult res = WarehouseHelper.GetWarehouse(id);
            return Json(new { success = res.success, message = res.message, data = res.data }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetWarehouses(DTParameterModel model)
        {
            WarehouseResult result = WarehouseHelper.GetWarehouses(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetWarehousesSelect()
        {
            WarehouseResult res = WarehouseHelper.GetWarehousesSelect();
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetWarehousesNoPharmacy()
        {
            WarehouseResult res = WarehouseHelper.GetWarehousesSelect();            
            return Json(new { data = res.data_list.Where(r => r.name != "Farmacia").ToList() , message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
    }
}