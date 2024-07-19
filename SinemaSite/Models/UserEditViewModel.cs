using System.ComponentModel.DataAnnotations;

namespace SinemaSite.Models
{
    public class UserEditViewModel
    {
        public string? KullaniciAdi { get; set; }

        public string? Isim { get; set; }

        public string? Soyisim { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

    }
}
