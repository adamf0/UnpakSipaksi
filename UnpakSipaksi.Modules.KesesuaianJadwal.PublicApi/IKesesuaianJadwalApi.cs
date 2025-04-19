using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.PublicApi
{
    public interface IKesesuaianJadwalApi
    {
        Task<KesesuaianJadwalResponse?> GetAsync(Guid KesesuaianJadwalUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
