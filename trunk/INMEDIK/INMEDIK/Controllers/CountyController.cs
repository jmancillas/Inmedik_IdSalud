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
    public class CountyController : Controller
    {
        // GET: County
        [MenuData]
        [PSAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [PSAuthorize]
        public JsonResult SaveCounty(CountyAux County)
        {
            Result res = CountyHelper.SaveCounty(County);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult DeleteCounty(CountyAux County)
        {
            Result res = CountyHelper.DeleteCounty(County);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetCounty(int id)
        {
            CountyResult res = CountyHelper.GetCounty(id);
            return Json(new { success = res.success, message = res.message, data = res.data }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetCountys(DTParameterModel model)
        {
            CountyResult result = CountyHelper.GetCountys(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetCountiesSelect(CityAux city)
        {
            CountyResult res = CountyHelper.GetCountiesSelect(city);
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }

    }
}