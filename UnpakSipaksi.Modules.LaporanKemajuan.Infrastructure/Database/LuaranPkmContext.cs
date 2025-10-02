using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.LaporanKemajuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Luaran;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Database
{
    public sealed class LuaranPkmContext(DbContextOptions<LuaranPkmContext> options) : DbContext(options), IUnitOfWorkLuaranPkm
    {
        internal DbSet<Domain.Luaran.Luaran> LuaranPkm { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Luaran.Luaran>().ToTable(Schemas.LuaranPkm);
            modelBuilder.ApplyConfiguration(new LuaranConfiguration());

            modelBuilder.Entity<Domain.Luaran.Luaran>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.LuaranPkm);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.JenisLuaranId)
                      .HasColumnName("id_jenis_luaran");

                entity.Property(e => e.IndikatorId)
                      .HasColumnName("id_indikator_capaian");

                entity.Property(e => e.PenenitianPkmId)
                      .HasColumnName("id_pkm");
            });
        }
    }
}
