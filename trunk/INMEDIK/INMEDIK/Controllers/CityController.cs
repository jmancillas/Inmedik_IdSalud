using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INMEDIK.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        [HttpPost]
        public JsonResult GetCities()
        {
            CityResult res =  CityHelper.GetCities();
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCitiesSelectAll()
        {
            CityResult res = CityHelper.GetCitiesSelect();
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCitiesSelect(StateAux state)
        {
            CityResult res = CityHelper.GetCitiesSelect(state);
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
    }
}