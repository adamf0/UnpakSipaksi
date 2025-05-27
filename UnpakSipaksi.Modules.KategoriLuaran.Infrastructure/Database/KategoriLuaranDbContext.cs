using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KategoriLuaran.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriLuaran.Infrastructure.KategoriLuaran;

namespace UnpakSipaksi.Modules.KategoriLuaran.Infrastructure.Database
{
    public sealed class KategoriLuaranDbContext(DbContextOptions<KategoriLuaranDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KategoriLuaran.KategoriLuaran> KategoriLuaran { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KategoriLuaran.KategoriLuaran>().ToTable(Schemas.KategoriLuaran);
            modelBuilder.ApplyConfiguration(new KategoriLuaranConfiguration());

            modelBuilder.Entity<Domain.KategoriLuaran.KategoriLuaran>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KategoriLuaran);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.KategoriId)
                      .HasColumnName("id_pdp_kategori");

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");

                entity.Property(e => e.Status)
                      .HasColumnName("status");
            });
        }
    }
}
