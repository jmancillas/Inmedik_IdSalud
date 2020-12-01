using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using INMEDIK.Models.Entity;

namespace INMEDIK.Models.Helpers
{
    public class BuildTemplateHelper
    {
        //Metodo que sirve para traer el html desde la base de datos y poder remplazar las variables con los valores reales
        public static GenericResult GenerateTemplate(string nameTemplate, List<KeyValuePair<string,string>> listKeyValue)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK model = new dbINMEDIK())
            {
                try
                {
                    Template template = model.Template.Where(temp => temp.Name == nameTemplate).FirstOrDefault();
                    if (template != null)
                    {
                        string htmlString = template.ContentTemplate;

                        foreach (KeyValuePair<string,string> word in listKeyValue)
                        {
                            htmlString = htmlString.Replace(word.Key, word.Value);
                        }

                        result.string_value = htmlString;
                    }
                    else
                    {
                        throw new Exception("No existe la plantilla que desea utilizar :(");
                    }

                }
                catch (Exception error)
                {
                    result.exception = error;
                    result.message = error.Message;
                }
            }

            return result;
        } 
        
        public static GenericResult GenerateTemplateError(string errorMessage)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK model = new dbINMEDIK())
            {
                try
                {
                    Template template = model.Template.Where(temp => temp.Name == "Error Template").FirstOrDefault();
                    if (template != null)
                    {
                        string htmlString = template.ContentTemplate;

                        htmlString = htmlString.Replace("$errorMessage",errorMessage);

                        result.string_value = htmlString;
                    }
                    else
                    {
                        throw new Exception("No existe la plantilla que desea utilizar :(");
                    }

                }
                catch (Exception error)
                {
                    result.exception = error;
                    result.message = error.Message;
                }
            }

            return result;
        }   
    }
}