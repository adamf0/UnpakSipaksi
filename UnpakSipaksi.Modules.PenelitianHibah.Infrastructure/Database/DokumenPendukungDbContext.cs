using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.DokumenPendukung;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database
{
    public sealed class DokumenPendukungDbContext(DbContextOptions<DokumenPendukungDbContext> options) : DbContext(options), IUnitOfWorkDokumenPendukung
    {
        internal DbSet<Domain.DokumenPendukung.DokumenPendukung> DokumenPendukung { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.DokumenPendukung.DokumenPendukung>().ToTable(Schemas.DokumenPendukung);
            modelBuilder.ApplyConfiguration(new DokumenPendukungConfiguration());

            modelBuilder.Entity<Domain.DokumenPendukung.DokumenPendukung>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"),
                    v => Guid.ParseExact(v, "D")
                );

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)")
                      .HasConversion(guidConverter);

                entity.Property(e => e.PenelitianHibahId)
                      .HasColumnName("id_pdp");

                entity.Property(e => e.File)
                      .HasColumnName("file_mitra");

                entity.Property(e => e.Link)
                      .HasColumnName("link");

                entity.Property(e => e.Kategori)
                      .HasColumnName("kategori");

                // Uncomment if needed
                // entity.Property(e => e.CatatanTolak)
                //       .HasColumnName("catatan_tolak");
            });
        }
    }
}
