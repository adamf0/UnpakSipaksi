using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.PublicApi
{
    public interface IKesesuaianWaktuRabLuaranFasilitasApi
    {
        Task<KesesuaianWaktuRabLuaranFasilitasResponse?> GetAsync(Guid KesesuaianWaktuRabLuaranFasilitasUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
