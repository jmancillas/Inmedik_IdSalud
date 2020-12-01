using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INMEDIK.Controllers
{
    public class CategoryController : Controller
    {
        [HttpPost]
        [Authorize]
        public JsonResult GetCategoriesSelect()
        {
            CategoryResult res = CategoryHelper.GetCategoriesSelect();
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCategories()
        {
            CategoryResult res = CategoryHelper.GetCategories();
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
    }
}