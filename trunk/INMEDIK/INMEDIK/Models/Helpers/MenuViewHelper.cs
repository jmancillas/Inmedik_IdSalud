using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMEDIK.Models.Helpers
{
    public class MenuViewAux
    {
        public int id { get; set; }
        public string name { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public string icon { get; set; }
        public string tooltip { get; set; }
    }

    public class MenuViewResult : Result
    {
        public MenuViewAux data { get; set; }
        public List<MenuViewAux> data_list { get; set; }

        public MenuViewResult()
        {
            data = new MenuViewAux();
            data_list = new List<MenuViewAux>();
        }
    }

    public class MenuViewHelper
    {
        public static MenuViewResult GetAllViews()
        {
            MenuViewResult result = new MenuViewResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.MenuView.Where(p => !p.Deleted).OrderBy(p => p.MenuOrder);
                    foreach (MenuView type in query.ToList())
                    {
                        MenuViewAux aux = new MenuViewAux();
                        DataHelper.fill(aux, type);
                        result.data_list.Add(aux);
                    }
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado" + result.exception_message;
                }
            }
            return result;
        }

        public static Result SaveRole(RolAux rol)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Role current = db.Role.Where(r => r.id == rol.id).FirstOrDefault();
                    if(current!=null)
                    {
                        current.MenuView.Clear();
                        foreach(MenuViewAux mv in rol.menuViewAux)
                        {
                            var view = db.MenuView.Where(v => v.id == mv.id).FirstOrDefault();
                            current.MenuView.Add(view);
                        }
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Rol no encontrado.";
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado" + result.exception_message;
                }
            }
            return result;
        }

        public static RolResult GetViewsForRole(int id)
        {
            RolResult result = new RolResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var role = db.Role.Where(r => r.id == id).FirstOrDefault();
                    if (role != null)
                    {
                        result.data = new RolAux();
                        result.data.fill(role);
                        var query = role.MenuView.OrderBy(p => p.Name);
                        foreach (MenuView type in query.ToList())
                        {
                            MenuViewAux aux = new MenuViewAux();
                            DataHelper.fill(aux, type);
                            result.data.menuViewAux.Add(aux);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Rol no encontrado.";
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado" + result.exception_message;
                }
            }
            return result;
        }
    }
}
