using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Infrastructure.RumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Infrastructure.Database
{
    public sealed class RumusanPrioritasMitraDbContext(DbContextOptions<RumusanPrioritasMitraDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.RumusanPrioritasMitra.RumusanPrioritasMitra> RumusanPrioritasMitra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.RumusanPrioritasMitra.RumusanPrioritasMitra>().ToTable(Schemas.RumusanPrioritasMitra);
            modelBuilder.ApplyConfiguration(new RumusanPrioritasMitraConfiguration());

            modelBuilder.Entity<Domain.RumusanPrioritasMitra.RumusanPrioritasMitra>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.RumusanPrioritasMitra);

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
