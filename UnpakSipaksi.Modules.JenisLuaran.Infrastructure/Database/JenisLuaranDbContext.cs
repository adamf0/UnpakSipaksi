using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.JenisLuaran.Infrastructure.JenisLuaran;
using UnpakSipaksi.Modules.JenisLuaran.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.JenisLuaran.Infrastructure.Database
{
    public sealed class JenisLuaranDbContext(DbContextOptions<JenisLuaranDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.JenisLuaran> JenisLuaran { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.JenisLuaran>().ToTable(Schemas.JenisLuaran);
            modelBuilder.ApplyConfiguration(new JenisLuaranConfiguration());

            modelBuilder.Entity<Domain.JenisLuaran>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.JenisLuaran);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");
            });
        }
    }
}
