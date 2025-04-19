using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Referensi.Infrastructure.Referensi;
using UnpakSipaksi.Modules.Referensi.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.Referensi.Infrastructure.Database
{
    public sealed class ReferensiDbContext(DbContextOptions<ReferensiDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.Referensi.Referensi> Referensi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Referensi.Referensi>().ToTable(Schemas.Referensi);
            modelBuilder.ApplyConfiguration(new ReferensiConfiguration());

            modelBuilder.Entity<Domain.Referensi.Referensi>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.Referensi);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("name");

                entity.Property(e => e.KebaruanReferensiId)
                      .HasColumnName("id_kebaruan_referensi");

                entity.Property(e => e.RelevansiKualitasReferensiId)
                      .HasColumnName("id_relevansi_kualitas_referensi");
            });
        }
    }
}
