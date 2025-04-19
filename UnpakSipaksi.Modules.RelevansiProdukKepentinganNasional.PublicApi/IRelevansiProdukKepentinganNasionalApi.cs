using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.PublicApi
{
    public interface IRelevansiProdukKepentinganNasionalApi
    {
        Task<RelevansiProdukKepentinganNasionalResponse?> GetAsync(Guid RelevansiProdukKepentinganNasionalUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
