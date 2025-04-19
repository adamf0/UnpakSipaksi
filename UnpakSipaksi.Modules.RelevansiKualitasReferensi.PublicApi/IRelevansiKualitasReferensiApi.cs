using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.PublicApi
{
    public interface IRelevansiKualitasReferensiApi
    {
        Task<RelevansiKualitasReferensiResponse?> GetAsync(Guid RelevansiKualitasReferensiUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
