using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.PubliApi
{
    public interface IKualitasKuantitasPublikasiJurnalIlmiahApi
    {
        Task<KualitasKuantitasPublikasiJurnalIlmiahResponse?> GetAsync(Guid KualitasKuantitasPublikasiJurnalIlmiahUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
