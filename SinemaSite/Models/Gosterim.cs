using System;
using System.Collections.Generic;

namespace SinemaSite.Models;

public partial class Gosterim
{
    public long Id { get; set; }

    public long? SalonId { get; set; }

    public long? FilmId { get; set; }

    public float Ucret { get; set; }

    public DateOnly SunumTarihi { get; set; }

    public TimeOnly SunumSaati { get; set; }

    public DateTime? OlusturulmaTarihi { get; set; }

    public DateTime? SilinmeTarihi { get; set; }

    public DateTime? GuncellemeTarihi { get; set; }

    public virtual Film? Film { get; set; }

    public virtual Salon? Salon { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
