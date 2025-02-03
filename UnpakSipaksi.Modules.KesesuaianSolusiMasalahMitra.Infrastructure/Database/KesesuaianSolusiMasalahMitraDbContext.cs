using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Infrastructure.KesesuaianSolusiMasalahMitra;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Infrastructure.Database
{
    public sealed class KesesuaianSolusiMasalahMitraDbContext(DbContextOptions<KesesuaianSolusiMasalahMitraDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra> KesesuaianSolusiMasalahMitra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra>().ToTable(Schemas.KesesuaianSolusiMasalahMitra);
            modelBuilder.ApplyConfiguration(new KesesuaianSolusiMasalahMitraConfiguration());

            modelBuilder.Entity<Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KesesuaianSolusiMasalahMitra);

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
