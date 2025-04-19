using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.PubliApi
{
    public interface IKualitasKuantitasPublikasiProsidingApi
    {
        Task<KualitasKuantitasPublikasiProsidingResponse?> GetAsync(Guid KualitasKuantitasPublikasiProsidingUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
