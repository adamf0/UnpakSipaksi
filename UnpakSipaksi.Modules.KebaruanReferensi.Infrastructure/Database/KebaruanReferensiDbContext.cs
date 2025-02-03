using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KebaruanReferensi.Infrastructure.KebaruanReferensi;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Infrastructure.Database
{
    public sealed class KebaruanReferensiDbContext(DbContextOptions<KebaruanReferensiDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KebaruanReferensi.KebaruanReferensi> KebaruanReferensi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KebaruanReferensi.KebaruanReferensi>().ToTable(Schemas.KebaruanReferensi);
            modelBuilder.ApplyConfiguration(new KebaruanReferensiConfiguration());

            modelBuilder.Entity<Domain.KebaruanReferensi.KebaruanReferensi>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KebaruanReferensi);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("name");

                entity.Property(e => e.Skor)
                      .HasColumnName("skor");

            });
        }
    }
}
