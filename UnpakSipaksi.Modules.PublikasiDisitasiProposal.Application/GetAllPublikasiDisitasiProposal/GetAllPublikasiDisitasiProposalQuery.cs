using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetPublikasiDisitasiProposal;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetAllPublikasiDisitasiProposal
{
    public sealed record GetAllPublikasiDisitasiProposalQuery() : IQuery<List<PublikasiDisitasiProposalResponse>>;
}
