using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class WarehouseAux
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class WarehouseResult : Result
    {
        public WarehouseAux data { get; set; }
        public List<WarehouseAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public WarehouseResult()
        {
            this.data = new WarehouseAux();
            this.data_list = new List<WarehouseAux>();
            this.total = new NumericResult();
        }
    }
    public class WarehouseHelper
    {
        public static WarehouseResult GetWarehouses(DTParameterModel filter)
        {
            WarehouseResult result = new WarehouseResult();
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
                    var query = db.Warehouse.Where(pt => !pt.Deleted);
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
                    foreach (Warehouse warehouse in query.ToList())
                    {
                        WarehouseAux aux = new WarehouseAux();
                        DataHelper.fill(aux, warehouse);
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

        public static WarehouseResult GetWarehouse(int id)
        {
            WarehouseResult result = new WarehouseResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Warehouse warehouse = db.Warehouse.Where(pt => pt.id == id).FirstOrDefault();
                    if (warehouse != null)
                    {
                        result.data = new WarehouseAux();
                        DataHelper.fill(result.data, warehouse);
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Colonia no encontrada.";
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

        public static Result SaveWarehouse(WarehouseAux warehouseAux)
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
                    if (warehouseAux.id != 0)
                    {
                        Warehouse warehouse = db.Warehouse.Where(pt => pt.id == warehouseAux.id).FirstOrDefault();
                        if (warehouse != null)
                        {
                            warehouse.Name = warehouseAux.name;
                            db.SaveChanges();
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = "Almacén no encontrada.";
                            return result;
                        }
                    }
                    else
                    {
                        Warehouse warehouse = new Warehouse();
                        warehouse.Name = warehouseAux.name;
                        db.Warehouse.Add(warehouse);
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

        public static Result DeleteWarehouse(WarehouseAux warehouseAux)
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
                    if (warehouseAux.id != 0)
                    {
                        Warehouse warehouse = db.Warehouse.Where(pt => pt.id == warehouseAux.id).FirstOrDefault();
                        if (warehouse != null)
                        {
                            warehouse.Deleted = true;
                            db.SaveChanges();
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = "Almacén no encontrada.";
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

        public static WarehouseResult GetWarehousesSelect()
        {
            WarehouseResult result = new WarehouseResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Warehouse.Where(c => !c.Deleted);
                    query = query.OrderBy(p => p.Name);
                    foreach (Warehouse warehouse in query.ToList())
                    {
                        WarehouseAux aux = new WarehouseAux();
                        DataHelper.fill(aux, warehouse);
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
