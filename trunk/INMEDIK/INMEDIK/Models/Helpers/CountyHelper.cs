using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class CountyAux
    {
        public int id { get; set; }
        public string name { get; set; }
        public CityAux cityAux { get; set; }

        public CountyAux()
        {
            cityAux = new CityAux();
        }
        public void fill(County dbCounty)
        {
            this.id = dbCounty.id;
            this.name = dbCounty.Name;
            this.cityAux = new CityAux();
            this.cityAux.fill(dbCounty.City);
        }
    }

    public class CountyResult : Result
    {
        public CountyAux data { get; set; }
        public List<CountyAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public CountyResult()
        {
            this.data = new CountyAux();
            this.data_list = new List<CountyAux>();
            this.total = new NumericResult();
        }
    }

    public class CountyHelper
    {
        public static CountyResult GetCountys(DTParameterModel filter)
        {
            CountyResult result = new CountyResult();
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
                    var query = db.County.Where(pt => !pt.Deleted);
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
                        if (column.Data == "cityAux.name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.City.Name.Contains(column.Search.Value));
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
                    foreach (County county in query.ToList())
                    {
                        CountyAux aux = new CountyAux();
                        aux.fill(county);
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

        public static CountyResult GetCounty(int id)
        {
            CountyResult result = new CountyResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    County county = db.County.Where(pt => pt.id == id).FirstOrDefault();
                    if (county != null)
                    {
                        result.data = new CountyAux();
                        result.data.fill(county);
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

        public static Result SaveCounty(CountyAux countyAux)
        {
            Result result = new Result();
            
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (countyAux.id != 0)
                    {
                        County county = db.County.Where(pt => pt.id == countyAux.id).FirstOrDefault();
                        if (county != null)
                        {
                            county.Name = countyAux.name;
                            county.CityId = countyAux.cityAux.id;
                            db.SaveChanges();
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = "Colonia no encontrada.";
                            return result;
                        }
                    }
                    else
                    {
                        County county = new County();
                        county.Name = countyAux.name;
                        county.CityId = countyAux.cityAux.id;
                        db.County.Add(county);
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

        public static Result DeleteCounty(CountyAux countyAux)
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
                    if (countyAux.id != 0)
                    {
                        County county = db.County.Where(pt => pt.id == countyAux.id).FirstOrDefault();
                        if (county != null)
                        {
                            county.Deleted = true;
                            db.SaveChanges();
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = "Colonia no encontrada.";
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

        public static CountyResult GetCountiesSelect(CityAux city)
        {
            CountyResult result = new CountyResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.County.Where(c => !c.Deleted);
                    if (city != null && city.id != 0)
                    {
                        query = query.Where(q => q.CityId == city.id);
                        query = query.OrderBy(p => p.Name);
                        foreach (County county in query.ToList())
                        {
                            CountyAux aux = new CountyAux();
                            DataHelper.fill(aux, county);
                            result.data_list.Add(aux);
                        }
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