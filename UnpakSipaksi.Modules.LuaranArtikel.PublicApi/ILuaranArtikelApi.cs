using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.LuaranArtikel.PublicApi
{
    public interface ILuaranArtikelApi
    {
        Task<LuaranArtikelResponse?> GetAsync(Guid LuaranArtikelUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
