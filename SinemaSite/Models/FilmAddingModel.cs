using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinemaSite.Models
{
    public class FilmAddingModel
    {
        [Required]
        public string Isim { get; set; }

        [Required]
        public int? Sure { get; set; }

        public List<string> Turler { get; set; }

        [Required(ErrorMessage = "Film posteri gereklidir.")]
        public IFormFile Resim { get; set; }
    }

}
