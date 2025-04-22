using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Insentif.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Insentif.Infrastructure.VerifikasiLppm;

namespace UnpakSipaksi.Modules.Insentif.Infrastructure.Database
{
    public sealed class InsentifDbContext(DbContextOptions<InsentifDbContext> options) : DbContext(options), IUnitOfWorkInsentif
    {
        internal DbSet<Domain.Insentif.Insentif> Insentif { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Insentif.Insentif>().ToTable(Schemas.Insentif);
            modelBuilder.ApplyConfiguration(new VerifikasiLppmConfiguration());

            modelBuilder.Entity<Domain.Insentif.Insentif>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"),
                    v => Guid.ParseExact(v, "D")
                );

                entity.ToTable(Schemas.Insentif);
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)")
                      .HasConversion(guidConverter);

                entity.Property(e => e.Nidn)
                      .HasColumnName("NIDN");

                entity.Property(e => e.JudulArtikel)
                      .HasColumnName("judul_artikel");

                entity.Property(e => e.NamaJurnalPenerbit)
                      .HasColumnName("nama_jurnal_penerbit");

                entity.Property(e => e.JumlahPenulis)
                      .HasColumnName("jumlah_penulis");

                entity.Property(e => e.JenisPublikasi)
                      .HasColumnName("jenis_publikasi");

                entity.Property(e => e.IndexJenisPublikasi)
                      .HasColumnName("id_jenis_publikasi");

                entity.Property(e => e.Link)
                      .HasColumnName("link");

                entity.Property(e => e.JenisJurnal)
                      .HasColumnName("jenis_jurnal");

                entity.Property(e => e.Peran)
                      .HasColumnName("peran");

                entity.Property(e => e.TanggalTerbit)
                      .HasColumnName("tanggal_terbit");

                entity.Property(e => e.Volume)
                      .HasColumnName("volume");

                entity.Property(e => e.Edisi)
                      .HasColumnName("edisi");

                entity.Property(e => e.Halaman)
                      .HasColumnName("halaman");

                entity.Property(e => e.DOI)
                      .HasColumnName("DOI");

                entity.Property(e => e.NamaKegiatanSeminar)
                      .HasColumnName("nama_kegiatan_seminar");

                entity.Property(e => e.LibatkanMahasiswa)
                      .HasColumnName("mahasiswa");
            });

            modelBuilder.ApplyConfiguration(new VerifikasiLppmConfiguration());
        }

    }
}
