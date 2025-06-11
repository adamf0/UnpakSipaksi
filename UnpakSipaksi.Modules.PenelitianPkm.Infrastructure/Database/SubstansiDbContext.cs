using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Substansi;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database
{
    public sealed class SubstansiDbContext(DbContextOptions<SubstansiDbContext> options) : DbContext(options), IUnitOfWorkSubstansi
    {
        internal DbSet<Domain.Substansi.Substansi> Substansi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Substansi.Substansi>().ToTable(Schemas.SubstansiUsulan);
            modelBuilder.ApplyConfiguration(new SubstansiConfiguration());

            modelBuilder.Entity<Domain.Substansi.Substansi>(entity =>
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

                entity.Property(e => e.PenelitianPkmId)
                      .HasColumnName("id_pkm");

                entity.Property(e => e.File)
                      .HasColumnName("file");
            });
        }
    }
}
