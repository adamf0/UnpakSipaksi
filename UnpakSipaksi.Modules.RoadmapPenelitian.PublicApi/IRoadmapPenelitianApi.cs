
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.PublicApi
{
    public interface IRoadmapPenelitianApi
    {
        Task<RoadmapPenelitianResponse?> GetAsync(Guid RoadmapPenelitianUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
