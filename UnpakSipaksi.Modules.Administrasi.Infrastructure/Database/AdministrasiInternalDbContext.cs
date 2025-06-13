using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Administrasi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Administrasi.Infrastructure.AdministrasiInternal;

namespace UnpakSipaksi.Modules.Administrasi.Infrastructure.Database
{
    public sealed class AdministrasiInternalDbContext(DbContextOptions<AdministrasiInternalDbContext> options) : DbContext(options), IUnitOfWorkAdministrasiInternal
    {
        internal DbSet<Domain.AdministrasiInternal.AdministrasiInternal> AdministrasiInternal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.AdministrasiInternal.AdministrasiInternal>().ToTable(Schemas.AdministrasiInternal);
            modelBuilder.ApplyConfiguration(new AdministrasiInternalConfiguration());

            modelBuilder.Entity<Domain.AdministrasiInternal.AdministrasiInternal>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.AdministrasiInternal);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Level)
                      .HasColumnName("level");

                entity.Property(e => e.Judul)
                      .HasColumnName("judul");

                entity.Property(e => e.HalamanSampul)
                      .HasColumnName("halamanSampul");

                entity.Property(e => e.HalamanPengesahan)
                      .HasColumnName("halamanPengesahan");

                entity.Property(e => e.IdentitasPengusul)
                      .HasColumnName("identitasPengusul");

                entity.Property(e => e.IdentitasMahasiswa)
                      .HasColumnName("identitasMahasiswa");

                entity.Property(e => e.MitraKerjasama)
                      .HasColumnName("mitraKerjasama");

                entity.Property(e => e.LuaranTargetCapaian)
                      .HasColumnName("luaranTargetCapaian");

                entity.Property(e => e.Rab)
                      .HasColumnName("rab");

                entity.Property(e => e.LatarBelakangRumusanMasalah)
                      .HasColumnName("latarBelakangRumusanMasalah");

                entity.Property(e => e.PendekatanPemecahanMasalah)
                      .HasColumnName("pendekatanPemecahanMasalah");

                entity.Property(e => e.Sota)
                      .HasColumnName("sota");

                entity.Property(e => e.PenjelasanCapaianRisetKepakaran)
                      .HasColumnName("penjelasanCapaianRisetKepakaran");

                entity.Property(e => e.PetaJalan)
                      .HasColumnName("petaJalan");

                entity.Property(e => e.TahapanPenelitian)
                      .HasColumnName("tahapanPenelitian");

                entity.Property(e => e.IndikatorCapaianPadaRab)
                      .HasColumnName("indikatorCapaianPadaRab");

                entity.Property(e => e.Jadwal)
                      .HasColumnName("jadwal");

                entity.Property(e => e.DaftarPustaka)
                      .HasColumnName("daftarPustaka");

                entity.Property(e => e.BiodataKetuaAnggota)
                      .HasColumnName("biodataKetuaAnggota");

                entity.Property(e => e.PaktaIntegritas)
                      .HasColumnName("paktaIntegritas");

                entity.Property(e => e.SuratKetersediaanMitra)
                      .HasColumnName("suratKetersediaanMitra");

                entity.Property(e => e.Keputusan)
                      .HasColumnName("keputusan");

                entity.Property(e => e.Komentar)
                      .HasColumnName("komentar");
            });
        }
    }
}
