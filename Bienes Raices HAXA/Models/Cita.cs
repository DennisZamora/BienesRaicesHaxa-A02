//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bienes_Raices_HAXA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cita
    {
        public long idCita { get; set; }
        public long idUsuario { get; set; }
        public long idPropiedad { get; set; }
        public string titulo { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFinal { get; set; }
    
        public virtual Propiedad Propiedad { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}