using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KuantitasStatusKi.Infrastructure.KuantitasStatusKi;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Infrastructure.Database
{
    public sealed class KuantitasStatusKiDbContext(DbContextOptions<KuantitasStatusKiDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KuantitasStatusKi.KuantitasStatusKi> KuantitasStatusKi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KuantitasStatusKi.KuantitasStatusKi>().ToTable(Schemas.KuantitasStatusKi);
            modelBuilder.ApplyConfiguration(new KuantitasStatusKiConfiguration());

            modelBuilder.Entity<Domain.KuantitasStatusKi.KuantitasStatusKi>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KuantitasStatusKi);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");

                entity.Property(e => e.Nilai)
                      .HasColumnName("nilai");

            });
        }
    }
}
