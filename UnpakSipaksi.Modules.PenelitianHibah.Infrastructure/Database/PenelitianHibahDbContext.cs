using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.PenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database
{
    public sealed class PenelitianHibahDbContext(DbContextOptions<PenelitianHibahDbContext> options) : DbContext(options), IUnitOfWorkHibah
    {
        internal DbSet<Domain.PenelitianHibah.PenelitianHibah> PenelitianHibah { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.PenelitianHibah.PenelitianHibah>().ToTable(Schemas.PenelitianHibah);
            modelBuilder.ApplyConfiguration(new PenelitianHibahConfiguration());

            modelBuilder.Entity<Domain.PenelitianHibah.PenelitianHibah>(entity =>
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

                entity.Property(e => e.SkemaId)
                      .HasColumnName("id_skema");

                entity.Property(e => e.TKT)
                      .HasColumnName("tkt");

                entity.Property(e => e.KategoriTKT)
                      .HasColumnName("kategori_tkt");

                entity.Property(e => e.BidangFokusPenelitianId)
                      .HasColumnName("id_bidang_fokus_penelitian");

                entity.Property(e => e.BidangFokusPenelitianTemaId)
                      .HasColumnName("id_bidang_fokus_penelitian_tema");

                entity.Property(e => e.BidangFokusPenelitianTemaTopikId)
                      .HasColumnName("id_bidang_fokus_penelitian_tema_topik");

                entity.Property(e => e.RumpunIlmu1Id)
                      .HasColumnName("id_rumpun_ilmu1");

                entity.Property(e => e.RumpunIlmu2Id)
                      .HasColumnName("id_rumpun_ilmu2");

                entity.Property(e => e.RumpunIlmu3Id)
                      .HasColumnName("id_rumpun_ilmu3");

                entity.Property(e => e.PrioritasRiset)
                      .HasColumnName("prioritas_riset");

                entity.Property(e => e.TahunPengajuan)
                      .HasColumnName("tahun_pengajuan");

                entity.Property(e => e.LamaKegiatan)
                      .HasColumnName("lama_kegiatan");

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
