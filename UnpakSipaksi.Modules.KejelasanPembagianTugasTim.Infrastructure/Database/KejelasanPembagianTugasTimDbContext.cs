using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Infrastructure.KejelasanPembagianTugasTim;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Infrastructure.Database
{
    public sealed class KejelasanPembagianTugasTimDbContext(DbContextOptions<KejelasanPembagianTugasTimDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim> KejelasanPembagianTugasTim { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim>().ToTable(Schemas.KejelasanPembagianTugasTim);
            modelBuilder.ApplyConfiguration(new KejelasanPembagianTugasTimConfiguration());

            modelBuilder.Entity<Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KejelasanPembagianTugasTim);

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Uuid)
                      .HasColumnName("uuid")
                      .HasColumnType("VARCHAR(36)");
                //.HasConversion(guidConverter);

                entity.Property(e => e.Nama)
                      .HasColumnName("name");

                entity.Property(e => e.Skor)
                      .HasColumnName("skor");

            });
        }
    }
}
