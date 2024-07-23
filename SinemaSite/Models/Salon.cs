using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SinemaSite.Models;

public partial class Salon
{
    public long Id { get; set; }

    public int? ToplamKoltuk { get; set; }

    public int? SalonNumarasi { get; set; }

    public int SalonTipi { get; set; } = 0;

    public long? SinemaId { get; set; }

    public DateTime? OlusturulmaTarihi { get; set; }

    public DateTime? SilinmeTarihi { get; set; }

    public DateTime? GuncellemeTarihi { get; set; }

    public virtual ICollection<Gosterim> Gosterims { get; set; } = new List<Gosterim>();

    [JsonIgnore]
    public virtual Sinema? Sinema { get; set; }
}
