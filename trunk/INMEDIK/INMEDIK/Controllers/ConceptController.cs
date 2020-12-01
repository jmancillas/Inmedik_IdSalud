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
    [Authorize]
    public class ConceptController : Controller
    {
        // GET: Concept
        [MenuData]
        [PSAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [PSAuthorize]
        public JsonResult SaveConcept(ConceptAux Concept)
        {
            Result res = ConceptHelper.SaveConcept(Concept);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }

        [PSAuthorize]
        public JsonResult SavePackage(PackageAux Package)
        {
            Result res = ConceptHelper.SavePackage(Package);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }

        [PSAuthorize]
        public JsonResult DeleteConcept(ConceptAux Concept)
        {
            Result res = ConceptHelper.DeleteConcept(Concept);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }

        [PSAuthorize]
        public JsonResult DeletePackage(PackageAux Package)
        {
            Result res = ConceptHelper.DeletePackage(Package);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }

        [PSAuthorize]
        public JsonResult GetConcept(int id)
        {
            ConceptResult res = ConceptHelper.GetConcept(id);
            return Json(new { success = res.success, message = res.message, data = res.data }, JsonRequestBehavior.DenyGet);
        }

        [PSAuthorize]
        public JsonResult GetPackage(int id)
        {
            PackageResult res = ConceptHelper.GetPackage(id);
            return Json(new { success = res.success, message = res.message, data = res.data }, JsonRequestBehavior.DenyGet);
        }

        [PSAuthorize]
        public JsonResult GetConcepts(DTParameterModel model)
        {
            ConceptResult result = ConceptHelper.GetConcepts(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetConceptsSelect()
        {
            ConceptResult res = ConceptHelper.GetConceptsSelect();
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTotalConceptProducts()
        {
            TotalConceptsResult result = ConceptHelper.GetTotalConceptProducts();
            return Json(new { data = result.data, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetConceptsOfCategory(string data = "")
        {
            Hashtable dataHash = JsonConvert.DeserializeObject<Hashtable>(data);
            ClinicResult CurrentClinic = new ClinicResult();
            ConceptResult res = new ConceptResult();

            string typed = dataHash["typed"].ToString();
            string CategoryName = dataHash["CategoryName"].ToString();

            UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            int ClinicId = CurrentClinic.data.id.Value;
            if (CurrentClinic.data.id.HasValue)
            {
                res = ConceptHelper.GetConceptsOfCategory(typed, CategoryName, ClinicId);
            }
            else
            {
                res.message = "No se encontro clinica.";
            }
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetConceptsTypeahead(string data = "")
        {
            Hashtable dataHash = JsonConvert.DeserializeObject<Hashtable>(data);
            ConceptResult res = new ConceptResult();

            string typed = dataHash["typed"].ToString();
            res = ConceptHelper.GetConceptsTypeahead(typed);
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
    }
}