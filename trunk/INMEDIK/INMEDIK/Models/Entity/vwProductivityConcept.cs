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
    
    public partial class vwProductivityConcept
    {
        public int Orderid { get; set; }
        public string ConceptName { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> ClinicId { get; set; }
        public Nullable<int> Quantity { get; set; }
    }
}
