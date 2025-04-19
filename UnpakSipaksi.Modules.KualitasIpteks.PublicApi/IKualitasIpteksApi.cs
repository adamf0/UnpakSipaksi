using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KualitasIpteks.PublicApi
{
    public interface IKualitasIpteksApi
    {
        Task<KualitasIpteksResponse?> GetAsync(Guid KesesuaianPenugasanUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
