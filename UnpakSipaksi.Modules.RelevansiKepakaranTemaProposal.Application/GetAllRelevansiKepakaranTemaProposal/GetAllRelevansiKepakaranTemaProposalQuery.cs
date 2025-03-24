using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetRelevansiKepakaranTemaProposal;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetAllRelevansiKepakaranTemaProposal
{
    public sealed record GetAllRelevansiKepakaranTemaProposalQuery() : IQuery<List<RelevansiKepakaranTemaProposalResponse>>;
}
