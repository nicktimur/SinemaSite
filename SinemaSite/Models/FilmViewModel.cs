using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinemaSite.Models
{
    public class FilmViewModel
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public DateTime SunumSaati { get; set; }
        public DateTime SunumTarihi { get; set; }
        public decimal Ucret { get; set; }
        public FilmModel Film { get; set; }
        public SalonModel Salon { get; set; }
        public List<TicketModel> Tickets { get; set; }
    }

    public class FilmModel
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public List<string> Turler { get; set; }
        public string ResimYolu { get; set; }
        public DateTime VizyonTarihi { get; set; }
    }

    public class SalonModel
    {
        public int Id { get; set; }
        public string SalonNumarasi { get; set; }
        public string SalonTipi { get; set; }
        public int Satir { get; set; }
        public int Sutun { get; set; }
        public SinemaModel Sinema { get; set; }
    }

    public class SinemaModel
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public string Adres { get; set; }
    }

    public class TicketModel
    {
        public int Id { get; set; }
        public int Satir { get; set; }
        public int Sutun { get; set; }
        public int MusteriId { get; set; }
    }


}
