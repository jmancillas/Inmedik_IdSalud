using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Entity;
using System.Globalization;

namespace INMEDIK.Models.Helpers
{
    public class NationalityAux
    {
        public int id { get; set; }
        public int countryCode { get; set; }
        public string name { get; set; }
        public string nationalitykey { get; set; }

        public void fillDB(ref Nationality nationality)
        {
            nationality.id = this.id;
            nationality.Countrycode = this.countryCode;
            nationality.Name = this.name;
            nationality.Nationalitykey = this.nationalitykey;
        }
    }

    public class NationalityResult : Result
    {
        public NationalityAux data { get; set; }
        public List<NationalityAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public NationalityResult()
        {
            data = new NationalityAux();
            data_list = new List<NationalityAux>();
            total = new NumericResult();
        }
    }

    public class NationalityHelper
    {
        public static NationalityResult GetNationalitySelect()
        {
            NationalityResult result = new NationalityResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    //var query = db.Nationality.OrderBy(p => p.Name);
                    //foreach(Nationality nationality in query.ToList())
                    //{
                    //    NationalityAux aux = new NationalityAux();
                    //    DataHelper.fill(aux, nationality);
                    //    result.data_list.Add(aux);
                    //}
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