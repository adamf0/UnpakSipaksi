using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.PublicApi
{
    public interface IKesesuaianPenugasanApi
    {
        Task<KesesuaianPenugasanResponse?> GetAsync(Guid KesesuaianPenugasanUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
