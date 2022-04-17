using System.ComponentModel.DataAnnotations;

namespace Bienes_Raices_HAXA.Models.ViewModelR
{
    public class RecuperarContrasenaViewModel
    {
        public string token { get; set; }
        [Required]
        public string contrasena { get; set; }

        [Compare("contrasena")]
        [Required]
        public string contrasena2 { get; set; }
    }
}