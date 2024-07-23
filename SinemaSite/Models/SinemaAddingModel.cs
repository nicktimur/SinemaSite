using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SinemaSite.Models
{
    public class SinemaAddingModel
    {
        [Required]
        public string SinemaAdi { get; set; }

        [Required]
        public string SinemaAdresi { get; set; }

    }

}
