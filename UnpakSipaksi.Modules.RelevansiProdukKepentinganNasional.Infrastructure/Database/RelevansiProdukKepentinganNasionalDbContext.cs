using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Infrastructure.RelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Infrastructure.Database
{
    public sealed class RelevansiProdukKepentinganNasionalDbContext(DbContextOptions<RelevansiProdukKepentinganNasionalDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional> RelevansiProdukKepentinganNasional { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional>().ToTable(Schemas.RelevansiProdukKepentinganNasional);
            modelBuilder.ApplyConfiguration(new RelevansiProdukKepentinganNasionalConfiguration());

            modelBuilder.Entity<Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.RelevansiProdukKepentinganNasional);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("name");

                entity.Property(e => e.BobotPDP)
                      .HasColumnName("bobot_pdp");

                entity.Property(e => e.BobotTerapan)
                      .HasColumnName("bobot_terapan");

                entity.Property(e => e.BobotKerjasama)
                      .HasColumnName("bobot_kerjasama");

                entity.Property(e => e.BobotPenelitianDasar)
                      .HasColumnName("bobot_penelitian_dasar");
            });
        }
    }
}
