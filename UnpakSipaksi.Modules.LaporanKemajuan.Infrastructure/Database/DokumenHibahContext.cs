using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.LaporanKemajuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Dokumen;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Database
{
    public sealed class DokumenHibahContext(DbContextOptions<DokumenHibahContext> options) : DbContext(options), IUnitOfWorkDokumenHibah
    {
        internal DbSet<Domain.Dokumen.Dokumen> DokumenHibah { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Dokumen.Dokumen>().ToTable(Schemas.Dokumen);
            modelBuilder.ApplyConfiguration(new DokumenConfiguration());

            modelBuilder.Entity<Domain.Dokumen.Dokumen>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.Dokumen);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.File)
                      .HasColumnName("file");

                entity.Property(e => e.Type)
                      .HasColumnName("type");

                entity.Property(e => e.PenenitianHibahId)
                      .HasColumnName("id_pdp");
            });
        }
    }
}
