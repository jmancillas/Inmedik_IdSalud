using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Entity;
using System.Globalization;
using System.Data.Entity;

namespace INMEDIK.Models.Helpers
{
    public class CIE10Aux
    {
        public int consecutivo { get; set; }
        public string catalog_Key { get; set; }
        public string nombre { get; set; }
        public string descripcion_capitulo { get; set; }
    }

    public class CIE10AuxResult : Result
    {
        public CIE10Aux data { get; set; }
        public List<CIE10Aux> data_list { get; set; }
        public NumericResult total { get; set; }

        public CIE10AuxResult()
        {
            data = new CIE10Aux();
            data_list = new List<CIE10Aux>();
            total = new NumericResult();
        }
    }

    public class CIE10Helper
    {
        public static CIE10AuxResult GetCIE10Catalog(DTParameterModel filter)
        {
            CIE10AuxResult result = new CIE10AuxResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<CIE10> query = db.CIE10;

                    foreach(DTColumn column in filter.Columns)
                    {
                        bool columnHasValue = !string.IsNullOrEmpty(column.Search.Value);
                        switch(column.Data)
                        {
                            case "consecutivo":
                                query = columnHasValue ? query.Where(u => u.Consecutivo.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "catalog_Key":
                                query = columnHasValue ? query.Where(u => u.Catalog_Key.Contains(column.Search.Value)) : query;
                                break;
                            case "nombre":
                                query = columnHasValue ? query.Where(u => u.Nombre.Contains(column.Search.Value)) : query;
                                break;
                            case "descripcion_capitulo":
                                query = columnHasValue ? query.Where(u => u.DESCRIPCION_CAPITULO.Contains(column.Search.Value)) : query;
                                break;
                            default:
                                break;
                        }

                        if(!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
                        {
                            string order = filter.Order.First().Dir;
                            string orderColumn = filter.Order.First().Data;

                            switch(orderColumn)
                            {
                                case "consecutivo":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.Consecutivo) : query.OrderByDescending(q => q.Consecutivo);
                                    break;
                                case "catalog_Key":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.Catalog_Key) : query.OrderByDescending(q => q.Catalog_Key);
                                    break;
                                case "nombre":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.Nombre) : query.OrderByDescending(q => q.Nombre);
                                    break;
                                case "descripcion_capitulo":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.DESCRIPCION_CAPITULO) : query.OrderByDescending(q => q.DESCRIPCION_CAPITULO);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach(CIE10 cie10 in query.ToList())
                    {
                        CIE10Aux aux = new CIE10Aux();
                        DataHelper.fill(aux, cie10);
                        result.data_list.Add(aux);
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
    }
}