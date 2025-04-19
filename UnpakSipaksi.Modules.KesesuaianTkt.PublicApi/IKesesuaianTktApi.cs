using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianTkt.PublicApi
{
    public interface IKesesuaianTktApi
    {
        Task<KesesuaianTktResponse?> GetAsync(Guid KesesuaianTktUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
