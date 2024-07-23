using System.ComponentModel.DataAnnotations;

namespace SinemaSite.Models
{
    public class AdminUserEditViewModel
    {
        public int Id { get; set; }

        public string? KullaniciAdi { get; set; }

        public string? Isim { get; set; }

        public string? Soyisim { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public int? KullaniciTipi { get; set; }

        public bool? AktifMi {  get; set; }
    }
}
