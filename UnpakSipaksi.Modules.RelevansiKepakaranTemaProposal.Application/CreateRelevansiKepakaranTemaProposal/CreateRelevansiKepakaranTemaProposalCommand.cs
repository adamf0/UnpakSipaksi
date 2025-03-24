using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.CreateRelevansiKepakaranTemaProposal
{
    public sealed record CreateRelevansiKepakaranTemaProposalCommand(
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand<Guid>;
}
