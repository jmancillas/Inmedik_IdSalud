//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace INMEDIK.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class AlmBranchBillingPayment
    {
        public int id { get; set; }
        public int AlmBranchBillingId { get; set; }
        public decimal Amount { get; set; }
        public Nullable<int> AlmFileDbId { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
    
        public virtual AlmBranchBilling AlmBranchBilling { get; set; }
        public virtual AlmFileDb AlmFileDb { get; set; }
    }
}