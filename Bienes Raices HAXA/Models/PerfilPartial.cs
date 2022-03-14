using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bienes_Raices_HAXA.Models
{
    public class PerfilPartial
    {
        public int idCita { get; set; }
        public string provincia{ get; set; }
        public string canton { get; set; }
        public string direccion { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFinal { get; set; }

    }
}