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

        [Required]
        public int FilmDurumu { get; set; }

        public List<string> Turler { get; set; }

        public IFormFile Resim { get; set; }

        public DateOnly VizyonTarihi { get; set; }
    }

}
