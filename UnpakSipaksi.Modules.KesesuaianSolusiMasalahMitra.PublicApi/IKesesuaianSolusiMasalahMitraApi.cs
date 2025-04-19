using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.PublicApi
{
    public interface IKesesuaianSolusiMasalahMitraApi
    {
        Task<KesesuaianSolusiMasalahMitraResponse?> GetAsync(Guid KesesuaianSolusiMasalahMitraUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
