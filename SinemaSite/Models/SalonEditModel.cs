using System.ComponentModel.DataAnnotations;

namespace SinemaSite.Models
{
    public class SalonEditModel
    {
        public int SalonId { get; set; }

        public string? SinemaAdi { get; set; }

        public string? SinemaAdres { get; set; }

        public int? SalonNumarasi { get; set; }

        public int? Satir { get; set; }

        public int? Sutun { get; set; }


    }
}
