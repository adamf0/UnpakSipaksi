using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Satuan.PublicApi
{
    public interface ISatuanApi
    {
        Task<SatuanResponse?> GetAsync(Guid SatuanUuid, CancellationToken cancellationToken = default);
    }

}
