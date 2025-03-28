using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.PenugasanReviewer.Infrastructure.PenugasanReviewer;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Infrastructure.Database
{
    public sealed class PenugasanReviewerDbContext(DbContextOptions<PenugasanReviewerDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.PenugasanReviewer.PenugasanReviewer> PenugasanReviewer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.PenugasanReviewer.PenugasanReviewer>().ToTable(Schemas.PenugasanReviewer);
            modelBuilder.ApplyConfiguration(new PenugasanReviewerConfiguration());

            modelBuilder.Entity<Domain.PenugasanReviewer.PenugasanReviewer>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.PenugasanReviewer);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nidn)
                      .HasColumnName("NIDN");

                entity.Property(e => e.Status)
                      .HasColumnName("status");

            });
        }
    }
}
