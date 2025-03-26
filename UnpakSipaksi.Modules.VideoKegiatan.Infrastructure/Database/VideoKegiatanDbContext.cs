using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.VideoKegiatan.Infrastructure.VideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.VideoKegiatan.Infrastructure.Database
{
    public sealed class VideoKegiatanDbContext(DbContextOptions<VideoKegiatanDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.VideoKegiatan.VideoKegiatan> VideoKegiatan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.VideoKegiatan.VideoKegiatan>().ToTable(Schemas.VideoKegiatan);
            modelBuilder.ApplyConfiguration(new VideoKegiatanConfiguration());

            modelBuilder.Entity<Domain.VideoKegiatan.VideoKegiatan>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.VideoKegiatan);

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
