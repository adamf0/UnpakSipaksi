using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriSumberDana.Infrastructure.KategoriSumberDana;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Infrastructure.Database
{
    public sealed class KategoriSumberDanaDbContext(DbContextOptions<KategoriSumberDanaDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KategoriSumberDana.KategoriSumberDana> KategoriSumberDana { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KategoriSumberDana.KategoriSumberDana>().ToTable(Schemas.KategoriSumberDana);
            modelBuilder.ApplyConfiguration(new KategoriSumberDanaConfiguration());

            modelBuilder.Entity<Domain.KategoriSumberDana.KategoriSumberDana>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KategoriSumberDana);

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
