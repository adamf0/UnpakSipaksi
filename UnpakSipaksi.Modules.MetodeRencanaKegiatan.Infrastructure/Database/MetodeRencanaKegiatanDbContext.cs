using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Infrastructure.MetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Infrastructure.Database
{
    public sealed class MetodeRencanaKegiatanDbContext(DbContextOptions<MetodeRencanaKegiatanDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan> MetodeRencanaKegiatan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan>().ToTable(Schemas.MetodeRencanaKegiatan);
            modelBuilder.ApplyConfiguration(new MetodeRencanaKegiatanConfiguration());

            modelBuilder.Entity<Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.MetodeRencanaKegiatan);

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
