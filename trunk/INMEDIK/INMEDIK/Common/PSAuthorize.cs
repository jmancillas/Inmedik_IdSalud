using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace INMEDIK.Common
{
    public class PSAuthorize: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string user_name = System.Web.HttpContext.Current.User.Identity.Name;
            ClinicResult CurrentClinic = new ClinicResult();
            var CurrentUser = UserHelper.GetCurrentUserAndClinic(out CurrentClinic);

            if (CurrentUser.User.isDemo.HasValue && CurrentUser.User.isDemo.Value && CurrentUser.User.expirationDate.HasValue &&
                            CurrentUser.User.expirationDate <= DateTime.UtcNow)
            {
                var Url = new UrlHelper(filterContext.RequestContext);
                var url = Url.Action("LogOut", "Authentication");
                filterContext.Result = new RedirectResult(url);
            }
            if (string.IsNullOrEmpty(user_name))
            {
                var Url = new UrlHelper(filterContext.RequestContext);
                var url = Url.Action("Index", "Authentication");
                filterContext.Result = new RedirectResult(url);
            }
            else
            {
                string controller = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
                bool permiso =
                    (controller == "Dashboard"
                    ||
                    CurrentUser.User.rolAux.name == "Admin")
                    ||
                    (CurrentClinic.data.menuViewAux.Any(m => m.controller == controller)
                    &&
                    (CurrentUser.User.rolAux.menuViewAux.Any(m => m.controller == controller)
                    ||
                    CurrentUser.User.menuViewAux.Any(m => m.controller == controller)));

                if (CurrentUser.User.isDemo.HasValue && CurrentUser.User.isDemo.Value && controller == "DemoAccess")
                {
                    permiso = false;
                }

                if (!permiso)
                {
                    var Url = new UrlHelper(filterContext.RequestContext);
                    var url = Url.Action("Index", "Dashboard");
                    filterContext.Result = new RedirectResult(url);
                }

                string userName = System.Web.HttpContext.Current.User.Identity.Name;
                GenericResult result = AuthenticationHelper.TimeOver(userName);
                if (false)//result.bool_value)
                {
                    var Url = new UrlHelper(filterContext.RequestContext);
                    var url = Url.Action("LogOut", "Authentication");
                    filterContext.Result = new RedirectResult(url);
                }
                else
                {
                    AuthenticationHelper.UpdateSessionUser(userName);
                }
            }
        }
    }
}