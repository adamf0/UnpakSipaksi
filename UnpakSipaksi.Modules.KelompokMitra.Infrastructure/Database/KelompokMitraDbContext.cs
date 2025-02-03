using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KelompokMitra.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KelompokMitra.Infrastructure.KelompokMitra;

namespace UnpakSipaksi.Modules.KelompokMitra.Infrastructure.Database
{
    public sealed class KelompokMitraDbContext(DbContextOptions<KelompokMitraDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KelompokMitra.KelompokMitra> KelompokMitra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KelompokMitra.KelompokMitra>().ToTable(Schemas.KelompokMitra);
            modelBuilder.ApplyConfiguration(new KelompokMitraConfiguration());

            modelBuilder.Entity<Domain.KelompokMitra.KelompokMitra>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KelompokMitra);

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
