using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.PublicApi
{
    public interface IKewajaranTahapanTargetApi
    {
        Task<KewajaranTahapanTargetResponse?> GetAsync(Guid KewajaranTahapanTargetUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
