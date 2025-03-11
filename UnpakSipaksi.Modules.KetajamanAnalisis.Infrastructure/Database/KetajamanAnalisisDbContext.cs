using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KetajamanAnalisis.Infrastructure.KetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Infrastructure.Database
{
    public sealed class KetajamanAnalisisDbContext(DbContextOptions<KetajamanAnalisisDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KetajamanAnalisis.KetajamanAnalisis> KetajamanAnalisis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KetajamanAnalisis.KetajamanAnalisis>().ToTable(Schemas.KetajamanAnalisis);
            modelBuilder.ApplyConfiguration(new KetajamanAnalisisConfiguration());

            modelBuilder.Entity<Domain.KetajamanAnalisis.KetajamanAnalisis>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KetajamanAnalisis);

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
