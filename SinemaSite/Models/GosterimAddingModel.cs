using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SinemaSite.Models
{
    public class GosterimAddingModel
    {

        [Required]
        public int SinemaId { get; set; }

        [Required]
        public int SalonId { get; set; }

        [Required]
        public int FilmId { get; set; }

        [Required]
        public int Ucret { get; set; }

        [Required]
        public DateOnly SunumTarihi { get; set; }

        [Required]
        public TimeOnly SunumSaati { get; set; }


    }

}
