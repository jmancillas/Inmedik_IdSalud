using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INMEDIK.Models.Entity;

namespace INMEDIK.Models.Helpers
{
    public class RolResult : Result
    {
        /// <summary>
        /// Objeto con la información del usuario
        /// </summary>
        public RolAux data { get; set; }
        /// <summary>
        /// Objeto con lista de usuarios
        /// </summary>
        public List<RolAux> data_list { get; set; }

        /// <summary>
        /// Objeto lectura cuando el modo es Single
        /// </summary>
        public int Id
        {
            get
            {
                if (success)
                {
                    return data.id;
                }
                else
                {
                    return -1;
                }
            }
        }
        public NumericResult total { get; set; }
        public RolResult()
        {
            data = new RolAux();
            data_list = new List<RolAux>();
            this.total = new NumericResult();
        }
    }

    public class RolAux
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<MenuViewAux> menuViewAux { get; set; }
        public void fill(Role dbRole)
        {
            id = dbRole.id;
            name = dbRole.Name;
            description = dbRole.Description;
        }

        public void fillDB(ref Role dbRole)
        {
            dbRole.id = this.id;
            dbRole.Name = this.name;
            dbRole.Description = this.description;
        }

        public RolAux()
        {
            this.menuViewAux = new List<MenuViewAux>();
        }
    }

    public class RolSelect
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool ticked { get; set; }

        public void fill(Role dbRole)
        {
            id = dbRole.id;
            name = dbRole.Name;
        }

    }

    public static class RolHelper
    {
        /// <summary>
        /// Función que obtiene todos los rols de la base de datos
        /// </summary>
        /// <returns></returns>
        public static RolResult GetAllRols()
        {
            RolResult result = new RolResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    List<Role> RolesDb = db.Role.ToList();

                    foreach (Role rolDb in RolesDb)
                    {
                        RolAux rlaux = new RolAux();
                        rlaux.fill(rolDb);
                        result.data_list.Add(rlaux);
                    }
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }

            return result;
        }

        /// <summary>
        /// Función que obtiene un rol por su id
        /// </summary>
        /// <param name="id">id del rol a retornar</param>
        /// <returns></returns>
        public static RolResult GetRolbyId(int id)
        {
            RolResult result = new RolResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Role RolesDb = db.Role.Where(r => r.id == id).FirstOrDefault();
                    if (RolesDb != null)
                    {
                        result.data.fill(RolesDb);
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Rol no encontrado";
                    }
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }

            return result;
        }

        public static Result UserIsInRole()
        {
            throw new NotImplementedException();
        }
    }
}
