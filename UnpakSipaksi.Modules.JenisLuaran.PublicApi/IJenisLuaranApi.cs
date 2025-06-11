using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.JenisLuaran.PublicApi
{
    public interface IJenisLuaranApi
    {
        Task<JenisLuaranResponse?> GetAsync(Guid JenisLuaranUuid, CancellationToken cancellationToken = default);
    }

}
