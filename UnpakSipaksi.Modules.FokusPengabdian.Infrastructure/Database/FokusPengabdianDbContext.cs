using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.FokusPengabdian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.FokusPengabdian.Infrastructure.FokusPengabdian;

namespace UnpakSipaksi.Modules.FokusPengabdian.Infrastructure.Database
{
    public sealed class FokusPengabdianDbContext(DbContextOptions<FokusPengabdianDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.FokusPengabdian.FokusPengabdian> FokusPengabdian { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.FokusPengabdian.FokusPengabdian>().ToTable(Schemas.FokusPengabdian);
            modelBuilder.ApplyConfiguration(new FokusPengabdianConfiguration());

            modelBuilder.Entity<Domain.FokusPengabdian.FokusPengabdian>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.FokusPengabdian);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("name");
            });
        }
    }
}
