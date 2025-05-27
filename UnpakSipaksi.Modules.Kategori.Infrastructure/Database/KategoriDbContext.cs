using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Kategori.Infrastructure.Kategori;
using UnpakSipaksi.Modules.Kategori.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.Kategori.Infrastructure.Database
{
    public sealed class KategoriDbContext(DbContextOptions<KategoriDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.Kategori.Kategori> Kategori { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Kategori.Kategori>().ToTable(Schemas.Kategori);
            modelBuilder.ApplyConfiguration(new KategoriConfiguration());

            modelBuilder.Entity<Domain.Kategori.Kategori>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.Kategori);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");
            });
        }
    }
}
