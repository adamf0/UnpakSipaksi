using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.PublicApi
{
    public interface IKetajamanPerumusanMasalahApi
    {
        Task<KetajamanPerumusanMasalahResponse?> GetAsync(Guid KetajamanPerumusanMasalahUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
