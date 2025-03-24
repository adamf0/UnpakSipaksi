using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RumpunIlmu1.PublicApi
{
    public interface IRumpunIlmu1Api
    {
        Task<RumpunIlmu1Response?> GetAsync(Guid RumpunIlmu1Uuid, CancellationToken cancellationToken = default);
    }

}
