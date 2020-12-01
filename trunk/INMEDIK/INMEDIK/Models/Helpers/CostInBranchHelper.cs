using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class CostInBranchAux
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal minPrice { get; set; }
        public decimal basePrice { get; set; }
        public ClinicAux clinicAux { get; set; }

        public CostInBranchAux()
        {
            clinicAux = new ClinicAux();
        }

        public bool IsValid()
        {
            return minPrice > 0
                && basePrice > 0;
        }
    }

    public class CostInBranchResult : Result
    {
        public CostInBranchAux data { get; set; }
        public List<CostInBranchAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public CostInBranchResult()
        {
            data = new CostInBranchAux();
            data_list = new List<CostInBranchAux>();
            total = new NumericResult();
        }
    }

    public class CostInBranchHelper
    {
    }
}