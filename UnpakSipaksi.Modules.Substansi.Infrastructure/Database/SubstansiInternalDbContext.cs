using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Substansi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Substansi.Infrastructure.SubstansiInternal;

namespace UnpakSipaksi.Modules.Substansi.Infrastructure.Database
{
    public sealed class SubstansiInternalDbContext(DbContextOptions<SubstansiInternalDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.SubstansiInternal.SubstansiInternal> Substansi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.SubstansiInternal.SubstansiInternal>().ToTable(Schemas.Substansi);
            modelBuilder.ApplyConfiguration(new SubstansiInternalConfiguration());

            modelBuilder.Entity<Domain.SubstansiInternal.SubstansiInternal>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.Substansi);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.PenelitianHibahId)
                    .HasColumnName("id_pdp");

                entity.Property(e => e.PublikasiDisitasiProposalId)
                    .HasColumnName("id_publikasi_disitasi_proposal");

                entity.Property(e => e.RelevansiKepakaranTemaProposalId)
                    .HasColumnName("id_relevansi_kepakaran_tema_proposal");

                entity.Property(e => e.JumlahKolaboratorPublikasiBereputasiId)
                    .HasColumnName("id_jumlah_kolaborator_publikasi_bereputasi");

                entity.Property(e => e.RelevansiProdukKepentinganNasionalId)
                    .HasColumnName("id_relevansi_produk_kepentingan_nasional");

                entity.Property(e => e.KetajamanPerumusanMasalahId)
                    .HasColumnName("id_ketajaman_perumusan_masalah");

                entity.Property(e => e.InovasiPemecahanMasalahId)
                    .HasColumnName("id_inovasi_pemecahan_masalah");

                entity.Property(e => e.SotaKebaharuanId)
                    .HasColumnName("id_sota_kebaharuan");

                entity.Property(e => e.RoadmapPenelitianId)
                    .HasColumnName("id_roadmap_penelitian");

                entity.Property(e => e.AkurasiPenelitianId)
                    .HasColumnName("id_akurasi_penelitian");

                entity.Property(e => e.KejelasanPembagianTugasTimId)
                    .HasColumnName("id_kejelasan_pembagian_tugas_tim");

                entity.Property(e => e.KesesuaianWaktuRabLuaranFasilitasId)
                    .HasColumnName("id_kesesuaian_waktu_rab_luaran_fasilitas");

                entity.Property(e => e.PotensiKetercapaianLuaranDijanjikanId)
                    .HasColumnName("id_potensi_ketercapaian_luaran_dijanjikan");

                entity.Property(e => e.ModelFeasibilityStudyId)
                    .HasColumnName("id_model_feasibility_study");

                entity.Property(e => e.KesesuaianTktId)
                    .HasColumnName("id_kesesuaian_tkt");

                entity.Property(e => e.KredibilitasMitraDukunganId)
                    .HasColumnName("id_kredibilitas_mitra_dukungan");

                entity.Property(e => e.KebaruanReferensiId)
                    .HasColumnName("id_kebaruan_referensi");

                entity.Property(e => e.RelevansiKualitasReferensiId)
                    .HasColumnName("id_relevansi_kualitas_referensi");

                entity.Property(e => e.Komentar)
                    .HasColumnName("Komentar");

                entity.Property(e => e.ReviewerInternal)
                    .HasColumnName("reviewerInternal");

                entity.Property(e => e.ReviewerExternal)
                    .HasColumnType("bigint unsigned")
                    .HasColumnName("reviewerExternal");

                entity.Property(e => e.TanggalMulai)
                    .HasColumnName("tanggal_mulai");

                entity.Property(e => e.TanggalBerakhir)
                    .HasColumnName("tanggal_berakhir");
            });
        }
    }
}
