using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PrioritasRiset.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PrioritasRiset.Infrastructure.PrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.Infrastructure.Database
{
    public sealed class PrioritasRisetDbContext(DbContextOptions<PrioritasRisetDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.PrioritasRiset.PrioritasRiset> PrioritasRiset { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.PrioritasRiset.PrioritasRiset>().ToTable(Schemas.PrioritasRiset);
            modelBuilder.ApplyConfiguration(new PrioritasRisetConfiguration());

            modelBuilder.Entity<Domain.PrioritasRiset.PrioritasRiset>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.PrioritasRiset);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");
            });
        }
    }
}
