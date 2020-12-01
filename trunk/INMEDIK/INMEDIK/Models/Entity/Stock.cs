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
    
    public partial class Stock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stock()
        {
            this.InvoiceMovement = new HashSet<InvoiceMovement>();
            this.IReturnProducts = new HashSet<IReturnProducts>();
            this.OrdersConcepts = new HashSet<OrdersConcepts>();
            this.WarehouseSuply = new HashSet<WarehouseSuply>();
        }
    
        public int id { get; set; }
        public int ConceptId { get; set; }
        public int InStock { get; set; }
        public decimal Cost { get; set; }
        public int ClinicId { get; set; }
        public bool Iva { get; set; }
        public double CurrIva { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string Batch { get; set; }
        public Nullable<System.DateTime> ExpiredDate { get; set; }
        public Nullable<int> AlmStockId { get; set; }
        public string Code { get; set; }
    
        public virtual Clinic Clinic { get; set; }
        public virtual Concept Concept { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceMovement> InvoiceMovement { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IReturnProducts> IReturnProducts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdersConcepts> OrdersConcepts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarehouseSuply> WarehouseSuply { get; set; }
    }
}