using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMEDIK.Models.Helpers
{
    public class Result
    {
        public string message { get; set; }
        /// <summary>
        /// Mensaje de retorno
        /// </summary>
        public string exception_message
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                if (exception == null)
                {
                    return "";
                }

                for (int index = 0; index < 5; ++index)
                {
                    stringBuilder.AppendLine(exception.Message);
                    if (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                    else
                    {
                        break;
                    }
                }

                return stringBuilder.ToString();
            }
        }
        /// <summary>
        /// Lista de errores en caso de que sean varios
        /// </summary>
        public List<string> errors { get; set; }
        /// <summary>
        /// Indicador de éxito o fracaso
        /// </summary>
        public bool success { get; set; } = false;
        /// <summary>
        /// Excepción en caso de succes falso
        /// </summary>
        public Exception exception { get; set; }
        /// <summary>
        /// Variable para diferenciar si es lista u objeto simple
        /// </summary>
        public ResultMode mode { get; set; } = ResultMode.Single;
        /// <summary>
        /// Lista de excepciones
        /// </summary>
        public List<Exception> exception_list { get; set; }

        public Result()
        {
            this.exception_list = new List<Exception>();
            errors = new List<string>();
        }
    }
    public enum ResultMode
    {
        Single,
        List
    }
    public class NumericResult : Result
    {
        public decimal value { get; set; }
        public Int32 IntValue
        {
            get
            {
                return Convert.ToInt32(value);
            }
        }
        public double DoubleValue
        {
            get
            {
                return Convert.ToDouble(value);
            }
        }
    }

    public class GenericResult : Result
    {
        public string string_value { get; set; }
        public int integer_value { get; set; }
        public bool bool_value { get; set; }
    }
}
