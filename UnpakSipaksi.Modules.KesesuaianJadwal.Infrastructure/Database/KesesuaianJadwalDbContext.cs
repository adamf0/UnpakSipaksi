using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KesesuaianJadwal.Infrastructure.KesesuaianJadwal;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Infrastructure.Database
{
    public sealed class KesesuaianJadwalDbContext(DbContextOptions<KesesuaianJadwalDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KesesuaianJadwal.KesesuaianJadwal> KesesuaianJadwal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KesesuaianJadwal.KesesuaianJadwal>().ToTable(Schemas.KesesuaianJadwal);
            modelBuilder.ApplyConfiguration(new KesesuaianJadwalConfiguration());

            modelBuilder.Entity<Domain.KesesuaianJadwal.KesesuaianJadwal>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KesesuaianJadwal);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");

                entity.Property(e => e.Nilai)
                      .HasColumnName("nilai");

            });
        }
    }
}
