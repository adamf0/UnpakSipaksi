using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Infrastructure.Database;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Infrastructure.RelevansiKepakaranTemaProposal
{
    internal sealed class RelevansiKepakaranTemaProposalRepository(RelevansiKepakaranTemaProposalDbContext context) : IRelevansiKepakaranTemaProposalRepository
    {
        public async Task<Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal RelevansiKepakaranTemaProposal = await context.RelevansiKepakaranTemaProposal.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return RelevansiKepakaranTemaProposal;
        }

        public async Task DeleteAsync(Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal RelevansiKepakaranTemaProposal)
        {
            context.RelevansiKepakaranTemaProposal.Remove(RelevansiKepakaranTemaProposal);
        }

        public void Insert(Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal RelevansiKepakaranTemaProposal)
        {
            context.RelevansiKepakaranTemaProposal.Add(RelevansiKepakaranTemaProposal);
        }
    }
}
