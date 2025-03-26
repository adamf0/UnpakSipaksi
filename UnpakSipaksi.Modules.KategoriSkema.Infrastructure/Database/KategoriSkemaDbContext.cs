using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KategoriSkema.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriSkema.Infrastructure.KategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.Infrastructure.Database
{
    public sealed class KategoriSkemaDbContext(DbContextOptions<KategoriSkemaDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KategoriSkema.KategoriSkema> KategoriSkema { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KategoriSkema.KategoriSkema>().ToTable(Schemas.KategoriSkema);
            modelBuilder.ApplyConfiguration(new KategoriSkemaConfiguration());

            modelBuilder.Entity<Domain.KategoriSkema.KategoriSkema>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KategoriSkema);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");

                entity.Property(e => e.Rule)
                      .HasColumnName("rule");

            });
        }
    }
}
