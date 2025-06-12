using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Org.BouncyCastle.Asn1.Ocsp;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.DokumenMitra;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database
{
    public sealed class DokumenMitraDbContext(DbContextOptions<DokumenMitraDbContext> options) : DbContext(options), IUnitOfWorkDokumenMitra
    {
        internal DbSet<Domain.DokumenMitra.DokumenMitra> DokumenMitra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.DokumenMitra.DokumenMitra>().ToTable(Schemas.DokumenMitra);
            modelBuilder.ApplyConfiguration(new DokumenMitraConfiguration());

            modelBuilder.Entity<Domain.DokumenMitra.DokumenMitra>(entity =>
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

                entity.Property(e => e.Mitra)
                      .HasColumnName("mitra");

                entity.Property(e => e.Provinsi)
                      .HasColumnName("provinsi");

                entity.Property(e => e.Kota)
                      .HasColumnName("kota");

                entity.Property(e => e.KelompokMitraId)
                      .HasColumnName("id_kelompok_mitra");

                entity.Property(e => e.PemimpinMitra)
                      .HasColumnName("pemimpinMitra");

                entity.Property(e => e.KontakMitra)
                      .HasColumnName("kontakMitra");

                entity.Property(e => e.File)
                      .HasColumnName("suratPernyataan");

                // Uncomment if needed
                // entity.Property(e => e.CatatanTolak)
                //       .HasColumnName("catatan_tolak");
            });
        }
    }
}
