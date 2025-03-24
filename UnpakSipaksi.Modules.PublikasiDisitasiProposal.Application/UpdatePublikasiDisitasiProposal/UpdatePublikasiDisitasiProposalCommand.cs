using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.UpdatePublikasiDisitasiProposal
{
    public sealed record UpdatePublikasiDisitasiProposalCommand(
        Guid Uuid,
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand;
}
