using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class WarehouseStockAux
    {
        public string name { get; set; }
        public string code { get; set; }
        public int inStock { get; set; }
        public int minStock { get; set; }
        public int sentMaterial { get; set; }
        public string warehouseName { get; set; }
        public int spentMaterial { get; set; }
        public int warehouseStock { get; set; }
        public int clinicId { get; set; }

        public WarehouseStockAux()
        {
        }
    }
    public class WarehouseStockResult : Result
    {
        public WarehouseStockAux data { get; set; }
        public List<WarehouseStockAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public WarehouseStockResult()
        {
            this.data = new WarehouseStockAux();
            this.data_list = new List<WarehouseStockAux>();
            this.total = new NumericResult();
        }
    }
    public class WarehouseStockHelper
    {
        public static WarehouseStockResult GetWarehouseStocks(int clinicId)
        {
            WarehouseStockResult result = new WarehouseStockResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    List<vwWarehouseStock> dbWhStocks = db.vwWarehouseStock.Where(w => w.clinicId == clinicId).OrderBy(w => w.Name).ToList();
                    if (dbWhStocks != null)
                    {
                        foreach (vwWarehouseStock dbwhstock in dbWhStocks)
                        {
                            WarehouseStockAux whstock = new WarehouseStockAux();
                            DataHelper.fill(whstock, dbwhstock);
                            result.data_list.Add(whstock);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "No se encontró inventario en los subalmacenes.";
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
        public static WarehouseStockResult GetWarehouseStocks(DTParameterModel filter, int clinicId)
        {
            WarehouseStockResult result = new WarehouseStockResult();
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
                    IQueryable<vwWarehouseStock> query = db.vwWarehouseStock;
                    query = query.Where(q => q.clinicId == clinicId);

                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Name.Contains(column.Search.Value));
                        }
                        if (column.Data == "code" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                           query = query.Where(q => q.code.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "inStock" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                           query = query.Where(q => q.inStock.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "warehouseStock" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                           // query = query.Where(q => q.warehouseStock.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "warehouseName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                           // query = query.Where(q => q.warehouseName.ToString().Contains(column.Search.Value));
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
                        if (orderColumn == "code")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.code);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.code);
                            }
                        }
                        if (orderColumn == "inStock")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.inStock);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.inStock);
                            }
                        }
                        if (orderColumn == "warehouseStock")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                              //  query = query.OrderBy(q => q.warehouseStock);
                            }
                            else
                            {
                               // query = query.OrderByDescending(q => q.warehouseStock);
                            }
                        }
                        if (orderColumn == "warehouseName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                //query = query.OrderBy(q => q.warehouseName);
                            }
                            else
                            {
                               // query = query.OrderByDescending(q => q.warehouseName);
                            }
                        }
                    }
                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (vwWarehouseStock dbwhstock in query.ToList())
                    {
                        WarehouseStockAux whstock = new WarehouseStockAux();
                        DataHelper.fill(whstock, dbwhstock);
                        result.data_list.Add(whstock);
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