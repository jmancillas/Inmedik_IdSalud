using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMEDIK.Models.Helpers
{
    public class ParameterAux
    {
        public string name { get; set; }
        public string value { get; set; }
        public string type { get; set; }
        public string showName { get; set; }
    }

    public class ParameterResult : Result
    {
        public List<ParameterAux> data_list { get; set; }

        public ParameterResult()
        {
            this.data_list = new List<ParameterAux>();
        }
    }

    public class ParameterHelper
    {
        public static NumericResult GetTax()
        {
            NumericResult result = new NumericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var param = db.Parameter.Where(p => p.Name == "Iva").FirstOrDefault();
                    if(param != null)
                    {
                        int val;
                        if (int.TryParse(param.Value, out val))
                        {
                            result.value = val;
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = "Es necesario configurar el Iva.";
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Es necesario configurar el Iva.";
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

        public static Result SaveParameters(List<ParameterAux> parameters)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    foreach(ParameterAux parameter in parameters)
                    {
                        var par = db.Parameter.Where(p => p.Name == parameter.name).FirstOrDefault();
                        if(par != null)
                        {
                            par.Value = parameter.value;
                        }
                    }
                    db.SaveChanges();
                    result.success = true;
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

        public static NumericResult GetCardCommission()
        {
            NumericResult result = new NumericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var param = db.Parameter.Where(p => p.Name == "CardCommission").FirstOrDefault();
                    if (param != null)
                    {
                        int val;
                        if (int.TryParse(param.Value, out val))
                        {
                            result.value = val;
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = "Es necesario configurar la comisión por pago con tarjeta.";
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Es necesario configurar la comisión por pago con tarjeta.";
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

        public static ParameterResult GetParameters()
        {
            ParameterResult result = new ParameterResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    foreach (var param in db.Parameter)
                    {
                        ParameterAux aux = new ParameterAux();
                        DataHelper.fill(aux, param);
                        result.data_list.Add(aux);
                    }
                    result.success = true;
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

        public static NumericResult GetRefreshTimeAgenda()
        {
            NumericResult result = new NumericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var param = db.Parameter.Where(p => p.Name == "RefreshTimeAgenda").FirstOrDefault();
                    if (param != null)
                    {
                        int val;
                        if (int.TryParse(param.Value, out val))
                        {
                            result.value = val;
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = "Es necesario configurar el tiempo de refresqueo para la tabla en agenda.";
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Es necesario configurar el tiempo de refresqueo para la tabla en agenda.";
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

        public static NumericResult GetRefreshTimeCashClosed()
        {
            NumericResult result = new NumericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var param = db.Parameter.Where(p => p.Name == "RefreshTimeCashClosed").FirstOrDefault();
                    if (param != null)
                    {
                        int val;
                        if (int.TryParse(param.Value, out val))
                        {
                            result.value = val;
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = "Es necesario configurar el tiempo de inactividad para salir al hacer corte de caja.";
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Es necesario configurar el tiempo de inactividad para salir al hacer corte de caja.";
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
    }
}
