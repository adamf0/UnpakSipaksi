using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.MemberNonDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database
{
    public sealed class MemberNonDosenDbContext(DbContextOptions<MemberNonDosenDbContext> options) : DbContext(options), IUnitOfWorkNonMember
    {
        internal DbSet<Domain.MemberNonDosen.MemberNonDosen> MemberNonDosen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.MemberNonDosen.MemberNonDosen>().ToTable(Schemas.ExternalMember);
            modelBuilder.ApplyConfiguration(new MemberNonDosenConfiguration());

            modelBuilder.Entity<Domain.MemberNonDosen.MemberNonDosen>(entity =>
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

                entity.Property(e => e.NomorIdentitas)
                      .HasColumnName("nomorIdentitas");

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");

                entity.Property(e => e.Afiliasi)
                      .HasColumnName("afiliasi");

                // Uncomment if needed
                // entity.Property(e => e.CatatanTolak)
                //       .HasColumnName("catatan_tolak");
            });
        }
    }
}
