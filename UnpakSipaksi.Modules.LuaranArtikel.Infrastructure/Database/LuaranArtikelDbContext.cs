using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.LuaranArtikel.Infrastructure.LuaranArtikel;
using UnpakSipaksi.Modules.LuaranArtikel.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.LuaranArtikel.Infrastructure.Database
{
    public sealed class LuaranArtikelDbContext(DbContextOptions<LuaranArtikelDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.LuaranArtikel.LuaranArtikel> LuaranArtikel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.LuaranArtikel.LuaranArtikel>().ToTable(Schemas.LuaranArtikel);
            modelBuilder.ApplyConfiguration(new LuaranArtikelConfiguration());

            modelBuilder.Entity<Domain.LuaranArtikel.LuaranArtikel>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.LuaranArtikel);

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
