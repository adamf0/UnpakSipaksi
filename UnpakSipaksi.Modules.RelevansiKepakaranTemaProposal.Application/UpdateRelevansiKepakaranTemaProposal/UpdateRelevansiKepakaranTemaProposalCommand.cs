using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.UpdateRelevansiKepakaranTemaProposal
{
    public sealed record UpdateRelevansiKepakaranTemaProposalCommand(
        Guid Uuid,
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand;
}
