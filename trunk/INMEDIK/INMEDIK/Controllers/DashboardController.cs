using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INMEDIK.Models.Helpers;
using INMEDIK.Common;

namespace INMEDIK.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [PSAuthorize]
        [MenuData]
        public ActionResult Index()
        {
            return View();
        }
    }
}