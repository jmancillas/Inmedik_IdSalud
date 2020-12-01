using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMEDIK.Models.Helpers
{
    public class CategoryAux
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class CategoryResult : Result
    {
        public CategoryAux data { get; set; }
        public List<CategoryAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public CategoryResult()
        {
            this.data = new CategoryAux();
            this.data_list = new List<CategoryAux>();
            this.total = new NumericResult();
        }
    }
    public class CategoryHelper
    {
        public static CategoryResult GetCategoriesSelect()
        {
            CategoryResult result = new CategoryResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Category.AsQueryable();
                    foreach (Category category in query.ToList())
                    {
                        CategoryAux aux = new CategoryAux();
                        DataHelper.fill(aux, category);
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
        public static CategoryResult GetCategories()
        {
            CategoryResult result = new CategoryResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var categoriesDb = db.Category.Where(c => c.Name != "Consultas" && c.Name != "Exámenes" && c.Name != "Incapacidades").ToList();
                    foreach (Category category in categoriesDb)
                    {
                        CategoryAux aux = new CategoryAux();
                        DataHelper.fill(aux, category);
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
