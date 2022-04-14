using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bienes_Raices_HAXA.Models.ViewModel
{
    public class RecuperarContrasenaViewModel
    {
        public string token { get; set; }
        [Required]
        public string contrasena { get; set; }

        [Compare("contrasena")]
        [Required]
        public string contrasena2{ get; set; }
}
}