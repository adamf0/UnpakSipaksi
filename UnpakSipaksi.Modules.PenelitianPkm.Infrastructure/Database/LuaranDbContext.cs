using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Luaran;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database
{
    public sealed class LuaranDbContext(DbContextOptions<LuaranDbContext> options) : DbContext(options), IUnitOfWorkLuaran
    {
        internal DbSet<Domain.Luaran.Luaran> Luaran { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Luaran.Luaran>().ToTable(Schemas.Luaran);
            modelBuilder.ApplyConfiguration(new LuaranConfiguration());

            modelBuilder.Entity<Domain.Luaran.Luaran>(entity =>
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

                entity.Property(e => e.JenisLuaranId)
                      .HasColumnName("id_jenis_luaran");

                entity.Property(e => e.IndikatorCapaianId)
                      .HasColumnName("id_indikator_capaian");

                entity.Property(e => e.Keterangan)
                      .HasColumnName("keterangan");

                entity.Property(e => e.Link)
                      .HasColumnName("link");

                entity.Property(e => e.Jenis)
                      .HasColumnName("jenis");

                // Uncomment if needed
                // entity.Property(e => e.CatatanTolak)
                //       .HasColumnName("catatan_tolak");
            });
        }
    }
}
