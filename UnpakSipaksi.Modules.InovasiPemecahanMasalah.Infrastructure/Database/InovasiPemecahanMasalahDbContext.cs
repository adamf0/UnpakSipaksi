using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Infrastructure.InovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Infrastructure.Database
{
    public sealed class InovasiPemecahanMasalahDbContext(DbContextOptions<InovasiPemecahanMasalahDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah> InovasiPemecahanMasalah { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah>().ToTable(Schemas.InovasiPemecahanMasalah);
            modelBuilder.ApplyConfiguration(new InovasiPemecahanMasalahConfiguration());

            modelBuilder.Entity<Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.InovasiPemecahanMasalah);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("name");

                entity.Property(e => e.BobotPDP)
                      .HasColumnName("bobot_pdp");

                entity.Property(e => e.BobotTerapan)
                      .HasColumnName("bobot_terapan");

                entity.Property(e => e.BobotKerjasama)
                      .HasColumnName("bobot_kerjasama");

                entity.Property(e => e.BobotPenelitianDasar)
                      .HasColumnName("bobot_penelitian_dasar");
            });
        }
    }
}
