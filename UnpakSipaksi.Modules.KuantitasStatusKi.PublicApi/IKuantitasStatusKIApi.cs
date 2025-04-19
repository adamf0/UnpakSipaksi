using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.PublicApi
{
    public interface IKuantitasStatusKiApi
    {
        Task<KuantitasStatusKiResponse?> GetAsync(Guid KuantitasStatusKiUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
