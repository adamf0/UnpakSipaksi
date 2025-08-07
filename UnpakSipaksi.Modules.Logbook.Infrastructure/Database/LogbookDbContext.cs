using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Logbook.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Logbook.Infrastructure.Logbook;

namespace UnpakSipaksi.Modules.Logbook.Infrastructure.Database
{
    public sealed class LogbookDbContext(DbContextOptions<LogbookDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.Logbook.Logbook> Logbook { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Logbook.Logbook>().ToTable(Schemas.Logbook);
            modelBuilder.ApplyConfiguration(new LogbookConfiguration());

            modelBuilder.Entity<Domain.Logbook.Logbook>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.Logbook);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Lampiran)
                      .HasColumnName("lampiran");

                entity.Property(e => e.Deskripsi)
                      .HasColumnName("deskripsi");

                entity.Property(e => e.Persentase)
                      .HasColumnName("persentase");

                entity.Property(e => e.TanggalKegiatan)
                      .HasColumnName("tanggal_kegiatan");

                entity.Property(e => e.PenelitianHibahId)
                      .HasColumnName("id_pdp");

                entity.Property(e => e.PenelitianPkmId)
                      .HasColumnName("id_pkm");

            });
        }
    }
}
