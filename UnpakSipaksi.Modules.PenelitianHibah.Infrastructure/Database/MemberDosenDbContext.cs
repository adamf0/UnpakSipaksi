using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.MemberDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database
{
    public sealed class MemberDosenDbContext(DbContextOptions<MemberDosenDbContext> options) : DbContext(options), IUnitOfWorkMember
    {
        internal DbSet<Domain.MemberDosen.MemberDosen> MemberDosen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.MemberDosen.MemberDosen>().ToTable(Schemas.DosenMember);
            modelBuilder.ApplyConfiguration(new MemberDosenConfiguration());

            modelBuilder.Entity<Domain.MemberDosen.MemberDosen>(entity =>
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

                entity.Property(e => e.PenelitianHibahId)
                      .HasColumnName("id_pdp");

                entity.Property(e => e.Status)
                      .HasColumnName("status");

                // Uncomment if needed
                // entity.Property(e => e.CatatanTolak)
                //       .HasColumnName("catatan_tolak");
            });
        }
    }
}
