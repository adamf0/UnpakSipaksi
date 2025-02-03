using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.Abstractions.Data;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Infrastructure.ArtikelMediaMassa;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Infrastructure.Database
{
    public sealed class ArtikelMediaMassaDbContext(DbContextOptions<ArtikelMediaMassaDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.ArtikelMediaMassa.ArtikelMediaMassa> ArtikelMediaMassa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.ArtikelMediaMassa.ArtikelMediaMassa>().ToTable(Schemas.ArtikelMediaMassa);
            modelBuilder.ApplyConfiguration(new ArtikelMediaMassaConfiguration());

            modelBuilder.Entity<Domain.ArtikelMediaMassa.ArtikelMediaMassa>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.ArtikelMediaMassa);

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
