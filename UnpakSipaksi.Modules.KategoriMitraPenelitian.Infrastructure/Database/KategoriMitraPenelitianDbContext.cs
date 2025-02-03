using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Infrastructure.KategoriMitraPenelitian;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Infrastructure.Database
{
    public sealed class KategoriMitraPenelitianDbContext(DbContextOptions<KategoriMitraPenelitianDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KategoriMitraPenelitian.KategoriMitraPenelitian> KategoriMitraPenelitian { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KategoriMitraPenelitian.KategoriMitraPenelitian>().ToTable(Schemas.KategoriMitraPenelitian);
            modelBuilder.ApplyConfiguration(new KategoriMitraPenelitianConfiguration());

            modelBuilder.Entity<Domain.KategoriMitraPenelitian.KategoriMitraPenelitian>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KategoriMitraPenelitian);

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
