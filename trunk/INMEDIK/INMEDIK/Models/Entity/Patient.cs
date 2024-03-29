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
    
    public partial class Patient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            this.AHF = new HashSet<AHF>();
            this.Cart = new HashSet<Cart>();
            this.Consult = new HashSet<Consult>();
            this.Exam = new HashSet<Exam>();
            this.Internment = new HashSet<Internment>();
            this.Laboratory = new HashSet<Laboratory>();
            this.Orders = new HashSet<Orders>();
            this.Service = new HashSet<Service>();
            this.tabletCart = new HashSet<tabletCart>();
            this.Xray = new HashSet<Xray>();
        }
    
        public int id { get; set; }
        public int PersonId { get; set; }
        public string Reference { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public Nullable<int> CreatedByClinic { get; set; }
        public Nullable<int> AppId { get; set; }
        public Nullable<int> APNPId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AHF> AHF { get; set; }
        public virtual APNP APNP { get; set; }
        public virtual APP APP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual Clinic Clinic { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consult> Consult { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exam> Exam { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Internment> Internment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Laboratory> Laboratory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual Person Person { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Service { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tabletCart> tabletCart { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Xray> Xray { get; set; }
    }
}
