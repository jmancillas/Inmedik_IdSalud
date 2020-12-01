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
    [PSAuthorize]
    [Authorize]
    public class ReturnController : Controller
    {
        // GET: Return
        [MenuData]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetReturns(DTParameterModel model)
        {
            ReturnResult result = ReturnHelper.GetReturns(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult SaveReturn(ReturnAux Return)
        {
            ClinicResult CurrentClinic = new ClinicResult();
            Result result = new Result();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentClinic.data.id.HasValue)
            {
                result = ReturnHelper.SaveReturn(Return, CurrentClinic.data.id.Value);
            }
            return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetReturn(int id)
        {

            ReturnResult result = ReturnHelper.GetReturn(id);
            return Json(new { data = result.data, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [Authorize]
        public JsonResult GetWarehouseStocks(DTParameterModel model)
        {
            ClinicResult CurrentClinic = new ClinicResult();
            AllProductExistResult result = new AllProductExistResult();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentClinic.data.id.HasValue)
            {
                result = ReturnHelper.GetStock(model, CurrentClinic.data.id.Value);
            }
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
    }
}