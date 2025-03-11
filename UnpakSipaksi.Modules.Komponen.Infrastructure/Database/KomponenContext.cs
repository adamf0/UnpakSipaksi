using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Komponen.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Komponen.Infrastructure.Komponen;

namespace UnpakSipaksi.Modules.Komponen.Infrastructure.Database
{
    public sealed class KomponenContext(DbContextOptions<KomponenContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.Komponen.Komponen> Komponen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Komponen.Komponen>().ToTable(Schemas.Komponen);
            modelBuilder.ApplyConfiguration(new KomponenConfiguration());

            modelBuilder.Entity<Domain.Komponen.Komponen>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.Komponen);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");

                entity.Property(e => e.MaxBiaya)
                      .HasColumnName("max_biaya");

            });
        }
    }
}
