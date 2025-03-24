using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Infrastructure.RelevansiKepakaranTemaProposal
{
    internal sealed class RelevansiKepakaranTemaProposalConfiguration : IEntityTypeConfiguration<Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal>
    {
        public void Configure(EntityTypeBuilder<Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
