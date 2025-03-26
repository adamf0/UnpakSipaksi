using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RumpunIlmu3.PublicApi
{
    public interface IRumpunIlmu3Api
    {
        Task<RumpunIlmu3Response?> GetAsync(Guid RumpunIlmu3Uuid, CancellationToken cancellationToken = default);
    }

}
