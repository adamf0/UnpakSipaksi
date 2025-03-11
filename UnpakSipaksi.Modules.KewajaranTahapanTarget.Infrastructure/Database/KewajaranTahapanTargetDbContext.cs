using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Infrastructure.KewajaranTahapanTarget;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Infrastructure.Database
{
    public sealed class KewajaranTahapanTargetDbContext(DbContextOptions<KewajaranTahapanTargetDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KewajaranTahapanTarget.KewajaranTahapanTarget> KewajaranTahapanTarget { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KewajaranTahapanTarget.KewajaranTahapanTarget>().ToTable(Schemas.KewajaranTahapanTarget);
            modelBuilder.ApplyConfiguration(new KewajaranTahapanTargetConfiguration());

            modelBuilder.Entity<Domain.KewajaranTahapanTarget.KewajaranTahapanTarget>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KewajaranTahapanTarget);

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
