using INMEDIK.Models.Entity;
using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace INMEDIK.Controllers
{
    class MenuDataAttribute : FilterAttribute, IResultFilter
    {

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Cookies["_Authdata"] != null && !string.IsNullOrEmpty(filterContext.HttpContext.Request.Cookies["_Authdata"].Value))
            {
                UserResult currentUser = UserHelper.GetUser(System.Web.HttpContext.Current.User.Identity.Name);

                List<MenuViewAux> userViews = currentUser.User.menuViewAux;
                List<MenuViewAux> menuViews = new List<MenuViewAux>();
                List<MenuView> CmenuViews = new List<MenuView>();
                List<MenuView> Resultado = new List<MenuView>();

                List<MenuView> UmenuViews = new List<MenuView>();
                List<MenuView> RmenuViews = new List<MenuView>();

                if (currentUser.success && currentUser.clinicId > -1)
                {
                    ClinicResult clinicRes = ClinicHelper.GetClinic(currentUser.clinicId);
                    
                    if (clinicRes.success && clinicRes.data.menuViewAux.Count > 0)
                    {
                        if (!currentUser.User.rolesselect.Where(rs => rs.name == "Admin").Any())
                        {
                            using (dbINMEDIK db = new dbINMEDIK())
                            {                                
                                CmenuViews.AddRange(
                                    db.Clinic.Where(c => c.id == currentUser.clinicId).FirstOrDefault().MenuView
                                );
                                UmenuViews.AddRange(
                                    db.User.Where(c => c.id == currentUser.Id).FirstOrDefault().MenuView
                                );
                                RmenuViews.AddRange(
                                     db.Role.Where(c => c.id == currentUser.User.rolAux.id).FirstOrDefault().MenuView
                                );

                                Resultado = UmenuViews.Union(RmenuViews).Intersect(CmenuViews).ToList();

                                foreach (MenuView mv in Resultado.OrderBy(r => r.MenuOrder))
                                {
                                    MenuViewAux temporal = new MenuViewAux();
                                    DataHelper.fill(temporal, mv);
                                    menuViews.Add(temporal);
                                }
                            }
                        }
                        else
                        {
                            MenuViewResult viewResult = MenuViewHelper.GetAllViews();
                            
                            menuViews.AddRange(viewResult.data_list);
                        }
                        if(currentUser.User.isDemo.HasValue && currentUser.User.isDemo.Value)
                        {
                            menuViews = menuViews.Where(m => m.controller != "DemoAccess").ToList();
                        }
                        filterContext.Controller.ViewBag.Menu = menuViews;
                        filterContext.Controller.ViewBag.ClinicName = clinicRes.data.name;
                        filterContext.Controller.ViewBag.UsuarioNombre = currentUser.User.rolAux.description + " : " + currentUser.User.nombre;
                    }
                    else
                    {
                        filterContext.Controller.ViewBag.errorMessage = "No se pudieron obtener los permisos para la clínica seleccionada.";
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                        new
                        {
                            action = "Index",
                            controller = "Authentication",
                            area = ""
                        }));
                    }
                }
                else
                {
                    filterContext.Controller.ViewBag.errorMessage = "La sesión ha finalizado.";
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                        new
                        {
                            action = "Index",
                            controller = "Authentication",
                            area = ""
                        }));
                }
            }
            else
            {
                filterContext.Controller.ViewBag.errorMessage = "La sesión ha finalizado.";
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                        new
                        {
                            action = "Index",
                            controller = "Authentication",
                            area = ""
                        }));
            }
        }
    }
}
