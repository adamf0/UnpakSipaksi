using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetPublikasiDisitasiProposal
{
    public sealed record GetPublikasiDisitasiProposalQuery(Guid PublikasiDisitasiProposalUuid) : IQuery<PublikasiDisitasiProposalResponse>;
}
