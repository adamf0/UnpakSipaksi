using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Metode.Infrastructure.Metode;
using UnpakSipaksi.Modules.Metode.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.Metode.Infrastructure.Database
{
    public sealed class MetodeDbContext(DbContextOptions<MetodeDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.Metode.Metode> Metode { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Metode.Metode>().ToTable(Schemas.Metode);
            modelBuilder.ApplyConfiguration(new MetodeConfiguration());

            modelBuilder.Entity<Domain.Metode.Metode>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.Metode);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

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

            });
        }
    }
}
