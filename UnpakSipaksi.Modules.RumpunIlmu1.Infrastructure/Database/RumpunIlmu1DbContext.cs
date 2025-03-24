using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.RumpunIlmu1.Infrastructure.RumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Infrastructure.Database
{
    public sealed class RumpunIlmu1DbContext(DbContextOptions<RumpunIlmu1DbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.RumpunIlmu1.RumpunIlmu1> RumpunIlmu1 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.RumpunIlmu1.RumpunIlmu1>().ToTable(Schemas.RumpunIlmu1);
            modelBuilder.ApplyConfiguration(new RumpunIlmu1Configuration());

            modelBuilder.Entity<Domain.RumpunIlmu1.RumpunIlmu1>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.RumpunIlmu1);

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
