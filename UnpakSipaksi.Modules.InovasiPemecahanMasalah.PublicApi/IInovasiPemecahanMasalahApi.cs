using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.PublicApi
{
    public interface IInovasiPemecahanMasalahApi
    {
        Task<InovasiPemecahanMasalahResponse?> GetAsync(Guid InovasiPemecahanMasalahUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
