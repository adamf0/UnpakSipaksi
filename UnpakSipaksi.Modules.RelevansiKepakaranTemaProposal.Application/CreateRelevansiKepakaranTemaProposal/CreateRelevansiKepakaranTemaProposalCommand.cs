using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.CreateRelevansiKepakaranTemaProposal
{
    public sealed record CreateRelevansiKepakaranTemaProposalCommand(
        string Nama,
        int Skor
    ) : ICommand<Guid>;
}
