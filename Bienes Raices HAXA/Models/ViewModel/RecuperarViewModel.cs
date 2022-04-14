using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bienes_Raices_HAXA.Models.ViewModel
{
    public class RecuperarViewModel
    {
        [EmailAddress]
        [Required]
        public string email { get; set; }
    }
}