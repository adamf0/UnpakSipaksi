using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Infrastructure.PotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Infrastructure.Database
{
    public sealed class PotensiKetercapaianLuaranDijanjikanDbContext(DbContextOptions<PotensiKetercapaianLuaranDijanjikanDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan> PotensiKetercapaianLuaranDijanjikan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan>().ToTable(Schemas.PotensiKetercapaianLuaranDijanjikan);
            modelBuilder.ApplyConfiguration(new PotensiKetercapaianLuaranDijanjikanConfiguration());

            modelBuilder.Entity<Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.PotensiKetercapaianLuaranDijanjikan);

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
