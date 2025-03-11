using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Infrastructure.KualitasKuantitasPublikasiProsiding;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Infrastructure.Database
{
    public sealed class KualitasKuantitasPublikasiProsidingDbContext(DbContextOptions<KualitasKuantitasPublikasiProsidingDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding> KualitasKuantitasPublikasiProsiding { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding>().ToTable(Schemas.KualitasKuantitasPublikasiProsiding);
            modelBuilder.ApplyConfiguration(new KualitasKuantitasPublikasiProsidingConfiguration());

            modelBuilder.Entity<Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KualitasKuantitasPublikasiProsiding);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");

                entity.Property(e => e.Nilai)
                      .HasColumnName("nilai");

            });
        }
    }
}
