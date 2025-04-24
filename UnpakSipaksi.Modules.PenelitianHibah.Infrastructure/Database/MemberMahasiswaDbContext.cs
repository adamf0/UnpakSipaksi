using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.MemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database
{
    public sealed class MemberMahasiswaDbContext(DbContextOptions<MemberMahasiswaDbContext> options) : DbContext(options), IUnitOfWorkMemberMahasiswa
    {
        internal DbSet<Domain.MemberMahasiswa.MemberMahasiswa> MemberMahasiswa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.MemberMahasiswa.MemberMahasiswa>().ToTable(Schemas.MahasiswaMember);
            modelBuilder.ApplyConfiguration(new MemberMahasiswaConfiguration());

            modelBuilder.Entity<Domain.MemberMahasiswa.MemberMahasiswa>(entity =>
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

                entity.Property(e => e.NPM)
                      .HasColumnName("nim");

                entity.Property(e => e.PenelitianHibahId)
                      .HasColumnName("id_pdp");

                entity.Property(e => e.BuktiMbkm)
                      .HasColumnName("bukti_mbkm");

                // Uncomment if needed
                // entity.Property(e => e.CatatanTolak)
                //       .HasColumnName("catatan_tolak");
            });
        }
    }
}
