using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class GenericHelper
    {
        public GenericHelper()
        {
            this.list_boolean = new List<bool>();
            this.list_integer = new List<int>();
            this.list_string = new List<string>();
            this.list_float = new List<float>();
            this.list_double = new List<double>();
        }
        /// <summary>
        /// Valor de la cadena de texto
        /// </summary>
        public string string_value { get; set; }
        /// <summary>
        /// Valor de un entero
        /// </summary>
        public int integer_value { get; set; }
        /// <summary>
        /// Valor de un flotante
        /// </summary>
        public float float_value { get; set; }
        /// <summary>
        /// Valor de un double
        /// </summary>
        public double double_value { get; set; }
        /// <summary>
        /// Valor de un booleano
        /// </summary>
        public bool boolean_value { get; set; }
        /// <summary>
        /// Lista de cadenas de texto
        /// </summary>
        public List<string> list_string { get; set; }
        /// <summary>
        /// Lista de enteros
        /// </summary>
        public List<int> list_integer { get; set; }
        /// <summary>
        /// Lista de flotantes
        /// </summary>
        public List<float> list_float { get; set; }
        /// <summary>
        /// Lista de doubles
        /// </summary>
        public List<double> list_double { get; set; }
        /// <summary>
        /// Lista de booleanos
        /// </summary>
        public List<bool> list_boolean { get; set; }
    }
    public class GenericService
    {
        public static Result<GenericHelper> ValidaImagen(HttpPostedFileBase file)
        {
            Resultado<GenericHelper> result = new Resultado<GenericHelper>();
            try
            {
                string file_Nombre = Path.GetFileName(file.FileName);
                string file_Extension = Path.GetExtension(file_Nombre).ToLower();

                string[] extensionesPermitidas = { ".jpg", ".png", ".bmp", ".jpeg" };
                //Si posee una estensión permitida... continuar
                if (Array.IndexOf(extensionesPermitidas, file_Extension) == -1)
                {
                    result.Exito = false;
                    result.Mensaje = "Formato de imagen no admitido.";
                }
                else
                {
                    result.Exito = true;
                }
            }
            catch (Exception)
            {
                result.Exito = false;
                result.Mensaje = "Formato no reconocido.";
            }

            return result;
        }
    }
}