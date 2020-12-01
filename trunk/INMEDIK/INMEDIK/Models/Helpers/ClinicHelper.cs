using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class ClinicAux
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string rfc { get; set; }
        public bool allowDonations { get; set; }
        public decimal? minDonation { get; set; }
        public decimal? withdrawalAt { get; set; }
        public AddressAux addressAux { get; set; }
        public List<WarehouseAux> warehouses { get; set; }
        public List<MenuViewAux> menuViewAux { get; set; }

        public ClinicAux()
        {
            this.addressAux = new AddressAux();
            this.warehouses = new List<WarehouseAux>();
            this.menuViewAux = new List<MenuViewAux>();
        }

    }

    public class ClinicResult : Result
    {
        public ClinicAux data { get; set; }
        public List<ClinicAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public ClinicResult()
        {
            this.data = new ClinicAux();
            this.data_list = new List<ClinicAux>();
            this.total = new NumericResult();
        }
    }

    public class ClinicHelper
    {
        public static ClinicResult GetClinics(DTParameterModel filter)
        {
            ClinicResult result = new ClinicResult();
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
                    var query = db.Clinic.Where(pt => !pt.Deleted && !pt.IsExternal);
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Name.Contains(column.Search.Value));
                        }
                        if (column.Data == "id" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.id.ToString().Contains(column.Search.Value));
                        }
                    }
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
                        if (orderColumn == "id")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.id);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.id);
                            }
                        }
                    }

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (Clinic clinic in query.ToList())
                    {
                        ClinicAux aux = new ClinicAux();
                        DataHelper.fill(aux, clinic);
                        DataHelper.fill(aux.addressAux, clinic.Address);
                        DataHelper.fill(aux.addressAux.countyAux, clinic.Address.County);
                        DataHelper.fill(aux.addressAux.countyAux.cityAux, clinic.Address.County.City);
                        DataHelper.fill(aux.addressAux.countyAux.cityAux.stateAux, clinic.Address.County.City.State);
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

        public static ClinicResult GetClinicsSelectForRol(int clinicId)
        {
            var UserIsAdmin = HttpContext.Current.User.IsInRole("Admin");
            ClinicResult result = new ClinicResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<Clinic> query;
                    if (UserIsAdmin)
                    {
                        query = db.Clinic.Where(c => !c.Deleted && !c.IsExternal);
                    }
                    else
                    {
                        query = db.Clinic.Where(c => c.id == clinicId && !c.IsExternal);
                    }
                    query = query.OrderBy(p => p.Name);
                    foreach (Clinic county in query.ToList())
                    {
                        ClinicAux aux = new ClinicAux();
                        DataHelper.fill(aux, county);
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

        public static ClinicResult GetClinic(int id)
        {
            ClinicResult result = new ClinicResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Clinic clinic = db.Clinic.Where(pt => pt.id == id && !pt.IsExternal).FirstOrDefault();
                    if (clinic != null)
                    {
                        result.data = new ClinicAux();
                        DataHelper.fill(result.data, clinic);
                        DataHelper.fill(result.data.addressAux, clinic.Address);
                        DataHelper.fill(result.data.addressAux.countyAux, clinic.Address.County);
                        DataHelper.fill(result.data.addressAux.countyAux.cityAux, clinic.Address.County.City);
                        DataHelper.fill(result.data.addressAux.countyAux.cityAux.stateAux, clinic.Address.County.City.State);
                        foreach (Warehouse warehouse in clinic.Warehouse)
                        {
                            WarehouseAux aux = new WarehouseAux();
                            DataHelper.fill(aux, warehouse);
                            result.data.warehouses.Add(aux);
                        }

                        foreach (MenuView mv in clinic.MenuView)
                        {
                            MenuViewAux aux = new MenuViewAux();
                            DataHelper.fill(aux, mv);
                            result.data.menuViewAux.Add(aux);
                        }

                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Clínica no encontrada.";
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
            
        public static Result SaveClinic(ClinicAux clinicAux)
        {
            Result result = new Result();
            UserResult userRes = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!userRes.success)
            {
                return userRes;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (clinicAux.id.HasValue)
                    {
                        Clinic clinic = db.Clinic.Where(pt => pt.id == clinicAux.id.Value && !pt.IsExternal).FirstOrDefault();
                        if (clinic != null)
                        {
                            clinic.Name = clinicAux.name;
                            clinic.PhoneNumber = clinicAux.phoneNumber;
                            clinic.Email = clinicAux.email;
                            clinic.AllowDonations = clinicAux.allowDonations;
                            clinic.RFC = clinicAux.rfc;
                            clinic.WithdrawalAt = clinicAux.withdrawalAt;
                            if (clinic.AllowDonations && clinicAux.minDonation.HasValue)
                            {
                                clinic.MinDonation = clinicAux.minDonation.Value;
                            }
                            else
                            {
                                clinic.MinDonation = null;
                            }
                            Address address = clinic.Address;
                            address.AddressLine = clinicAux.addressAux.addressLine;
                            address.PostalCode = clinicAux.addressAux.postalCode;
                            address.CountyId = clinicAux.addressAux.countyAux.id;

                            clinic.Warehouse.Clear();
                            foreach (var warehouse in clinicAux.warehouses)
                            {
                                clinic.Warehouse.Add(db.Warehouse.Where(w => w.id == warehouse.id).FirstOrDefault());
                            }
                            clinic.MenuView.Clear();
                            foreach (var mv in clinicAux.menuViewAux)
                            {
                                clinic.MenuView.Add(db.MenuView.Where(m => m.id == mv.id).FirstOrDefault());
                            }

                            db.SaveChanges();
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = "Clínica no encontrada.";
                            return result;
                        }
                    }
                    else
                    {
                        Clinic clinic = db.Clinic.Create();
                        clinic.Name = clinicAux.name;
                        clinic.PhoneNumber = clinicAux.phoneNumber;
                        clinic.Email = clinicAux.email;
                        clinic.AllowDonations = clinicAux.allowDonations;
                        clinic.RFC = clinicAux.rfc;
                        clinic.IsExternal = false;
                        if (clinic.AllowDonations && clinicAux.minDonation.HasValue)
                        {
                            clinic.MinDonation = clinicAux.minDonation.Value;
                        }
                        else
                        {
                            clinic.MinDonation = null;
                        }

                        Address address = db.Address.Create();
                        address.AddressLine = clinicAux.addressAux.addressLine;
                        address.PostalCode = clinicAux.addressAux.postalCode;
                        address.CountyId = clinicAux.addressAux.countyAux.id;

                        clinic.Address = address;

                        foreach (var warehouse in clinicAux.warehouses)
                        {
                            clinic.Warehouse.Add(db.Warehouse.Where(w => w.id == warehouse.id).FirstOrDefault());
                        }

                        foreach (var mv in clinicAux.menuViewAux)
                        {
                            clinic.MenuView.Add(db.MenuView.Where(m => m.id == mv.id).FirstOrDefault());
                        }

                        db.Clinic.Add(clinic);
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

        public static Result DeleteClinic(ClinicAux clinicAux)
        {
            Result result = new Result();
            UserResult userRes = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!userRes.success)
            {
                return userRes;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (clinicAux.id.HasValue)
                    {
                        Clinic clinic = db.Clinic.Where(pt => pt.id == clinicAux.id.Value && !pt.IsExternal).FirstOrDefault();
                        if (clinic != null)
                        {
                            clinic.Deleted = true;
                            db.SaveChanges();
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = "Clínica no encontrada.";
                            return result;
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.message = "No se ha seleccionado una colonia a borrar.";
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

        public static ClinicResult GetClinicsSelect()
        {
            ClinicResult result = new ClinicResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<vwClinics> query = db.vwClinics;
                    foreach (vwClinics county in query.ToList())
                    {
                        ClinicAux aux = new ClinicAux();
                        DataHelper.fill(aux, county);
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

    }
}