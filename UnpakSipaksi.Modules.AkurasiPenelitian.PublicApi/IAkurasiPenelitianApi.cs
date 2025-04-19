using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.PublicApi
{
    public interface IAkurasiPenelitianApi
    {
        Task<AkurasiPenelitianResponse?> GetAsync(Guid AkurasiPenelitianUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
