using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.PublicApi
{
    public interface IKetajamanAnalisisApi
    {
        Task<KetajamanAnalisisResponse?> GetAsync(Guid KetajamanAnalisisUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
