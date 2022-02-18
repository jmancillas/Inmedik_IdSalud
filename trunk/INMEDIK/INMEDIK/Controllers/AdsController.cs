using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace INMEDIK.Controllers
{
    [Authorize]
    public class AdsController : Controller
    {
        // GET: County
        [PSAuthorize]
        [MenuData]
        public ActionResult Index()
        {
            return View();
        }
        [PSAuthorize]
        public JsonResult SaveAd(AdsAux ad)
        {
            GenericResult res = AdsHelper.SaveAd(ad);
            return Json(new { success = res.success, message = res.message, id = res.integer_value }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult DeleteAd(AdsAux ad)
        {
            GenericResult res = AdsHelper.DeleteAd(ad.id);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetAd(int id)
        {
            AdResult res = AdsHelper.GetAd(id);
            return Json(new { success = res.success, message = res.message, data = Newtonsoft.Json.JsonConvert.SerializeObject(res.data) }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetAds(DTParameterModel model)
        {
            AdResult result = AdsHelper.GetAds(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult GetActiveAds()
        {
            AdResult result = AdsHelper.GetActiveAds();
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetKeywords(string data)
        {
            Hashtable dataHash = JsonConvert.DeserializeObject<Hashtable>(data);
            ConceptResult res = new ConceptResult();

            string typed = dataHash["typed"].ToString();
            KeywordResult result = AdsHelper.GetKeywords(typed);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public FileResult GetAdImage(int adId)
        {
            FileDbResult FileRes = AdsHelper.GetFileDbByAdId(adId);

            var dir = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files/");
            return File(Path.Combine(dir + FileRes.data.Name), FileRes.data.ContentType);
        }
        [PSAuthorize]
        public FileResult GetEvImage(int evId)
        {
            FileDbResult FileRes = AdsHelper.GetFileDbByEvId(evId);

            var dir = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files/ElectronicFile/");
            return File(Path.Combine(dir + FileRes.data.Name), FileRes.data.ContentType);
        }

        [PSAuthorize]
        public FilePathResult DownloadEvFile(int evId)
        {
            FileDbResult FileRes = AdsHelper.GetFileDbByEvId(evId);
            return new FilePathResult(string.Format("~/App_Data/Files/ElectronicFile/{0}", FileRes.data.Name), FileRes.data.ContentType);
        }
    }
}