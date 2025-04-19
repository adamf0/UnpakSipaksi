
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.PublicApi
{
    public interface IMetodeRencanaKegiatanApi
    {
        Task<MetodeRencanaKegiatanResponse?> GetAsync(Guid MetodeRencanaKegiatanUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }
}
