using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Infrastructure.KesesuaianWaktuRabLuaranFasilitas;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Infrastructure.Database
{
    public sealed class KesesuaianWaktuRabLuaranFasilitasDbContext(DbContextOptions<KesesuaianWaktuRabLuaranFasilitasDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas> KesesuaianWaktuRabLuaranFasilitas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas>().ToTable(Schemas.KesesuaianWaktuRabLuaranFasilitas);
            modelBuilder.ApplyConfiguration(new KesesuaianWaktuRabLuaranFasilitasConfiguration());

            modelBuilder.Entity<Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KesesuaianWaktuRabLuaranFasilitas);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("name");

                entity.Property(e => e.Skor)
                      .HasColumnName("skor");

            });
        }
    }
}
