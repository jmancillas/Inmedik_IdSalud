using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace INMEDIK.Controllers
{
    class PSRoleProvider : RoleProvider
    {
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return this.FindUsersInRole(roleName, usernameToMatch);
        }
        public override string[] GetUsersInRole(string roleName)
        {
            return this.GetUsersInRole(roleName);
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            this.RemoveUsersFromRoles(usernames, roleNames);
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            //base.AddUsersToRoles(usernames, roleNames);
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return this.DeleteRole(roleName, throwOnPopulatedRole);
        }
        public override bool RoleExists(string roleName)
        {
            return this.RoleExists(roleName);
        }
        public override void CreateRole(string roleName)
        {
            this.CreateRole(roleName);
        }
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Verifica que el usuario exista en un rol
        /// </summary>
        /// <param name="username">User account</param>
        /// <param name="roleName">Rol al que debe pertenecer</param>
        /// <returns>Verdadero si pertenece al Rol, Falso si no pertenece</returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            bool flag = false;
            using (dbINMEDIK db = new dbINMEDIK())
            {
                User us = db.User.Where(q => q.UserAccount == username).FirstOrDefault();
                foreach (Role ur in us.Role)
                {
                    if (ur.Name == roleName)
                    {
                        flag = true;
                    }
                }

            }

            return flag;
        }

        /// <summary>
        /// Get all the User´s Roles
        /// </summary>
        /// <param name="username">User Account</param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                User user = db.User.Where(q => q.UserAccount == username).FirstOrDefault();

                foreach (Role ur in user.Role)
                {
                    roles.Add(ur.Name);
                }


            }
            return roles.ToArray();
        }


        /// <summary>
        /// Obtiene una lista de todos los roles
        /// </summary>
        /// <returns></returns>
        public override string[] GetAllRoles()
        {
            List<string> roles = new List<string>();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                List<Role> dbRoles = db.Role.ToList();
                if (dbRoles != null)
                {
                    foreach (Role rol in dbRoles)
                    {
                        roles.Add(rol.Name);
                    }
                }
            }
            return roles.ToArray();
        }
    }
}
