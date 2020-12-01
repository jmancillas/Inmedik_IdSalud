using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class SupplierAux
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public AddressAux addressAux { get; set; }

        public SupplierAux()
        {
            addressAux = new AddressAux();
        }
    }

    public class SupplierResult : Result
    {
        public SupplierAux data { get; set; }
        public List<SupplierAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public SupplierResult()
        {
            this.data = new SupplierAux();
            this.data_list = new List<SupplierAux>();
            this.total = new NumericResult();
        }
    }

    public class SupplierHelper
    {
        public static SupplierResult GetSuppliers(DTParameterModel filter)
        {
            SupplierResult result = new SupplierResult();
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
                    var query = db.Supplier.Where(pt => !pt.Deleted);
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
                    foreach (Supplier supplier in query.ToList())
                    {
                        SupplierAux aux = new SupplierAux();
                        DataHelper.fill(aux, supplier);
                        DataHelper.fill(aux.addressAux, supplier.Address);
                        DataHelper.fill(aux.addressAux.countyAux, supplier.Address.County);
                        DataHelper.fill(aux.addressAux.countyAux.cityAux, supplier.Address.County.City);
                        DataHelper.fill(aux.addressAux.countyAux.cityAux.stateAux, supplier.Address.County.City.State);
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

        public static SupplierResult GetSupplier(int id)
        {
            SupplierResult result = new SupplierResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Supplier supplier = db.Supplier.Where(pt => pt.id == id).FirstOrDefault();
                    if (supplier != null)
                    {
                        result.data = new SupplierAux();
                        DataHelper.fill(result.data, supplier);
                        DataHelper.fill(result.data.addressAux, supplier.Address);
                        DataHelper.fill(result.data.addressAux.countyAux, supplier.Address.County);
                        DataHelper.fill(result.data.addressAux.countyAux.cityAux, supplier.Address.County.City);
                        DataHelper.fill(result.data.addressAux.countyAux.cityAux.stateAux, supplier.Address.County.City.State);
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

        public static Result SaveSupplier(SupplierAux supplierAux)
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
                    if (supplierAux.id != 0)
                    {
                        Supplier supplier = db.Supplier.Where(pt => pt.id == supplierAux.id).FirstOrDefault();
                        if (supplier != null)
                        {
                            supplier.Name = supplierAux.name;
                            supplier.PhoneNumber = supplierAux.phoneNumber;
                            supplier.Email = supplierAux.email;

                            Address address = supplier.Address;
                            address.AddressLine = supplierAux.addressAux.addressLine;
                            address.PostalCode = supplierAux.addressAux.postalCode;
                            address.CountyId = supplierAux.addressAux.countyAux.id;


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
                        Supplier supplier = db.Supplier.Create();
                        supplier.Name = supplierAux.name;
                        supplier.PhoneNumber = supplierAux.phoneNumber;
                        supplier.Email = supplierAux.email;

                        Address address = db.Address.Create();
                        address.AddressLine = supplierAux.addressAux.addressLine;
                        address.PostalCode = supplierAux.addressAux.postalCode;
                        address.CountyId = supplierAux.addressAux.countyAux.id;

                        supplier.Address = address;

                        db.Supplier.Add(supplier);
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

        public static Result DeleteSupplier(SupplierAux supplierAux)
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
                    if (supplierAux.id != 0)
                    {
                        Supplier supplier = db.Supplier.Where(pt => pt.id == supplierAux.id).FirstOrDefault();
                        if (supplier != null)
                        {
                            supplier.Deleted = true;
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

        public static SupplierResult GetSuppliersSelect()
        {
            SupplierResult result = new SupplierResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Supplier.Where(c => !c.Deleted);
                    query = query.OrderBy(p => p.Name);
                    foreach (Supplier county in query.ToList())
                    {
                        SupplierAux aux = new SupplierAux();
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
