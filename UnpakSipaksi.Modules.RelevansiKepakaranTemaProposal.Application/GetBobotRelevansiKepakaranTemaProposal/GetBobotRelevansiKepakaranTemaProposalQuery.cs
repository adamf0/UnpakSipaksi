using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetBobotRelevansiKepakaranTemaProposal
{
    public sealed record GetBobotRelevansiKepakaranTemaProposalQuery(string KategoriSkema) : IQuery<int?>;
}
