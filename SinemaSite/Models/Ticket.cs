using System;
using System.Collections.Generic;

namespace SinemaSite.Models;

public partial class Ticket
{
    public long Id { get; set; }

    public long? GosterimId { get; set; }

    public int? Sutun { get; set; }

    public int? Satır { get; set; }

    public long? MusteriId { get; set; }

    public DateOnly? SatinAlimTarihi { get; set; }

    public DateTime? OlusturulmaTarihi { get; set; }

    public DateTime? SilinmeTarihi { get; set; }

    public DateTime? GuncellemeTarihi { get; set; }

    public virtual Gosterim? Gosterim { get; set; }

    public virtual Kullanici? Musteri { get; set; }
}
