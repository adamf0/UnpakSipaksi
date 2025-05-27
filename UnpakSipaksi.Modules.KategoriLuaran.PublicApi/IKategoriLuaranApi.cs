using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriLuaran.PublicApi
{
    public interface IKategoriLuaranApi
    {
        Task<KategoriLuaranResponse?> GetAsync(Guid KategoriUuid, CancellationToken cancellationToken = default);
    }

}
