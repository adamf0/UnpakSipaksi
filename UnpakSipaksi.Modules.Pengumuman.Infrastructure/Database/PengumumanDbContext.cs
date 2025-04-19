using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Pengumuman.Infrastructure.Pengumuman;
using UnpakSipaksi.Modules.Pengumuman.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.Pengumuman.Infrastructure.Database
{
    public sealed class PengumumanDbContext(DbContextOptions<PengumumanDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.Pengumuman.Pengumuman> Pengumuman { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Pengumuman.Pengumuman>().ToTable(Schemas.Pengumuman);
            modelBuilder.ApplyConfiguration(new PengumumanConfiguration());

            modelBuilder.Entity<Domain.Pengumuman.Pengumuman>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.Pengumuman);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Pesan)
                      .HasColumnName("isi");

                entity.Property(e => e.File)
                      .HasColumnName("file");

                entity.Property(e => e.Url)
                      .HasColumnName("url");

                entity.Property(e => e.Type)
                      .HasColumnName("type");

                entity.Property(e => e.Target)
                      .HasColumnName("type_target");

                entity.Property(e => e.Nidn)
                      .HasColumnName("nidn");

                entity.Property(e => e.KodeFaKultas)
                      .HasColumnName("kode_fakultas");

                entity.Property(e => e.TypeExpired)
                      .HasColumnName("type_expire");

                entity.Property(e => e.TanggalAwal)
                      .HasColumnName("tanggal_awal");

                entity.Property(e => e.TanggalAkhir)
                      .HasColumnName("tanggal_akhir");

            });
        }
    }
}
