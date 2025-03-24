using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetRelevansiKepakaranTemaProposal
{
    public sealed record GetRelevansiKepakaranTemaProposalQuery(Guid RelevansiKepakaranTemaProposalUuid) : IQuery<RelevansiKepakaranTemaProposalResponse>;
}
