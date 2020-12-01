using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class AlmFolioAux
    {
        public int Id { get; set; }
        public int BranchRequestFolio { get; set; }
        public string sBranchRequestFolio
        {
            get { return (BranchRequestFolio).ToString("D7"); }
        }
    }

    public class AlmFolioHelper
    {

    }
}