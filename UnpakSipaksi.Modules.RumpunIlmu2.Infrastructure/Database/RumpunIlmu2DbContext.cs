using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.RumpunIlmu2.Infrastructure.RumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Infrastructure.Database
{
    public sealed class RumpunIlmu2DbContext(DbContextOptions<RumpunIlmu2DbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.RumpunIlmu2.RumpunIlmu2> RumpunIlmu2 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.RumpunIlmu2.RumpunIlmu2>().ToTable(Schemas.RumpunIlmu2);
            modelBuilder.ApplyConfiguration(new RumpunIlmu2Configuration());

            modelBuilder.Entity<Domain.RumpunIlmu2.RumpunIlmu2>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.RumpunIlmu2);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");

                entity.Property(e => e.IdRumpunIlmu1)
                      .HasColumnName("id_rumpun_ilmu1");

            });
        }
    }
}
