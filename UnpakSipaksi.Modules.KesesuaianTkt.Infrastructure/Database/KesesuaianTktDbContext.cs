using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KesesuaianTkt.Infrastructure.KesesuaianTkt;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Infrastructure.Database
{
    public sealed class KesesuaianTktDbContext(DbContextOptions<KesesuaianTktDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.KesesuaianTkt.KesesuaianTkt> KesesuaianTkt { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.KesesuaianTkt.KesesuaianTkt>().ToTable(Schemas.KesesuaianTkt);
            modelBuilder.ApplyConfiguration(new KesesuaianTktConfiguration());

            modelBuilder.Entity<Domain.KesesuaianTkt.KesesuaianTkt>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.KesesuaianTkt);

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
