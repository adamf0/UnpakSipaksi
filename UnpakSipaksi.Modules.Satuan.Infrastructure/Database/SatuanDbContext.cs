using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Satuan.Infrastructure.Satuan;
using UnpakSipaksi.Modules.Satuan.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.Satuan.Infrastructure.Database
{
    public sealed class SatuanDbContext(DbContextOptions<SatuanDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.Satuan.Satuan> Satuan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Satuan.Satuan>().ToTable(Schemas.Satuan);
            modelBuilder.ApplyConfiguration(new SatuanConfiguration());

            modelBuilder.Entity<Domain.Satuan.Satuan>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.Satuan);

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
