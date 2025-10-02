using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.LaporanKemajuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Luaran;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Database
{
    public sealed class LuaranHibahContext(DbContextOptions<LuaranHibahContext> options) : DbContext(options), IUnitOfWorkLuaranHibah
    {
        internal DbSet<Domain.Luaran.Luaran> LuaranHibah { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Luaran.Luaran>().ToTable(Schemas.LuaranHibah);
            modelBuilder.ApplyConfiguration(new LuaranConfiguration());

            modelBuilder.Entity<Domain.Luaran.Luaran>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.LuaranHibah);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.KategoriId)
                      .HasColumnName("id_pdp_kategori");

                entity.Property(e => e.LuaranId)
                      .HasColumnName("id_pdp_kategori_luaran");

                entity.Property(e => e.PenenitianHibahId)
                      .HasColumnName("id_pdp");
            });
        }
    }
}
