using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.SotaKebaharuan.PublicApi
{
    public interface ISotaKebaharuanApi
    {
        Task<SotaKebaharuanResponse?> GetAsync(Guid SotaKebaharuanUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
