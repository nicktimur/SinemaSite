using System;
using System.Collections.Generic;

namespace SinemaSite.Models;

public partial class Film
{
    public long Id { get; set; }

    public string? Isim { get; set; }

    public int? Sure { get; set; }

    public int FilmDurumu { get; set; } = 0;

    public List<string> Turler { get; set; } = new List<string>();

    public string ResimYolu { get; set; }

    public DateOnly VizyonTarihi { get; set; }

    public DateTime? OlusturulmaTarihi { get; set; }

    public DateTime? SilinmeTarihi { get; set; }

    public DateTime? GuncellemeTarihi { get; set; }

    public virtual ICollection<Gosterim> Gosterims { get; set; } = new List<Gosterim>();
}
