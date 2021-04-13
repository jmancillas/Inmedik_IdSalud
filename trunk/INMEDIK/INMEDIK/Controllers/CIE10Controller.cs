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
    public class CIE10Controller : Controller
    {
        // GET: CIE10
        [MenuData]
        public ActionResult Index()
        {
            return View();
        }
     
        public JsonResult GetCIE10(DTParameterModel model)
        {
            CIE10AuxResult result = CIE10Helper.GetCIE10Catalog(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
    }
}