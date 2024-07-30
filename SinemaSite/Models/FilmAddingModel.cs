using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinemaSite.Models
{
    public class FilmAddingModel
    {
        public int Id { get; set; }

        [Required]
        public string Isim { get; set; }

        [Required]
        public int? Sure { get; set; }

        public List<string> Turler { get; set; }

        public IFormFile Resim { get; set; }

        [Required]
        public DateOnly VizyonTarihi { get; set; }
    }

}
