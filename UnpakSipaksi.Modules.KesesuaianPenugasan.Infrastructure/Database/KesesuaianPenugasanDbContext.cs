using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Infrastructure.KesesuaianPenugasan;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Infrastructure.Database
{
    public sealed class KesesuaianPenugasanDbContext(DbContextOptions<KesesuaianPenugasanDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KesesuaianPenugasan.KesesuaianPenugasan> KesesuaianPenugasan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KesesuaianPenugasan.KesesuaianPenugasan>().ToTable(Schemas.KesesuaianPenugasan);
            modelBuilder.ApplyConfiguration(new KesesuaianPenugasanConfiguration());

            modelBuilder.Entity<Domain.KesesuaianPenugasan.KesesuaianPenugasan>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KesesuaianPenugasan);

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
