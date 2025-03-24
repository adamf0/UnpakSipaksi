using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Infrastructure.PublikasiDisitasiProposal
{
    internal sealed class PublikasiDisitasiProposalConfiguration : IEntityTypeConfiguration<Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal>
    {
        public void Configure(EntityTypeBuilder<Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
