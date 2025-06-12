using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.DokumenLainnya;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database
{
    public sealed class DokumenLainnyaDbContext(DbContextOptions<DokumenLainnyaDbContext> options) : DbContext(options), IUnitOfWorkDokumenLainnya
    {
        internal DbSet<Domain.DokumenLainnya.DokumenLainnya> DokumenLainnya { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.DokumenLainnya.DokumenLainnya>().ToTable(Schemas.DokumenLainnya);
            modelBuilder.ApplyConfiguration(new DokumenLainnyaConfiguration());

            modelBuilder.Entity<Domain.DokumenLainnya.DokumenLainnya>(entity =>
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

                entity.Property(e => e.PenelitianPkmId)
                      .HasColumnName("id_pdp");

                entity.Property(e => e.File)
                      .HasColumnName("file_kontrak");

                entity.Property(e => e.Keterangan)
                      .HasColumnName("keterangan");

                // Uncomment if needed
                // entity.Property(e => e.CatatanTolak)
                //       .HasColumnName("catatan_tolak");
            });
        }
    }
}
