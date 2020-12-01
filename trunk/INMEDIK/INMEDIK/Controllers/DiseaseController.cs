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
    public class DiseaseController : Controller
    {
        // GET: County
        [MenuData]
        [PSAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [PSAuthorize]
        public JsonResult SaveDisease(DiseaseAux disease)
        {
            Result res = DiseaseHelper.SaveDisease(disease);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult DeleteDisease(DiseaseAux disease)
        {
            GenericResult res = DiseaseHelper.DeleteDisease(disease.id);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetDisease(int id)
        {
            DiseaseResult res = DiseaseHelper.GetDisease(id);
            return Json(new { success = res.success, message = res.message, data = res.data }, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult GetDiseases(DTParameterModel model)
        {
            DiseaseResult result = DiseaseHelper.GetDiseases(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
    }
}