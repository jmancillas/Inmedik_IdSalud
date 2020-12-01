using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INMEDIK.Models.Entity;
using INMEDIK.Models.Helpers;
using System.Text;
using INMEDIK.Common;

namespace INMEDIK.Controllers
{
    [Authorize]
    public class RolConfigurationController : Controller
    {

        // GET: RolesConfiguration
        [MenuData]
        [PSAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [PSAuthorize]
        public JsonResult GetRoles()
        {
            RolResult result = RolHelper.GetAllRols();
            return Json(new { data = result.data_list }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetAllViews()
        {
            MenuViewResult result = MenuViewHelper.GetAllViews();
            return Json(new { data = result.data_list, message = result.message, success = result.success }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PSAuthorize]
        public JsonResult GetViewsForRole(int id)
        {
            RolResult result = MenuViewHelper.GetViewsForRole(id);
            return Json(new { data = result.data, message = result.message, success = result.success }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PSAuthorize]
        public JsonResult SaveRole(RolAux rol)
        {
            Result result = MenuViewHelper.SaveRole(rol);
            return Json(new { message = result.message, success = result.success }, JsonRequestBehavior.AllowGet);
        }
    }
}
