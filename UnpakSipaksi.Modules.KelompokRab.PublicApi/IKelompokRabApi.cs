using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KelompokRab.PublicApi
{
    public interface IKelompokRabApi
    {
        Task<KelompokRabResponse?> GetAsync(Guid KelompokRabUuid, CancellationToken cancellationToken = default);
    }

}
