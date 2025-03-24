using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Infrastructure.PublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Infrastructure.Database
{
    public sealed class PublikasiDisitasiProposalDbContext(DbContextOptions<PublikasiDisitasiProposalDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal> PublikasiDisitasiProposal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal>().ToTable(Schemas.PublikasiDisitasiProposal);
            modelBuilder.ApplyConfiguration(new PublikasiDisitasiProposalConfiguration());

            modelBuilder.Entity<Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.PublikasiDisitasiProposal);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("name");

                entity.Property(e => e.BobotPDP)
                      .HasColumnName("bobot_pdp");

                entity.Property(e => e.BobotTerapan)
                      .HasColumnName("bobot_terapan");

                entity.Property(e => e.BobotKerjasama)
                      .HasColumnName("bobot_kerjasama");

                entity.Property(e => e.BobotPenelitianDasar)
                      .HasColumnName("bobot_penelitian_dasar");
            });
        }
    }
}
