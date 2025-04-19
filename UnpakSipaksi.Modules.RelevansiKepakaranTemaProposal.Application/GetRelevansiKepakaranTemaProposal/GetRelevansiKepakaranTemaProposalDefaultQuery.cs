

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetRelevansiKepakaranTemaProposal
{
    public sealed record GetRelevansiKepakaranTemaProposalDefaultQuery(Guid RelevansiKepakaranTemaProposalUuid) : IQuery<RelevansiKepakaranTemaProposalDefaultResponse>;
}
