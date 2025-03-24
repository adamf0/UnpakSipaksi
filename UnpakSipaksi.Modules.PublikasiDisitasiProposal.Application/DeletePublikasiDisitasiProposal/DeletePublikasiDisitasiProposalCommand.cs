using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.PublikasiDisitasiProposal
{
    public sealed record DeletePublikasiDisitasiProposalCommand(
        Guid uuid
    ) : ICommand;
}
