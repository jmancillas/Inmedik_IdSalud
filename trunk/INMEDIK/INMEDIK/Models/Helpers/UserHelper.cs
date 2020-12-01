using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INMEDIK.Models.Entity;
using System.Web;
using System.Globalization;

namespace INMEDIK.Models.Helpers
{
    public class UserResult : Result
    {
        /// <summary>
        /// Objeto con la información del usuario
        /// </summary>
        public UserAux User { get; set; }
        /// <summary>
        /// Objeto con lista de usuarios
        /// </summary>
        public List<UserAux> Users { get; set; }

        /// <summary>
        /// Objeto lectura cuando el modo es Single
        /// </summary>
        public int Id
        {
            get
            {
                if (success)
                {
                    return User.id.Value;
                }
                else
                {
                    return -1;
                }
            }
        }
        public int clinicId
        {
            get
            {
                if (success && User.currentClinicId.HasValue)
                {
                    return User.currentClinicId.Value;
                }
                else
                {
                    return -1;
                }
            }
        }
        public NumericResult total { get; set; }
        public UserResult()
        {
            User = new UserAux();
            Users = new List<UserAux>();
            this.total = new NumericResult();
        }
    }

    public class UserAux
    {
        public int? id { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public bool active { get; set; }
        public int? currentClinicId { get; set; }
        public ClinicAux currentClinic { get; set; }
        public RolAux rolAux { get; set; }
        public int updatedBy { get; set; }
        public int createdBy { get; set; }
        public bool? isDemo { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? updatedDate { get; set; }
        public DateTime? expirationDate { get; set; }
        public string sExpiration
        {
            get
            {
                return expirationDate.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(expirationDate.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"))
                : "";
            }
        }
        public bool authenticator { get; set; } = false;
        public List<RolSelect> rolesselect { get; set; }
        public List<MenuViewAux> menuViewAux { get; set; }

        public UserAux()
        {
            this.rolAux = new RolAux();
            this.rolesselect = new List<RolSelect>();
            this.menuViewAux = new List<MenuViewAux>();
            this.currentClinic = new ClinicAux();
        }

        public void fill(User dbUser)
        {
            this.id = dbUser.id;
            this.account = dbUser.UserAccount;
            this.active = dbUser.UserActive.Value;
            this.updatedBy = dbUser.UpdatedBy;
            this.createdBy = dbUser.CreatedBy;
            this.authenticator = dbUser.Authenticator;
            this.isDemo = dbUser.IsDemo;
            this.expirationDate = dbUser.ExpirationDate;
            this.rolAux.fill(dbUser.Role.FirstOrDefault());
            this.currentClinicId = dbUser.CurrentClinicId;
            this.createdDate = dbUser.CreatedDate;
            this.updatedDate = dbUser.UpdatedDate;
            if (dbUser.Employee != null)
            {
                Employee empDB = dbUser.Employee.FirstOrDefault();
                if (empDB != null)
                {
                    this.nombre = empDB.Person.Name + " " + empDB.Person.LastName;
                }
            }

            RolResult result = RolHelper.GetAllRols();
            foreach (var rol in dbUser.Role.ToList())
            {
                RolSelect aux = new RolSelect()
                {
                    id = rol.id,
                    name = rol.Name,
                };

                this.rolesselect.Add(aux);
            }
            foreach (var view in dbUser.MenuView)
            {
                MenuViewAux menuview = new MenuViewAux();
                DataHelper.fill(menuview, view);
                this.menuViewAux.Add(menuview);
            }

        }
        public void fillDB(ref User dbUser)
        {
            if (this.id.HasValue)
            {
                dbUser.id = this.id.Value;
            }
            dbUser.UserAccount = this.account;
            dbUser.UserActive = true;
            if (!string.IsNullOrEmpty(this.password))
            {
                dbUser.UserPassword = AuthenticationHelper.Encrypt(this.password);
            }
            UserResult currentUser = UserHelper.GetUser(System.Web.HttpContext.Current.User.Identity.Name);
            dbUser.UpdatedDate = DateTime.UtcNow;
            dbUser.UpdatedBy = currentUser.Id;
            if (!this.id.HasValue)
            {
                dbUser.CreatedBy = currentUser.Id;
                dbUser.CreatedDate = DateTime.UtcNow;
            }
            dbUser.Authenticator = this.authenticator;
            dbUser.IsDemo = this.isDemo;
            dbUser.ExpirationDate = this.expirationDate.HasValue ? this.expirationDate : (DateTime?)null;
        }
    }

    public class UserHelper
    {
        /// <summary>
        /// Se obtiene el usuario por su cuenta
        /// </summary>
        /// <param name="account">cuenta del usuario a obtener</param>
        /// <returns></returns>
        public static UserResult GetUser(string account)
        {
            UserResult result = new UserResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    User userDb = db.User.Where(u => u.UserAccount.ToLower().Trim() == account.ToLower().Trim() && u.UserActive == true).FirstOrDefault();
                    if (userDb != null)
                    {
                        result.User.fill(userDb);
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Usuario no encontrado";
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
        public static UserResult GetUserAndClinic(string account)
        {
            UserResult result = new UserResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    User userDb = db.User.Where(u => u.UserAccount.ToLower().Trim() == account.ToLower().Trim() && u.UserActive == true).FirstOrDefault();
                    if (userDb != null)
                    {
                        result.User.fill(userDb);
                        DataHelper.fill(result.User.currentClinic, userDb.Clinic);
                        foreach (var menu in userDb.Clinic.MenuView)
                        {
                            MenuViewAux aux = new MenuViewAux();
                            DataHelper.fill(aux, menu);
                            result.User.currentClinic.menuViewAux.Add(aux);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Usuario no encontrado";
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

        /// <summary>
        /// Obtiene todos los usuarios de la base de datos por medio de filtros
        /// </summary>
        /// <param name="filter">Objeto del datatable con los filtros necesarios</param>
        /// <returns></returns>
        public static UserResult GetAllUsers(DTParameterModel filter)
        {
            UserResult result = new UserResult();
            string order = "";
            string orderColumn = "";

            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {

                    var query = db.User.Where(pt => pt.UserActive == true);
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "account" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.UserAccount.ToString().Contains(column.Search.Value));
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "account")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.UserAccount);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.UserAccount);
                            }
                        }
                    }

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);

                    foreach (User userDb in query.ToList())
                    {
                        UserAux usraux = new UserAux();
                        usraux.fill(userDb);
                        result.Users.Add(usraux);
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
        /// Función que obtiene un usuario por medio del id
        /// </summary>
        /// <param name="id">id del usuario a retornar</param>
        /// <returns></returns>
        public static UserResult LoadUsr(int id)
        {
            UserResult result = new UserResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    User type = db.User.Where(pt => pt.id == id).FirstOrDefault();
                    if (type != null)
                    {
                        result.User = new UserAux();
                        result.User.fill(type);
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Tipo de producto no encontrado.";
                        return result;
                    }
                }

                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        /// <summary>
        /// Funcion que inaciva un usuario
        /// </summary>
        /// <param name="Userpar">Objeto de usuario a inactivar</param>
        /// <returns></returns>
        public static UserResult DeleteUser(UserAux Userpar)
        {
            UserResult result = new UserResult();
            UserResult userRes = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!userRes.success)
            {
                return userRes;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (Userpar.id.HasValue)
                    {
                        User type = db.User.Where(pt => pt.id == Userpar.id).FirstOrDefault();
                        if (type != null)
                        {
                            type.UserActive = false;
                            type.UpdatedBy = userRes.Id;
                            type.UpdatedDate = DateTime.UtcNow;
                            db.SaveChanges();
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = "Usuario no encontrado.";
                            return result;
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.message = "No se ha seleccionado usuario a borrar.";
                        return result;
                    }

                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }
        public static GenericResult SetAuthenticator(int userId, bool authenticator)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    User dbUser = db.User.Where(u => u.id == userId).FirstOrDefault();
                    if (dbUser != null)
                    {
                        dbUser.Authenticator = authenticator;
                        dbUser.UpdatedDate = DateTime.UtcNow;
                        dbUser.UpdatedBy = userId;
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Usuario no encontrado";
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }
        public static void SaveClinicUser(UserAux user)
        {
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    User temp = db.User.FirstOrDefault(u => u.id == user.id);
                    temp.CurrentClinicId = user.currentClinicId;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        /// <summary>
        /// Función que guarda un usuario en la base de datos
        /// </summary>
        /// <param name="Userpar">Objeto de usuario a guardar</param>
        /// <returns></returns>
        public static UserResult SaveUser(UserAux Userpar)
        {
            UserResult result = new UserResult();
            UserResult userRes = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!userRes.success)
            {
                return userRes;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (Userpar.id.HasValue)
                    {
                        User type = db.User.Where(pt => pt.id == Userpar.id).FirstOrDefault();
                        if (type != null)
                        {

                            Userpar.fillDB(ref type);

                            //Se limpian los roles para llenar con los que selecciono
                            type.Role.Clear();
                            foreach (var item in Userpar.rolesselect)
                            {
                                Role rol = db.Role.Where(q => q.id == item.id).FirstOrDefault();

                                if (rol != null)
                                {
                                    type.Role.Add(rol);
                                }
                                else
                                {
                                    result.success = false;
                                    result.message = "Rol no encontrado.";
                                    return result;
                                }

                            }

                            db.SaveChanges();
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = "Usuario no encontrado.";
                            return result;
                        }
                    }
                    else
                    {
                        User type = new User();
                        Userpar.fillDB(ref type);

                        foreach (var item in Userpar.rolesselect)
                        {
                            Role rol = db.Role.Where(q => q.id == item.id).FirstOrDefault();

                            if (rol != null)
                            {
                                type.Role.Add(rol);
                            }
                            else
                            {
                                result.success = false;
                                result.message = "Rol no encontrado.";
                                return result;
                            }
                        }
                        db.User.Add(type);
                        db.SaveChanges();

                        result.success = true;
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        /// <summary>
        /// Se obtienen todos los usuarios por medio de un rol
        /// </summary>
        /// <param name="idrol">id del rol</param>
        /// <returns></returns>
        public static UserResult GetAllUsersbyRol(int idrol)
        {
            UserResult result = new UserResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    List<User> UsersDb = db.User.Where(u => u.Role.Where(i => i.id == idrol).Any() && u.UserActive == true).ToList();

                    foreach (User UserDb in UsersDb)
                    {
                        UserAux empaux = new UserAux();
                        empaux.fill(UserDb);
                        result.Users.Add(empaux);
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
        /// Funcion que obtiene el usuario que esta actualmente logueado
        /// </summary>
        /// <returns></returns>
        public static UserResult GetCurrentUserAndClinic(out ClinicResult ClurrentClinic)
        {
            UserResult userRes = new UserResult();
            ClurrentClinic = new ClinicResult();

            UserResult CurrentUser = UserHelper.GetUserAndClinic(HttpContext.Current.User.Identity.Name);
            if (CurrentUser.clinicId > -1)
            {
                userRes = CurrentUser;
                ClurrentClinic.data = CurrentUser.User.currentClinic;
            }
            else
            {
                userRes.message = "La sesión ha finalizado.";
                userRes.success = false;
            }

            return userRes;
        }
        /// <summary>
        /// Funcion que obtiene el usuario que esta actualmente logueado
        /// </summary>
        /// <returns></returns>
        public static UserResult GetCurrentUserAndClinic(HttpRequestBase Request, out ClinicResult ClurrentClinic)
        {
            UserResult userRes = new UserResult();
            ClurrentClinic = new ClinicResult();

            if (Request.Cookies["_Authdata"] != null && !string.IsNullOrEmpty(Request.Cookies["_Authdata"].Value))
            {

                UserResult CurrentUser = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
                if (CurrentUser.clinicId > -1)
                {
                    userRes = CurrentUser;
                    ClurrentClinic = ClinicHelper.GetClinic(CurrentUser.clinicId);
                }
                else
                {
                    userRes.message = "La sesión ha finalizado.";
                    userRes.success = false;
                }
            }
            else
            {
                userRes.message = "La sesión ha finalizado.";
                userRes.success = false;
            }

            return userRes;
        }
    }
}
