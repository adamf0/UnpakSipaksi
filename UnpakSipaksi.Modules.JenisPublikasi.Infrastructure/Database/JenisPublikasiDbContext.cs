using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.JenisPublikasi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.JenisPublikasi.Infrastructure.JenisPublikasi;

namespace UnpakSipaksi.Modules.JenisPublikasi.Infrastructure.Database
{
    public sealed class JenisPublikasiDbContext(DbContextOptions<JenisPublikasiDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.JenisPublikasi.JenisPublikasi> JenisPublikasi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.JenisPublikasi.JenisPublikasi>().ToTable(Schemas.JenisPublikasi);
            modelBuilder.ApplyConfiguration(new JenisPublikasiConfiguration());

            modelBuilder.Entity<Domain.JenisPublikasi.JenisPublikasi>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.JenisPublikasi);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");

                entity.Property(e => e.Sbu)
                      .HasColumnName("sbu");

            });
        }
    }
}
