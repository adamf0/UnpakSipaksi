using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Infrastructure.RelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Infrastructure.Database
{
    public sealed class RelevansiKepakaranTemaProposalDbContext(DbContextOptions<RelevansiKepakaranTemaProposalDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal> RelevansiKepakaranTemaProposal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal>().ToTable(Schemas.RelevansiKepakaranTemaProposal);
            modelBuilder.ApplyConfiguration(new RelevansiKepakaranTemaProposalConfiguration());

            modelBuilder.Entity<Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal>(entity =>
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString("D"), // Mengonversi Guid ke string dengan format "N" (tidak ada tanda hubung)
                    v => Guid.ParseExact(v, "D") // Mengonversi string kembali menjadi Guid
                );
                entity.ToTable(Schemas.RelevansiKepakaranTemaProposal);

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
