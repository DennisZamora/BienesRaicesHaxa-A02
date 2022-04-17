using System.ComponentModel.DataAnnotations;

namespace Bienes_Raices_HAXA.Models.ViewModel
{
    public class RecuperarViewModel
    {
        [EmailAddress]
        [Required]
        public string email { get; set; }
    }
}