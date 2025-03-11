using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KualitasIpteks.Infrastructure.KualitasIpteks;
using UnpakSipaksi.Modules.KualitasIpteks.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KualitasIpteks.Infrastructure.Database
{
    public sealed class KualitasIpteksDbContext(DbContextOptions<KualitasIpteksDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KualitasIpteks.KualitasIpteks> KualitasIpteks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KualitasIpteks.KualitasIpteks>().ToTable(Schemas.KualitasIpteks);
            modelBuilder.ApplyConfiguration(new KualitasIpteksConfiguration());

            modelBuilder.Entity<Domain.KualitasIpteks.KualitasIpteks>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KualitasIpteks);

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
