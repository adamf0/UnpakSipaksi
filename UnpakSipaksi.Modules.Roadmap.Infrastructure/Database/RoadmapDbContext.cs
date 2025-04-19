using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.Roadmap.Infrastructure.Roadmap;
using UnpakSipaksi.Modules.Roadmap.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.Roadmap.Infrastructure.Database
{
    public sealed class RoadmapDbContext(DbContextOptions<RoadmapDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.Roadmap.Roadmap> Roadmap { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Roadmap.Roadmap>().ToTable(Schemas.Roadmap);
            modelBuilder.ApplyConfiguration(new RoadmapConfiguration());

            modelBuilder.Entity<Domain.Roadmap.Roadmap>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.Roadmap);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nidn)
                      .HasColumnName("NIDN");

                entity.Property(e => e.Link)
                      .HasColumnName("link");

            });
        }
    }
}
