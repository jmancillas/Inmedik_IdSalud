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
    
    public partial class vwAlmBranchRequestResponse
    {
        public int Id { get; set; }
        public int Folio { get; set; }
        public int RequestId { get; set; }
        public int RequestFolio { get; set; }
        public string Clinic { get; set; }
        public int ClinicId { get; set; }
        public string Estatus { get; set; }
        public int AlmStatusId { get; set; }
        public System.DateTime Created { get; set; }
        public bool Seen { get; set; }
    }
}