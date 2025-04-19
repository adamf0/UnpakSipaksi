using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KebaruanReferensi.PublicApi
{
    public interface IKebaruanReferensiApi
    {
        Task<KebaruanReferensiResponse?> GetAsync(Guid KebaruanReferensiUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
