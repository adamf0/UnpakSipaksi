using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database
{
    public sealed class PenelitianPkmDbContext(DbContextOptions<PenelitianPkmDbContext> options) : DbContext(options), IUnitOfWorkHibah
    {
        internal DbSet<Domain.PenelitianPkm.PenelitianPkm> PenelitianPkm { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.PenelitianPkm.PenelitianPkm>().ToTable(Schemas.PenelitianPkm);
            modelBuilder.ApplyConfiguration(new PenelitianPkmConfiguration());

            modelBuilder.Entity<Domain.PenelitianPkm.PenelitianPkm>(entity =>
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

                entity.Property(e => e.NIDN)
                      .HasColumnName("NIDN");

                entity.Property(e => e.Judul)
                      .HasColumnName("judul");

                entity.Property(e => e.KategoriProgramPengabdianId)
                      .HasColumnName("id_kategori_program_pengabdian");

                entity.Property(e => e.FokusPenelitianId)
                      .HasColumnName("id_fokus_pengabdian");

                entity.Property(e => e.RirnId)
                      .HasColumnName("id_rirn");

                entity.Property(e => e.RumpunIlmu1Id)
                      .HasColumnName("id_rumpun_ilmu1");

                entity.Property(e => e.RumpunIlmu2Id)
                      .HasColumnName("id_rumpun_ilmu2");

                entity.Property(e => e.RumpunIlmu3Id)
                      .HasColumnName("id_rumpun_ilmu3");

                entity.Property(e => e.TahunPengajuan)
                      .HasColumnName("tahun_pengajuan");

                entity.Property(e => e.Status)
                      .HasColumnName("status");

                entity.Property(e => e.Type)
                      .HasColumnName("type");

                entity.Property(e => e.Catatan)
                      .HasColumnName("catatan");

                // Uncomment if needed
                // entity.Property(e => e.CatatanTolak)
                //       .HasColumnName("catatan_tolak");
            });
        }
    }
}
