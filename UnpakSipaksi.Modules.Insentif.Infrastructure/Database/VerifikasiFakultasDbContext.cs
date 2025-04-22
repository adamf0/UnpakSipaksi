
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Insentif.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Insentif.Infrastructure.VerifikasiFakultas;

namespace UnpakSipaksi.Modules.Insentif.Infrastructure.Database
{
    public sealed class VerifikasiFakultasDbContext(DbContextOptions<VerifikasiFakultasDbContext> options) : DbContext(options), IUnitOfWorkVerifikasiFakultas
    {
        internal DbSet<Domain.VerifikasiFakultas.VerifikasiFakultas> VerifikasiFakultas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.VerifikasiFakultas.VerifikasiFakultas>().ToTable(Schemas.VerifikasiFakultas);
            modelBuilder.ApplyConfiguration(new VerifikasiFakultasConfiguration());

            modelBuilder.Entity<Domain.VerifikasiFakultas.VerifikasiFakultas>(entity =>
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
                      .HasColumnName("bukti_publikasi");

                entity.Property(e => e.StatusJurnal)
                      .HasColumnName("status_jurnal");

                entity.Property(e => e.PeranPenulis)
                      .HasColumnName("peran_penulis");

                entity.Property(e => e.JumlahPenulis)
                      .HasColumnName("cek_jumlah_penulis");

                entity.Property(e => e.JenisJurnal)
                      .HasColumnName("jurnal_internal");

                entity.Property(e => e.LibatkanMahasiswa)
                      .HasColumnName("mahasiswa_c");

                entity.Property(e => e.SBU)
                      .HasColumnName("sbu");

                entity.Property(e => e.Porsi)
                      .HasColumnName("porsi_sbu");

                entity.Property(e => e.Insentif)
                      .HasColumnName("cek_insentif");

                entity.Property(e => e.StatusPengajuan)
                      .HasColumnName("status_pengajuan");

                entity.Property(e => e.Catatan)
                      .HasColumnName("keterangan");
            });
        }
    }
}
