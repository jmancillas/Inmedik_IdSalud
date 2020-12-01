using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace INMEDIK.Controllers
{
    public class ShippingWarehouseController : Controller
    {
        // GET: ShippingWarehouse
        [MenuData]
        [Authorize]
        [PSAuthorize]
        public ActionResult Index()
        {
            return View();           
        }

        [Authorize]
        [PSAuthorize]
        public JsonResult ExistingValidation(PackageConceptAux concept)
        {
            Result result = new Result();

            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();
            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            result = ConceptHelper.ExistingValidation(concept, CurrentClinic.data.id.Value);
            return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);

        }

        [Authorize]
        [PSAuthorize]
        public JsonResult SaveShippingWarehouse(List<PackageConceptAux> concepts)
        {
            Result result = new Result();

            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();
            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);

            if (CurrentUser != null && CurrentClinic != null)
            {
                result = ConceptHelper.SaveShippingWarehouse(concepts, CurrentUser, CurrentClinic);
            }

            return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);

        }
    }
}