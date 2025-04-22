using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Insentif.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Insentif.Infrastructure.VerifikasiLppm;

namespace UnpakSipaksi.Modules.Insentif.Infrastructure.Database
{
    public sealed class VerifikasiLppmDbContext(DbContextOptions<VerifikasiLppmDbContext> options) : DbContext(options), IUnitOfWorkVerifikasiLppm
    {
        internal DbSet<Domain.VerifikasiLppm.VerifikasiLppm> VerifikasiLppm { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.VerifikasiLppm.VerifikasiLppm>().ToTable(Schemas.VerifikasiLppm);
            modelBuilder.ApplyConfiguration(new VerifikasiLppmConfiguration());

            modelBuilder.Entity<Domain.VerifikasiLppm.VerifikasiLppm>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"),
                    v => Guid.ParseExact(v, "D")
                );

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)")
                      .HasConversion(guidConverter);
                //.HasConversion(guidConverter);

                entity.Property(e => e.BuktiPublikasi)
                      .HasColumnName("bukti_publikasi_v");

                entity.Property(e => e.StatusJurnal)
                      .HasColumnName("status_jurnal_v");

                entity.Property(e => e.PeranPenulis)
                      .HasColumnName("peran_penulis_v");

                entity.Property(e => e.JumlahPenulis)
                      .HasColumnName("cek_jumlah_penulis_v");

                entity.Property(e => e.JenisJurnal)
                      .HasColumnName("jurnal_internal_v");

                entity.Property(e => e.LibatkanMahasiswa)
                      .HasColumnName("mahasiswa_v");

                entity.Property(e => e.SBU)
                      .HasColumnName("sbu_v");

                entity.Property(e => e.Porsi)
                      .HasColumnName("porsi_sbu_v");

                entity.Property(e => e.Insentif)
                      .HasColumnName("cek_insentif_v");

                entity.Property(e => e.StatusPengajuan)
                      .HasColumnName("status_pengajuan_v");

                entity.Property(e => e.Catatan)
                      .HasColumnName("keterangan_v");
            });
        }
    }
}
