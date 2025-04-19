using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.VideoKegiatan.PublicApi
{
    public interface IVideoKegiatanApi
    {
        Task<VideoKegiatanResponse?> GetAsync(Guid VideoKegiatanUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
