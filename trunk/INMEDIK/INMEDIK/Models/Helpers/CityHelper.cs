using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMEDIK.Models.Helpers
{
    public class CityAux
    {
        public int id { get; set; }
        public string name { get; set; }
        public StateAux stateAux { get; set; }

        public CityAux()
        {
            stateAux = new StateAux();
        }

        public void fill(City dbCity)
        {
            this.id = dbCity.id;
            this.name = dbCity.Name;

            StateAux aux = new StateAux();
            aux.fill(dbCity.State);
            this.stateAux = aux;
        }

    }

    public class CityResult : Result
    {
        public CityAux data { get; set; }
        public List<CityAux> data_list { get; set; }
        public CityResult()
        {
            data_list = new List<CityAux>();
        }
    }

    public class CityHelper
    {
        public static CityResult GetCities()
        {

            CityResult result = new CityResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.City;
                    foreach (City city in query)
                    {
                        CityAux aux = new CityAux();
                        aux.fill(city);
                        result.data_list.Add(aux);
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado" + result.exception_message;
                }
            }
            return result;
        }
        public static CityResult GetCitiesSelect()
        {
            CityResult result = new CityResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.City.AsQueryable();

                    query = query.OrderBy(p => p.Name);
                    foreach (City type in query.ToList())
                    {
                        CityAux aux = new CityAux();
                        aux.fill(type);
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
        public static CityResult GetCitiesSelect(StateAux state)
        {
            CityResult result = new CityResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.City.AsQueryable();
                    if (state != null && state.id != 0)
                    {
                        query = query.Where(q => q.StateId == state.id);
                        query = query.OrderBy(p => p.Name);
                        foreach (City type in query.ToList())
                        {
                            CityAux aux = new CityAux();
                            aux.fill(type);
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
