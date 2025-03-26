using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RumpunIlmu2.PublicApi
{
    public interface IRumpunIlmu2Api
    {
        Task<RumpunIlmu2Response?> GetAsync(Guid RumpunIlmu2Uuid, CancellationToken cancellationToken = default);
    }

}
