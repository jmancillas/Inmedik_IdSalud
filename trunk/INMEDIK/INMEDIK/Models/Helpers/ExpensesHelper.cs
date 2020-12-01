using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Entity;

namespace INMEDIK.Models.Helpers
{
    public class ExpensesAux
    {
        public int id { get; set; }
        public int CashClosingId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created { get; set; }
        public int idUserCreated { get; set; }
        public DateTime Updated { get; set; }
        public int idUserUdated { get; set; }
        
        public ExpensesAux()
        {
        }
    }

    public class ExpensesResult : Result
    {
        public ExpensesAux data { get; set; }
        public List<ExpensesAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public ExpensesResult()
        {
            this.data = new ExpensesAux();
            this.data_list = new List<ExpensesAux>();
            this.total = new NumericResult();
        }
    }
    public class ExpensesHelper
    {
    }
}