using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.DokumenKontrak;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database
{
    public sealed class DokumenKontrakDbContext(DbContextOptions<DokumenKontrakDbContext> options) : DbContext(options), IUnitOfWorkDokumenKontrak
    {
        internal DbSet<Domain.DokumenKontrak.DokumenKontrak> DokumenKontrak { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.DokumenKontrak.DokumenKontrak>().ToTable(Schemas.DokumenKontrak);
            modelBuilder.ApplyConfiguration(new DokumenKontrakConfiguration());

            modelBuilder.Entity<Domain.DokumenKontrak.DokumenKontrak>(entity =>
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

                entity.Property(e => e.PenelitianHibahId)
                      .HasColumnName("id_pdp");

                entity.Property(e => e.File)
                      .HasColumnName("file_kontrak");

                // Uncomment if needed
                // entity.Property(e => e.CatatanTolak)
                //       .HasColumnName("catatan_tolak");
            });
        }
    }
}
