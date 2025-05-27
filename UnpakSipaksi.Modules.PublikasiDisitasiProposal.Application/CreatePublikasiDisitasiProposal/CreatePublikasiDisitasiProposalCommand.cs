using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.CreatePublikasiDisitasiProposal
{
    public sealed record CreatePublikasiDisitasiProposalCommand(
        string Nama,
        int Skor
    ) : ICommand<Guid>;
}
