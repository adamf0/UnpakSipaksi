

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetPublikasiDisitasiProposal
{
    public sealed record GetPublikasiDisitasiProposalDefaultQuery(Guid PublikasiDisitasiProposalUuid) : IQuery<PublikasiDisitasiProposalDefaultResponse>;
}
