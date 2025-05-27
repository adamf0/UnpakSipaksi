using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.UpdateRelevansiKepakaranTemaProposal
{
    public sealed record UpdateRelevansiKepakaranTemaProposalCommand(
        string Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
