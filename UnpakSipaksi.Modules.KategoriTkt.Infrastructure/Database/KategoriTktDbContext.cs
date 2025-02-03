using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KategoriTkt.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriTkt.Infrastructure.KategoriTkt;

namespace UnpakSipaksi.Modules.KategoriTkt.Infrastructure.Database
{
    public sealed class KategoriTktDbContext(DbContextOptions<KategoriTktDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KategoriTkt.KategoriTkt> KategoriTkt { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KategoriTkt.KategoriTkt>().ToTable(Schemas.KategoriTkt);
            modelBuilder.ApplyConfiguration(new KategoriTktConfiguration());

            modelBuilder.Entity<Domain.KategoriTkt.KategoriTkt>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KategoriTkt);

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
