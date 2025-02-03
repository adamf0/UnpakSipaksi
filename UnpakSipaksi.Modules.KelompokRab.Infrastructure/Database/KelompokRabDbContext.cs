using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KelompokRab.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KelompokRab.Infrastructure.KelompokRab;

namespace UnpakSipaksi.Modules.KelompokRab.Infrastructure.Database
{
    public sealed class KelompokRabDbContext(DbContextOptions<KelompokRabDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KelompokRab.KelompokRab> KelompokRab { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KelompokRab.KelompokRab>().ToTable(Schemas.KelompokRab);
            modelBuilder.ApplyConfiguration(new KelompokRabConfiguration());

            modelBuilder.Entity<Domain.KelompokRab.KelompokRab>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KelompokRab);

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
