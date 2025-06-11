using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.RAB;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database
{
    public sealed class RABDbContext(DbContextOptions<RABDbContext> options) : DbContext(options), IUnitOfWorkRAB
    {
        internal DbSet<Domain.RAB.RAB> RAB { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.RAB.RAB>().ToTable(Schemas.RAB);
            modelBuilder.ApplyConfiguration(new RABConfiguration());

            modelBuilder.Entity<Domain.RAB.RAB>(entity =>
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
                       .HasColumnName("id_pkm");

                entity.Property(e => e.KelompokRabId) //null
                      .HasColumnName("kelompok_rab");

                entity.Property(e => e.KomponenId) //null
                      .HasColumnName("komponen");

                entity.Property(e => e.Item) //null
                      .HasColumnName("item");

                entity.Property(e => e.SatuanId) //null
                      .HasColumnName("satuan");

                entity.Property(e => e.HargaSatuan) //null
                      .HasColumnName("harga_satuan");

                entity.Property(e => e.Total) //null
                      .HasColumnName("total");

                // Uncomment if needed
                // entity.Property(e => e.CatatanTolak)
                //       .HasColumnName("catatan_tolak");
            });
        }
    }
}
