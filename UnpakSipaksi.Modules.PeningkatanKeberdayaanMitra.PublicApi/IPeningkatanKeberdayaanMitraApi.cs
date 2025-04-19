using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.PublicApi
{
    public interface IPeningkatanKeberdayaanMitraApi
    {
        Task<PeningkatanKeberdayaanMitraResponse?> GetAsync(Guid PeningkatanKeberdayaanMitraUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
