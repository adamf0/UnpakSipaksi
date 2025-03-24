using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Infrastructure.PeningkatanKeberdayaanMitra;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Infrastructure.Database
{
    public sealed class PeningkatanKeberdayaanMitraDbContext(DbContextOptions<PeningkatanKeberdayaanMitraDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra> PeningkatanKeberdayaanMitra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra>().ToTable(Schemas.PeningkatanKeberdayaanMitra);
            modelBuilder.ApplyConfiguration(new PeningkatanKeberdayaanMitraConfiguration());

            modelBuilder.Entity<Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.PeningkatanKeberdayaanMitra);

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
