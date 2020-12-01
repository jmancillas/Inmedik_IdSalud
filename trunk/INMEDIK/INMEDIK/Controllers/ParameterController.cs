using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INMEDIK.Controllers
{
    public class ParameterController : Controller
    {
        // GET: Parameter
        [Authorize]
        [PSAuthorize]
        [MenuData]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [PSAuthorize]
        public JsonResult GetParameters()
        {
            ParameterResult res = ParameterHelper.GetParameters();
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        [PSAuthorize]
        public JsonResult SaveParameters(List<ParameterAux> parameters)
        {
            Result res = ParameterHelper.SaveParameters(parameters);
            return Json(new {message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
    }
}