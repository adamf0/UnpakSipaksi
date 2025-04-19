using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.PublicApi
{
    public interface IPublikasiDisitasiProposalApi
    {
        Task<PublikasiDisitasiProposalResponse?> GetAsync(Guid PublikasiDisitasiProposalUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
