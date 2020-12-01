using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Entity;

namespace INMEDIK.Models.Helpers
{
    public class SpecialtyAux
    {
        public int id { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public SpecialtyAux()
        {
        }
    }
    public class SpecialtyResult : Result
    {
        public SpecialtyAux data { get; set; }
        public List<SpecialtyAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public SpecialtyResult()
        {
            this.data = new SpecialtyAux();
            this.data_list = new List<SpecialtyAux>();
            this.total = new NumericResult();
        }
    }
    public class SpecialtyHelper
    {
        public static SpecialtyResult GetSpecialty(int id)
        {
            SpecialtyResult result = new SpecialtyResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Specialty dbSpec = db.Specialty.Where(s => s.id == id).FirstOrDefault();
                    if (dbSpec != null)
                    {
                        DataHelper.fill(result.data, dbSpec);
                        result.success = true;
                    }
                    else
                    {
                        result.message = "Especialidad no encontrado";
                        result.success = false;
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
        public static SpecialtyResult GetSpecialties()
        {
            SpecialtyResult result = new SpecialtyResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Specialty.AsQueryable();
                    foreach (Specialty specialty in query.ToList())
                    {
                        SpecialtyAux aux = new SpecialtyAux();
                        DataHelper.fill(aux, specialty);
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