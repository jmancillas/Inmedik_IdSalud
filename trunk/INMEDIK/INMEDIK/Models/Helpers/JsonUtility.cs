using Newtonsoft.Json;
using System.Web;


namespace INMEDIK.Models.Helpers
{
    public class JsonUtility
    {
        /// <summary>
        /// Convierte un objeto a una cadena JSON codificada para uso en JavaScript
        /// </summary>
        /// <param name="Obj">Objeto a codificar</param>
        /// <returns>Cadena Json</returns>
        public static string ObjectToJsonJSEncode(object Obj)
        {
            return HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(Obj, new JsonSerializerSettings() { DateFormatString = "dd/MM/yyyy" }));
        }

    }
}