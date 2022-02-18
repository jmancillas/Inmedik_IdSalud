using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using System.Web.Configuration;

namespace INMEDIK.Controllers
{
    [Authorize]
    public class ElectronicFileController : Controller
    {
        // GET: ElectronicFile
        [MenuData]
        public ActionResult Index()
        {
            AhfDiseaseResult DiseaseRes = ElectronicFileHelper.GetDiseases();
            NationalityResult NationalityRes = NationalityHelper.GetNationalitySelect();
            ClinicResult ClinicRes = ClinicHelper.GetClinicsSelect();
            SpecialtyResult SpecialityRes = SpecialtyHelper.GetSpecialties();

            ViewBag.NationalityRes = JsonUtility.ObjectToJsonJSEncode(NationalityRes.data_list);
            ViewBag.DiseaseRes = JsonUtility.ObjectToJsonJSEncode(DiseaseRes.data_list);
            ViewBag.ClinicRes = JsonUtility.ObjectToJsonJSEncode(ClinicRes.data_list);
            ViewBag.SpecialityRes = JsonUtility.ObjectToJsonJSEncode(SpecialityRes.data_list);
            return View();
        }
        
        public JsonResult GetElectronicFiles(DTParameterModel model)
        {
            vwElectronicFileAuxResult result = ElectronicFileHelper.GetElectronicFiles(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        
        public JsonResult GetConsultFile(int consultId)
        {
            ElectronicFileResult result = ElectronicFileHelper.GetConsultFile(consultId);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        
        public JsonResult SaveCurrentNote(ConceptAux concept, EvolutionNoteAux evolution, int idx, int electronicFileId, bool toPrint, ConceptAux tipec, bool toSigns)
        {
            EvolutionNoteResult result = ElectronicFileHelper.SaveCurrentNote(concept, evolution,idx,electronicFileId,toPrint, tipec, toSigns);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        
        public JsonResult SaveCurrentInterconsult(InterconsultAux interconsult, int idx, int electronicFileId)
        {
            InterconsultResult result = ElectronicFileHelper.SaveCurrentInterconsult(interconsult, idx, electronicFileId);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        
        public JsonResult SaveCurrentReferenceNote(ReferencenoteAux reference, int idx, int electronicFileId)
        {
            ReferenceNoteResult result = ElectronicFileHelper.SaveCurrentReferenceNote(reference, idx, electronicFileId);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        
        public JsonResult SaveClinicHistoryElements(ElectronicFileAux ConsultFile)
        {
            ElectronicFileResult result = ElectronicFileHelper.SaveClinicHistoryElements(ConsultFile);
            return Json(new { data = result.data, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        
        public JsonResult SaveExplorationElements(ElectronicFileAux ConsultFile)
        {
            ElectronicFileResult result = ElectronicFileHelper.SaveExplorationElements(ConsultFile);
            return Json(new { data = result.data, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        
        public JsonResult SavePreviousResult(ElectronicFileAux ConsultFile)
        {
            ElectronicFileResult result = ElectronicFileHelper.SavePreviousResult(ConsultFile);
            return Json(new { data = result.data, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        
        public JsonResult SaveServices (ElectronicFileAux ConsultFile, ServiceNoteAux services, string name, int idx, int electronicFileId, bool toPrint)
        {
            ElectronicFileResult result = ElectronicFileHelper.SaveServices(ConsultFile, services, name, idx, electronicFileId, toPrint);
            return Json(new { result.data, result.success, result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        
        public JsonResult SaveMedicaments(ElectronicFileAux ConsultFile, MedicamStock medicament)
        {
            ElectronicFileResult result = ElectronicFileHelper.SaveMedicaments(ConsultFile, medicament);
            return Json(new { result.data, result.success, result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        
        public JsonResult SaveExamns(ElectronicFileAux ConsultFile)
        {
            ElectronicFileResult result = ElectronicFileHelper.SaveExamns(ConsultFile);
            return Json(new { result.data, result.success, result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        
        public JsonResult PreSavePlan(ElectronicFileAux ConsultFile)
        {
            ElectronicFileResult result = ElectronicFileHelper.PreSavePlan(ConsultFile);
            return Json(new { data = result.data, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult LoadActiveSubstance(string data)
        {
            vwSubstanceActiveResult result = ElectronicFileHelper.LoadActiveSubstance(data);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetUnit(string data)
        {
            vwPharmaceuticalFormResult result = ElectronicFileHelper.GetUnit(data);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult LoadPresentation(string data)
        {
            vwPharmaceuticalFormResult result = ElectronicFileHelper.LoadPresentation(data);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetWayOfAdministration(string data)
        {
            WayOfAdministrationResult result = ElectronicFileHelper.GetWayOfAdministration(data);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        
        public JsonResult GetPharmaceuticalForm(vwSubstanceActiveAux item)
        {
            PharmaceuticalFormResult result = ElectronicFileHelper.GetPharmaceuticalForm(item);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        
        public JsonResult GetPresentation(PharmaceuticalFormAux unit, vwSubstanceActiveAux substance)
        {
            PresentationResult result = ElectronicFileHelper.GetPresentation(unit, substance);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        
        public JsonResult GetActiveSubstance(vwSubstanceActiveAux item)
        {
            ActiveSubstanceResult result = ElectronicFileHelper.GetActiveSubstance(item);
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        
        public FileStreamResult PrintRecipe(int evolutionNoteId)
        {
            string userName = HttpContext.User.Identity.Name;
            string htmlPrenscription = ElectronicFileHelper.BuildPlan(evolutionNoteId, userName).string_value;
            byte[] byteArray = PdfManager. HtmlToPdFNoMargin(htmlPrenscription);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }
        
        public FileStreamResult BuildRecipe(int consultFileId)
        {
            string userName = HttpContext.User.Identity.Name;
            string htmlPrenscription = ElectronicFileHelper.BuildRecipe(consultFileId, userName).string_value;
            byte[] byteArray = PdfManager.HtmlToPdFNoMargin(htmlPrenscription);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }
        
        public JsonResult CancelEvolutionNote(int Evid)
        {
            EvolutionNoteResult result = ElectronicFileHelper.CancelEvolutionNote(Evid);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        
        public JsonResult CancelReferenceNote(int RefId)
        {
            ReferenceNoteResult result = ElectronicFileHelper.CancelReferenceNote(RefId);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        
        public JsonResult CancelInterconsultNote(int InId)
        {
            InterconsultResult result = ElectronicFileHelper.CancelInterconsultNote(InId);
            return Json(new { success = result.success, data = result.data, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        
        public JsonResult GetElectronicFileUpdates(ElectronicFileAux ConsultFile)
        {
            vwElectronicFilesUpdatesAuxResult result = ElectronicFileHelper.GetElectronicFileUpdates(ConsultFile);
            return Json(new { success = result.success, data = result.data_list, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
    }
}