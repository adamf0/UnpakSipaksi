using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Rirn.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Rirn.Infrastructure.Rirn;

namespace UnpakSipaksi.Modules.Rirn.Infrastructure.Database
{
    public sealed class RirnDbContext(DbContextOptions<RirnDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.Rirn.Rirn> Rirn { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Rirn.Rirn>().ToTable(Schemas.Rirn);
            modelBuilder.ApplyConfiguration(new RirnConfiguration());

            modelBuilder.Entity<Domain.Rirn.Rirn>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.Rirn);

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
