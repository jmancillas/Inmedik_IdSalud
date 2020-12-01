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
    
    public partial class CashClosing
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CashClosing()
        {
            this.DenominationByCashClose = new HashSet<DenominationByCashClose>();
            this.Expenses = new HashSet<Expenses>();
            this.Withdrawal = new HashSet<Withdrawal>();
        }
    
        public int id { get; set; }
        public int ClinicId { get; set; }
        public decimal InitialCash { get; set; }
        public Nullable<decimal> FinalCash { get; set; }
        public Nullable<decimal> RemainingOrMissing { get; set; }
        public System.DateTime DateOpened { get; set; }
        public int UserIdWhoOpened { get; set; }
        public Nullable<System.DateTime> DateClosed { get; set; }
        public Nullable<int> UserIdWhoClosed { get; set; }
        public Nullable<decimal> TotalCash { get; set; }
        public Nullable<decimal> TotalCrediCard { get; set; }
        public Nullable<decimal> TotalVoucher { get; set; }
        public Nullable<decimal> TotalExpense { get; set; }
        public Nullable<decimal> TotalCancelation { get; set; }
        public Nullable<decimal> TotalWithdrawal { get; set; }
        public Nullable<decimal> TotalSell { get; set; }
        public Nullable<int> TotalConsult { get; set; }
        public Nullable<decimal> TotalProductSell { get; set; }
        public Nullable<int> Number { get; set; }
    
        public virtual Clinic Clinic { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DenominationByCashClose> DenominationByCashClose { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expenses> Expenses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Withdrawal> Withdrawal { get; set; }
    }
}