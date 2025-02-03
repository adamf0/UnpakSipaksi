using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.TopikPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.TopikPenelitian.Infrastructure.TopikPenelitian;

namespace UnpakSipaksi.Modules.TopikPenelitian.Infrastructure.Database
{
    public sealed class TopikPenelitianDbContext(DbContextOptions<TopikPenelitianDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.TopikPenelitian.TopikPenelitian> TopikPenelitian { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.TopikPenelitian.TopikPenelitian>().ToTable(Schemas.TopikPenelitian);
            modelBuilder.ApplyConfiguration(new TopikPenelitianConfiguration());

            modelBuilder.Entity<Domain.TopikPenelitian.TopikPenelitian>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.TopikPenelitian);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");

                entity.Property(e => e.TemaPenelitianId)
                      .HasColumnName("id_bidang_fokus_penelitian_tema");

            });
        }
    }
}
