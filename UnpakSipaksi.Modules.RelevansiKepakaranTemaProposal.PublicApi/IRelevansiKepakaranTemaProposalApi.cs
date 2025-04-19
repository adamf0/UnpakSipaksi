using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.PublicApi
{
    public interface IRelevansiKepakaranTemaProposalApi
    {
        Task<RelevansiKepakaranTemaProposalResponse?> GetAsync(Guid RelevansiKepakaranTemaProposalUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
