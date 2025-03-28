using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Infrastructure.KategoriProgramPengabdian;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Infrastructure.Database
{
    public sealed class KategoriProgramPengabdianDbContext(DbContextOptions<KategoriProgramPengabdianDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KategoriProgramPengabdian.KategoriProgramPengabdian> KategoriProgramPengabdian { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KategoriProgramPengabdian.KategoriProgramPengabdian>().ToTable(Schemas.KategoriProgramPengabdian);
            modelBuilder.ApplyConfiguration(new KategoriProgramPengabdianConfiguration());

            modelBuilder.Entity<Domain.KategoriProgramPengabdian.KategoriProgramPengabdian>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KategoriProgramPengabdian);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("nama");

                entity.Property(e => e.Rule)
                      .HasColumnName("rule");

            });
        }
    }
}
