using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.UpdatePublikasiDisitasiProposal
{
    public sealed record UpdatePublikasiDisitasiProposalCommand(
        string Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
