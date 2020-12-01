using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class StateAux
    {
        public int id { get; set; }
        public string name { get; set; }

        public void fill(State dbState)
        {
            this.id = dbState.id;
            this.name = dbState.Name;
        }
    }

    public class StateResult:Result
    {
        public StateAux data { get; set; }
        public List<StateAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public StateResult()
        {
            this.data = new StateAux();
            this.data_list = new List<StateAux>();
            this.total = new NumericResult();
        }
    }

    public class StateHelper
    {
        public static StateResult GetStatesSelect()
        {
            StateResult result = new StateResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.State.OrderBy(p => p.Name);
                    foreach (State type in query.ToList())
                    {
                        StateAux aux = new StateAux();
                        DataHelper.fill(aux, type);
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