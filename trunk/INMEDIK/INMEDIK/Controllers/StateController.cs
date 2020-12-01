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
    public class StateController : Controller
    {
        // GET: State
        [MenuData]
        [PSAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetStatesSelect()
        {
            StateResult res = StateHelper.GetStatesSelect();
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
    }

}