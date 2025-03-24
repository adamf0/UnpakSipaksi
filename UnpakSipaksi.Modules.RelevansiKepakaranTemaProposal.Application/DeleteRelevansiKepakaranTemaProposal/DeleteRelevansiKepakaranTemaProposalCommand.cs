using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.DeleteRelevansiKepakaranTemaProposal
{
    public sealed record DeleteRelevansiKepakaranTemaProposalCommand(
        Guid uuid
    ) : ICommand;
}
