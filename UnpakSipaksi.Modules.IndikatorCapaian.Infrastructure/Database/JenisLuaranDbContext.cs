using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.IndikatorCapaian.Infrastructure.IndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Infrastructure.Database
{
    public sealed class IndikatorCapaianDbContext(DbContextOptions<IndikatorCapaianDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.IndikatorCapaian> IndikatorCapaian { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.IndikatorCapaian>().ToTable(Schemas.IndikatorCapaian);
            modelBuilder.ApplyConfiguration(new IndikatorCapaianConfiguration());

            modelBuilder.Entity<Domain.IndikatorCapaian>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.IndikatorCapaian);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");

                entity.Property(e => e.JenisLuaranId)
                      .HasColumnName("id_jenis_luaran");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");

                entity.Property(e => e.Status)
                      .HasColumnName("status");
            });
        }
    }
}
