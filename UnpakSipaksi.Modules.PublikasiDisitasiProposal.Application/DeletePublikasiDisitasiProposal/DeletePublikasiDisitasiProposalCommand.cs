using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.DeletePublikasiDisitasiProposal
{
    public sealed record DeletePublikasiDisitasiProposalCommand(
        Guid uuid
    ) : ICommand;
}
