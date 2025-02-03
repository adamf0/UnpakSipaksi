using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.AuthorSinta.Application.Abstractions.Data;
using UnpakSipaksi.Modules.AuthorSinta.Infrastructure.AuthorSinta;

namespace UnpakSipaksi.Modules.AuthorSinta.Infrastructure.Database
{
    public sealed class AuthorSintaDbContext(DbContextOptions<AuthorSintaDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.AuthorSinta.AuthorSinta> AuthorSinta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.AuthorSinta.AuthorSinta>().ToTable(Schemas.AuthorSinta);
            modelBuilder.ApplyConfiguration(new AuthorSintaConfiguration());

            modelBuilder.Entity<Domain.AuthorSinta.AuthorSinta>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.AuthorSinta);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nidn)
                      .HasColumnName("NIDN");

                entity.Property(e => e.AuthorId)
                      .HasColumnName("author_id");

                entity.Property(e => e.Score)
                      .HasColumnName("score");
            });
        }
    }
}
