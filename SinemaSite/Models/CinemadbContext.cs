using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace SinemaSite.Models;

public partial class CinemadbContext : DbContext
{
    public CinemadbContext()
    {
    }

    public CinemadbContext(DbContextOptions<CinemadbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Gosterim> Gosterims { get; set; }

    public virtual DbSet<Kullanici> Kullanicis { get; set; }

    public virtual DbSet<Salon> Salons { get; set; }

    public virtual DbSet<Sinema> Sinemas { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;user id=root;password=1111;database=cinemadb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.38-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("film");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GuncellemeTarihi)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("guncelleme_tarihi");
            entity.Property(e => e.Isim)
                .HasMaxLength(255)
                .HasColumnName("isim");
            entity.Property(e => e.OlusturulmaTarihi)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("olusturulma_tarihi");
            entity.Property(e => e.SilinmeTarihi)
                .HasColumnType("timestamp")
                .HasColumnName("silinme_tarihi");
            entity.Property(e => e.Sure).HasColumnName("sure");
        });

        modelBuilder.Entity<Gosterim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("gosterim");

            entity.HasIndex(e => e.FilmId, "film_id");

            entity.HasIndex(e => e.SalonId, "salon_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.GuncellemeTarihi)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("guncelleme_tarihi");
            entity.Property(e => e.OlusturulmaTarihi)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("olusturulma_tarihi");
            entity.Property(e => e.SalonId).HasColumnName("salon_id");
            entity.Property(e => e.SilinmeTarihi)
                .HasColumnType("timestamp")
                .HasColumnName("silinme_tarihi");
            entity.Property(e => e.SunumTarihi)
                .HasColumnType("datetime")
                .HasColumnName("sunum_tarihi");

            entity.HasOne(d => d.Film).WithMany(p => p.Gosterims)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("gosterim_ibfk_2");

            entity.HasOne(d => d.Salon).WithMany(p => p.Gosterims)
                .HasForeignKey(d => d.SalonId)
                .HasConstraintName("gosterim_ibfk_1");
        });

        modelBuilder.Entity<Kullanici>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("kullanici");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.KullaniciAdi, "kullanici_adi").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AktifMi).HasColumnName("aktif_mi");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.GuncellemeTarihi)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("guncelleme_tarihi");
            entity.Property(e => e.Isim)
                .HasMaxLength(255)
                .HasColumnName("isim");
            entity.Property(e => e.KullaniciAdi).HasColumnName("kullanici_adi");
            entity.Property(e => e.KullaniciTipi).HasColumnName("kullanici_tipi");
            entity.Property(e => e.OlusturulmaTarihi)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("olusturulma_tarihi");
            entity.Property(e => e.Sifre)
                .HasMaxLength(255)
                .HasColumnName("sifre");
            entity.Property(e => e.SilinmeTarihi)
                .HasColumnType("timestamp")
                .HasColumnName("silinme_tarihi");
            entity.Property(e => e.SonAktifTarih)
                .HasColumnType("timestamp")
                .HasColumnName("son_aktif_tarih");
            entity.Property(e => e.Soyisim)
                .HasMaxLength(255)
                .HasColumnName("soyisim");
        });

        modelBuilder.Entity<Salon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("salon");

            entity.HasIndex(e => e.SinemaId, "sinema_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GuncellemeTarihi)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("guncelleme_tarihi");
            entity.Property(e => e.OlusturulmaTarihi)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("olusturulma_tarihi");
            entity.Property(e => e.SalonMumarasi).HasColumnName("salon_mumarasi");
            entity.Property(e => e.SalonTipi)
                .HasColumnType("enum('Standart','IMAX','4DX')")
                .HasColumnName("salon_tipi");
            entity.Property(e => e.SilinmeTarihi)
                .HasColumnType("timestamp")
                .HasColumnName("silinme_tarihi");
            entity.Property(e => e.SinemaId).HasColumnName("sinema_id");
            entity.Property(e => e.ToplamKoltuk).HasColumnName("toplam_koltuk");

            entity.HasOne(d => d.Sinema).WithMany(p => p.Salons)
                .HasForeignKey(d => d.SinemaId)
                .HasConstraintName("salon_ibfk_1");
        });

        modelBuilder.Entity<Sinema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sinema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GuncellemeTarihi)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("guncelleme_tarihi");
            entity.Property(e => e.Isim)
                .HasMaxLength(255)
                .HasColumnName("isim");
            entity.Property(e => e.Adres)
                .HasMaxLength(255)
                .HasColumnName("konum");
            entity.Property(e => e.OlusturulmaTarihi)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("olusturulma_tarihi");
            entity.Property(e => e.SilinmeTarihi)
                .HasColumnType("timestamp")
                .HasColumnName("silinme_tarihi");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tickets");

            entity.HasIndex(e => e.GosterimId, "gosterim_id");

            entity.HasIndex(e => e.MusteriId, "musteri_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GosterimId).HasColumnName("gosterim_id");
            entity.Property(e => e.GuncellemeTarihi)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("guncelleme_tarihi");
            entity.Property(e => e.MusteriId).HasColumnName("musteri_id");
            entity.Property(e => e.OlusturulmaTarihi)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("olusturulma_tarihi");
            entity.Property(e => e.SatinAlimTarihi).HasColumnName("satin_alim_tarihi");
            entity.Property(e => e.Satır).HasColumnName("satır");
            entity.Property(e => e.SilinmeTarihi)
                .HasColumnType("timestamp")
                .HasColumnName("silinme_tarihi");
            entity.Property(e => e.Sutun).HasColumnName("sutun");

            entity.HasOne(d => d.Gosterim).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.GosterimId)
                .HasConstraintName("tickets_ibfk_1");

            entity.HasOne(d => d.Musteri).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MusteriId)
                .HasConstraintName("tickets_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
