using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace INMEDIK.Models.Helpers
{
    public class DataHelper
    {
        public static void fill(object toObject, object fromObject)
        {
            Dictionary<string, PropertyInfo> propertiesTo = new Dictionary<string, System.Reflection.PropertyInfo>();
            foreach (PropertyInfo propertyTo in toObject.GetType().GetProperties())
            {
                propertiesTo.Add(propertyTo.Name.ToUpper(), propertyTo);
            }
            foreach (PropertyInfo propertyFrom in fromObject.GetType().GetProperties())
            {
                string name = propertyFrom.Name;
                if (propertiesTo.ContainsKey(name.ToUpper()) && propertyFrom.GetValue(fromObject) != null)
                {
                    object valueFrom = propertyFrom.GetValue(fromObject);
                    PropertyInfo pTo = propertiesTo[name.ToUpper()];
                    Type realType = Nullable.GetUnderlyingType(pTo.PropertyType);
                    if (pTo.PropertyType != propertyFrom.PropertyType)
                    {
                        if (realType == null)
                        {
                            valueFrom = Convert.ChangeType(valueFrom, pTo.PropertyType);
                        }
                        else
                        {
                            valueFrom = (valueFrom == null) ? null : Convert.ChangeType(valueFrom, realType);
                        }
                    }
                    pTo.SetValue(toObject, valueFrom);
                }
            }

        }
    }
}