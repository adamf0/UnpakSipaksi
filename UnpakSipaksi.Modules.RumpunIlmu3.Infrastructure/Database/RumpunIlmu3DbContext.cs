using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.RumpunIlmu3.Infrastructure.RumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Infrastructure.Database
{
    public sealed class RumpunIlmu3DbContext(DbContextOptions<RumpunIlmu3DbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.RumpunIlmu3.RumpunIlmu3> RumpunIlmu3 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.RumpunIlmu3.RumpunIlmu3>().ToTable(Schemas.RumpunIlmu3);
            modelBuilder.ApplyConfiguration(new RumpunIlmu3Configuration());

            modelBuilder.Entity<Domain.RumpunIlmu3.RumpunIlmu3>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.RumpunIlmu3);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");

                entity.Property(e => e.IdRumpunIlmu2)
                      .HasColumnName("id_rumpun_ilmu2");

            });
        }
    }
}
