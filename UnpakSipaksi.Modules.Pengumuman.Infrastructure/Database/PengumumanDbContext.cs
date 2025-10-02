using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Pengumuman.Infrastructure.Pengumuman;
using UnpakSipaksi.Modules.Pengumuman.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.Pengumuman.Infrastructure.Database
{
    public sealed class PengumumanDbContext(DbContextOptions<PengumumanDbContext> options)
        : DbContext(options), IUnitOfWork
    {
        public DbSet<Domain.Pengumuman.Pengumuman> Pengumuman { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Pengumuman.Pengumuman>().ToTable(Schemas.Pengumuman);
            modelBuilder.ApplyConfiguration(new PengumumanConfiguration());

            modelBuilder.Entity<Domain.Pengumuman.Pengumuman>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"),
                    v => Guid.ParseExact(v, "D")
                );

                entity.ToTable(Schemas.Pengumuman);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)")
                      .HasConversion(guidConverter);

                entity.Property(e => e.Pesan)
                      .HasColumnName("isi")
                      .HasColumnType("TEXT"); // isi panjang

                entity.Property(e => e.File)
                      .HasColumnName("file")
                      .HasColumnType("VARCHAR(500)");

                entity.Property(e => e.Url)
                      .HasColumnName("url")
                      .HasColumnType("VARCHAR(1000)");

                // ENUM diganti jadi VARCHAR(20)
                entity.Property(e => e.Type)
                      .HasColumnName("type")
                      .HasColumnType("VARCHAR(20)")
                      .HasDefaultValue("pengumuman");

                entity.Property(e => e.Target)
                      .HasColumnName("type_target")
                      .HasColumnType("VARCHAR(20)")
                      .HasDefaultValue("all");

                entity.Property(e => e.Nidn)
                      .HasColumnName("nidn")
                      .HasColumnType("VARCHAR(50)");

                entity.Property(e => e.KodeFaKultas)
                      .HasColumnName("kode_fakultas")
                      .HasColumnType("CHAR(9)");

                entity.Property(e => e.TypeExpired)
                      .HasColumnName("type_expire")
                      .HasColumnType("VARCHAR(20)")
                      .HasDefaultValue("no expire");

                entity.Property(e => e.TanggalAwal)
                      .HasColumnName("tanggal_awal")
                      .HasColumnType("DATETIME");

                entity.Property(e => e.TanggalAkhir)
                      .HasColumnName("tanggal_akhir")
                      .HasColumnType("DATETIME");
            });
        }

    }
}
