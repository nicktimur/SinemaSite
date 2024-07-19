using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SinemaSite.Models;

public partial class Kullanici
{
    public long Id { get; set; }

    public string? Isim { get; set; }

    public string? Soyisim { get; set; }

    public string KullaniciAdi { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sifre { get; set; } = null!;

    public int KullaniciTipi { get; set; } = 0;

    public float Bakiye { get; set; } = 0f;

    public DateTime? OlusturulmaTarihi { get; set; }

    public bool AktifMi { get; set; } = true;

    public DateTime? SilinmeTarihi { get; set; }

    public DateTime? GuncellemeTarihi { get; set; }

    public DateTime? SonAktifTarih { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
