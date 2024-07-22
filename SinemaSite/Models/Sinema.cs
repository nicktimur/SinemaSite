using System;
using System.Collections.Generic;

namespace SinemaSite.Models;

public partial class Sinema
{
    public long Id { get; set; }

    public string Isim { get; set; }

    public string Adres { get; set; }

    public DateTime? OlusturulmaTarihi { get; set; }

    public DateTime? SilinmeTarihi { get; set; }

    public DateTime? GuncellemeTarihi { get; set; }

    public virtual ICollection<Salon> Salons { get; set; } = new List<Salon>();
}
