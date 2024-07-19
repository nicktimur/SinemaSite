using System;
using System.Collections.Generic;

namespace SinemaSite.Models;

public partial class Admin
{
    public long Id { get; set; }

    public long? KullaniciId { get; set; }

    public int ErisimSeviyesi { get; set; }

    public DateTime? OlusturulmaTarihi { get; set; }

    public DateTime? SilinmeTarihi { get; set; }

    public DateTime? GuncellemeTarihi { get; set; }

    public DateTime? SonAktifTarih { get; set; }

    public virtual Kullanici? Kullanici { get; set; }
}
