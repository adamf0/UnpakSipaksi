using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Rirn.PublicApi
{
    public interface IRirnApi
    {
        Task<RirnResponse?> GetAsync(Guid RirnUuid, CancellationToken cancellationToken = default);
    }

}
