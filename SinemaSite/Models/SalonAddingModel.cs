using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SinemaSite.Models
{
    public class SalonAddingModel
    {
        [Required]
        public int SalonNumarasi { get; set; }

        [Required]
        public int? Sutun { get; set; }

        [Required]
        public int? Satir { get; set; }

        [Required]
        public int SalonTipi { get; set; }

        [Required]
        public int SinemaId { get; set; }
    }

}
