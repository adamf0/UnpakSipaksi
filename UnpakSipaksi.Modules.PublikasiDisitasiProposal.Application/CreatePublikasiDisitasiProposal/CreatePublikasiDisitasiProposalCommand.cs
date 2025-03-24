using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.CreatePublikasiDisitasiProposal
{
    public sealed record CreatePublikasiDisitasiProposalCommand(
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand<Guid>;
}
