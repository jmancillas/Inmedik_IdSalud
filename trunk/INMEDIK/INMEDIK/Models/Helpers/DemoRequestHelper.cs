using INMEDIK.Models.Entity;
using INMEDIK.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class DemoRequestAux
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string message { get; set; }
        public bool isUser { get; set; }
        public bool rejected { get; set; }
        public DateTime? created { get; set; }
        public int useId { get; set; }
        public UserAux userAux { get; set; }
        public string sCreated
        {
            get
            {
                return created.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(created.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"))
                : "";
            }
        }
        public DateTime? updated { get; set; }
        public string sUpdated
        {
            get
            {
                return updated.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(updated.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"))
                : "";
            }
        }
        
        public DemoRequestAux()
        {
            this.userAux = new UserAux();
        }

        public void fillDB(ref DemoRequest data)
        {
            data.Email = this.email;
            data.IsUser = this.isUser;
            data.Message = this.message;
            data.Name = this.name;
            data.Phone = this.phone;
            data.Rejected = this.rejected;
        }
    }
    public class DemoRequestResult : Result
    {
        public DemoRequestAux data { get; set; }
        public List<DemoRequestAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public DemoRequestResult()
        {
            data = new DemoRequestAux();
            data_list = new List<DemoRequestAux>();
            total = new NumericResult();
        }
    }
    public class DemoRequestHelper
    {
        public static DemoRequestResult GetDemoRequest(int id)
        {
            DemoRequestResult result = new DemoRequestResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    DemoRequest dataDB = db.DemoRequest.FirstOrDefault(f => f.id == id);
                    if (dataDB != null)
                    {
                        DataHelper.fill(result.data, dataDB);
                        if (dataDB.User != null)
                        {
                            result.data.userAux.fill(dataDB.User);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Datos no encontrados.";
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
        public static DemoRequestResult GetDemoRequests(DTParameterModel filter, bool? rejected = null, bool? confirmed = null)
        {
            DemoRequestResult result = new DemoRequestResult();

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
                    var query = db.DemoRequest.AsQueryable();
                    if (!rejected.HasValue && !confirmed.HasValue)
                    {
                        query = db.DemoRequest.Where(d => !d.IsUser && !(d.Rejected.HasValue && d.Rejected.Value));
                    }
                    else if (rejected.HasValue && rejected.Value)
                    {
                        query = db.DemoRequest.Where(d => !d.IsUser && d.Rejected.HasValue && d.Rejected.Value);
                    }
                    else if (confirmed.HasValue && confirmed.Value)
                    {
                        query = db.DemoRequest.Where(d => d.IsUser && d.Rejected.HasValue && !d.Rejected.Value);
                    }
                    #region filtros
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Name.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "phone" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Phone.Contains(column.Search.Value));
                        }
                        if (column.Data == "email" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Email.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "userAux.account" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.User.UserAccount.ToString().Contains(column.Search.Value));
                        }
                    }
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {

                        if (orderColumn == "name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Name);
                            }
                        }
                        if (orderColumn == "phone")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Phone);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Phone);
                            }
                        }
                        if (orderColumn == "email")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Email);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Email);
                            }
                        }
                        if (orderColumn == "rejected")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Rejected);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Rejected);
                            }
                        }
                        if (orderColumn == "sCreated")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Created);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Created);
                            }
                        }
                        if (orderColumn == "sUpdated")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Updated);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Updated);
                            }
                        }
                        if (orderColumn == "userAux.sExpiration")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.User.ExpirationDate);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.User.ExpirationDate);
                            }
                        }
                        if (orderColumn == "userAux.account")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.User.UserAccount);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.User.UserAccount);
                            }
                        }
                    }
                    #endregion

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (DemoRequest demoDB in query.ToList())
                    {
                        DemoRequestAux aux = new DemoRequestAux();
                        DataHelper.fill(aux, demoDB);
                        if (demoDB.User != null)
                        {
                            aux.userAux.fill(demoDB.User);
                        }
                        result.data_list.Add(aux);
                    }
                    result.success = true;
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
        public static GenericResult SaveDemoRequest(DemoRequestAux data)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    DemoRequest dbDemo = db.DemoRequest.Create();
                    data.fillDB(ref dbDemo);
                    db.DemoRequest.Add(dbDemo);
                    db.SaveChanges();
                    result.success = true;
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
        public static GenericResult ReplyDemoRequest(DemoRequestAux data)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                using (DbContextTransaction dbtransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        DemoRequest dbDemo = db.DemoRequest.FirstOrDefault(d => d.id == data.id);
                        if (dbDemo == null)
                        {
                            throw new Exception("Solicitud no encontrada.");
                        }
                        data.fillDB(ref dbDemo);
                        if (!data.rejected)
                        {
                            //valida email
                            if(!correosMailer.ValidaEmail(data.email))
                            {
                                throw new Exception("Correo no válido.");
                            }
                            string tempUser = data.email;
                            if (db.User.Any(u => u.UserAccount == tempUser))
                            {
                                tempUser = (tempUser + DateTime.UtcNow.Ticks);
                                tempUser = tempUser.Substring(0, Math.Min(tempUser.Length, 50));
                            }
                            //crear usuario
                            User demoUser = db.User.Create();
                            string pass = System.Web.Security.Membership.GeneratePassword(6, 1);
                            UserAux userPar = new UserAux()
                            {
                                active = true,
                                isDemo = true,
                                account = tempUser,
                                expirationDate = DateTime.UtcNow.AddDays(7),
                                password = pass
                            };
                            userPar.fillDB(ref demoUser);

                            Role rol = db.Role.Where(q => q.Name == "Admin").FirstOrDefault();

                            if (rol != null)
                            {
                                demoUser.Role.Add(rol);
                            }
                            else
                            {
                                throw new Exception("Rol no encontrado.");
                            }
                            foreach (var menuView in rol.MenuView)
                            {
                                demoUser.MenuView.Add(menuView);
                            }
                            db.User.Add(demoUser);
                            db.SaveChanges();
                            //crear persona
                            Address direcc = db.Address.Create();
                            direcc.PostalCode = "00000";
                            direcc.County = db.County.Where(c => c.Name == "Otro").FirstOrDefault();
                            direcc.AddressLine = "Desconocido";
                            db.Address.Add(direcc);

                            Person personDB = db.Person.Create();
                            PersonAux personAux = new PersonAux()
                            {
                                name = data.name,
                                lastName = "",
                                birthDate = DateTime.Today,
                                sex = "X",
                                email = data.email,
                                phoneNumber = data.phone,
                                mobile = data.phone
                            };
                            personAux.fillDB(ref personDB);
                            personDB.Address = direcc;
                            db.Person.Add(personDB);
                            db.SaveChanges();
                            //crear employee
                            Employee employeeDB = db.Employee.Create();
                            employeeDB.Person = personDB;
                            employeeDB.User = demoUser;
                            employeeDB.Code = "0000DEMO";
                            employeeDB.canCancel = true;
                            foreach (var clinic in db.Clinic.Where(c => !c.Deleted))
                            {
                                employeeDB.Clinic.Add(clinic);
                            }
                            db.Employee.Add(employeeDB);
                            db.SaveChanges();
                            dbDemo.IsUser = true;
                            dbDemo.User = demoUser;
                            //crear correo y enviarlo
                            correosMailer mailer = new correosMailer();
                            mailer.SendMail(data.email, data.name, tempUser, pass).Send();
                        }

                        db.SaveChanges();
                        dbtransaction.Commit();
                        result.success = true;
                    }
                    catch (Exception ex)
                    {
                        dbtransaction.Rollback();
                        result.success = false;
                        result.exception = ex;
                        result.message = "Ocurrió un error inesperado. " + result.exception_message;
                    }
                }
            }
            return result;
        }
    }
}