using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.Mailer;

namespace INMEDIK.Util
{
    public class correosMailer : MailerBase
    {
        public correosMailer()
        {
            MasterName = "_LayoutEmail";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool ValidaEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public virtual MvcMailMessage SendMail(string email, string nombre, string usuario, string pass)
        {
            ViewBag.nombre = nombre;
            ViewBag.usuario = usuario;
            ViewBag.pass = pass;

            return Populate(x =>
            {
                x.Subject = "ID Salud Demo";
                x.ViewName = "DemoAccessAccount";
                x.To.Add(new System.Net.Mail.MailAddress(email, "ID Salud Demo"));
            });
        }

    }
}