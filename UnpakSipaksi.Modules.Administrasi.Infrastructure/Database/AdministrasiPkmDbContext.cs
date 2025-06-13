using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Administrasi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Administrasi.Infrastructure.AdministrasiPkm;

namespace UnpakSipaksi.Modules.Administrasi.Infrastructure.Database
{
    public sealed class AdministrasiPkmDbContext(DbContextOptions<AdministrasiPkmDbContext> options) : DbContext(options), IUnitOfWorkAdministrasiPkm
    {
        internal DbSet<Domain.AdministrasiPkm.AdministrasiPkm> AdministrasiPkm { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.AdministrasiPkm.AdministrasiPkm>().ToTable(Schemas.AdministrasiPkm);
            modelBuilder.ApplyConfiguration(new AdministrasiPkmConfiguration());

            modelBuilder.Entity<Domain.AdministrasiPkm.AdministrasiPkm>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.AdministrasiPkm);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Level).HasColumnName("Level");

                entity.Property(e => e.Judul).HasColumnName("Judul");

                entity.Property(e => e.HalamanSampul).HasColumnName("halamanSampul");

                entity.Property(e => e.HalamanPengesahan).HasColumnName("halamanPengesahan");

                entity.Property(e => e.IdentitasPengusul).HasColumnName("identitasPengusul");

                entity.Property(e => e.MitraPkm).HasColumnName("mitraPkm");

                entity.Property(e => e.LuaranTargetCapaian).HasColumnName("luaranTargetCapaian");

                entity.Property(e => e.Rab).HasColumnName("rab");

                entity.Property(e => e.RingkasanKataKunci).HasColumnName("ringkasanKataKunci");

                entity.Property(e => e.AnalisisSituasi).HasColumnName("analisisSituasi");

                entity.Property(e => e.PermasalahanMitra).HasColumnName("permasalahanMitra");

                entity.Property(e => e.SolusiPermasalahan).HasColumnName("solusiPermasalahan");

                entity.Property(e => e.MetodePelaksanaan).HasColumnName("metodePelaksanaan");

                entity.Property(e => e.Jadwal).HasColumnName("jadwal");

                entity.Property(e => e.DaftarPustaka).HasColumnName("daftarPustaka");

                entity.Property(e => e.BiodataKetuaAnggota).HasColumnName("biodataKetuaAnggota");

                entity.Property(e => e.GambaranIptek).HasColumnName("gambaranIptek");

                entity.Property(e => e.PetaLokasiMitra).HasColumnName("petaLokasiMitra");

                entity.Property(e => e.SuratPernyataanKetuaPelaksana).HasColumnName("suratPernyataanKetuaPelaksana");

                entity.Property(e => e.SuratKetersediaanMitra).HasColumnName("suratKetersediaanMitra");

                entity.Property(e => e.MelibatkanMahasiswa).HasColumnName("melibatkanMahasiswa");

                entity.Property(e => e.MendukungIKU).HasColumnName("mendukungIKU");

                entity.Property(e => e.Keputusan).HasColumnName("keputusan");

                entity.Property(e => e.Komentar).HasColumnName("komentar");
            });
        }
    }
}
