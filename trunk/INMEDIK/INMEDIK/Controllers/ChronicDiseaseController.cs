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
    public class ChronicDiseaseController : Controller
    {
        [Authorize]
        public JsonResult GetDiseases()
        {
            ChronicDiseaseResult result = ChronicDiseaseHelper.GetChronicDiseases(false);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
    }
}