using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.UrgensiPenelitian.Infrastructure.UrgensiPenelitian;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Infrastructure.Database
{
    public sealed class UrgensiPenelitianDbContext(DbContextOptions<UrgensiPenelitianDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.UrgensiPenelitian.UrgensiPenelitian> UrgensiPenelitian { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.UrgensiPenelitian.UrgensiPenelitian>().ToTable(Schemas.UrgensiPenelitian);
            modelBuilder.ApplyConfiguration(new UrgensiPenelitianConfiguration());

            modelBuilder.Entity<Domain.UrgensiPenelitian.UrgensiPenelitian>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.UrgensiPenelitian);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

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

            });
        }
    }
}
