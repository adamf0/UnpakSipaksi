using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.PublicApi
{
    public interface IPotensiKetercapaianLuaranDijanjikanApi
    {
        Task<PotensiKetercapaianLuaranDijanjikanResponse?> GetAsync(Guid PotensiKetercapaianLuaranDijanjikanUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
