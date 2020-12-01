using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace INMEDIK.Common
{
    public class ObjectManager
    {
        public static bool CompareObjects(object objectFromCompare, object objectToCompare)
        {
            bool areSame = false;
            //Dictionary<string, PropertyInfo> propertiesValuesObjectFromCompare = new Dictionary<string, PropertyInfo>();
            //Dictionary<string, PropertyInfo> propertiesValuesObjectToCompare = new Dictionary<string, PropertyInfo>();

            foreach (PropertyInfo property in objectFromCompare.GetType().GetProperties())
            {

                if (property.GetValue(objectFromCompare) != null)
                {
                    object dataFromCompare = objectFromCompare.GetType().GetProperty(property.Name).GetValue(objectFromCompare).ToString();
                    object dataToCompare = objectFromCompare.GetType().GetProperty(property.Name).GetValue(objectToCompare).ToString();

                    Type typeFrom = objectFromCompare.GetType().GetProperty(property.Name).GetValue(objectFromCompare, null).GetType();
                    Type typeTo = objectFromCompare.GetType().GetProperty(property.Name).GetValue(objectToCompare, null).GetType();

                    dynamic convertedFromValue = Convert.ChangeType(dataFromCompare, typeFrom);
                    dynamic convertedToValue = Convert.ChangeType(dataToCompare, typeTo);

                    if (convertedFromValue != convertedToValue)
                    {
                        return areSame = false;
                    }
                    else
                    {
                        areSame = true;
                    }
                }
            }

            return areSame;
        }
    }
}