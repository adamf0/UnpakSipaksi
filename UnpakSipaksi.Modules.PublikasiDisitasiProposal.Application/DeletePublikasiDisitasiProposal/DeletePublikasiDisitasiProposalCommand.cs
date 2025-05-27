using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.DeletePublikasiDisitasiProposal
{
    public sealed record DeletePublikasiDisitasiProposalCommand(
        string uuid
    ) : ICommand;
}
