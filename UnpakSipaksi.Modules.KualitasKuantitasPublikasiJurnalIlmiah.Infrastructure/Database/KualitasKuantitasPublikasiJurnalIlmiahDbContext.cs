using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Infrastructure.KualitasKuantitasPublikasiJurnalIlmiah;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Infrastructure.Database
{
    public sealed class KualitasKuantitasPublikasiJurnalIlmiahDbContext(DbContextOptions<KualitasKuantitasPublikasiJurnalIlmiahDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah> KualitasKuantitasPublikasiJurnalIlmiah { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah>().ToTable(Schemas.KualitasKuantitasPublikasiJurnalIlmiah);
            modelBuilder.ApplyConfiguration(new KualitasKuantitasPublikasiJurnalIlmiahConfiguration());

            modelBuilder.Entity<Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KualitasKuantitasPublikasiJurnalIlmiah);

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
