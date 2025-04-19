using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.PublicApi
{
    public interface IKejelasanPembagianTugasTimApi
    {
        Task<KejelasanPembagianTugasTimResponse?> GetAsync(Guid KejelasanPembagianTugasTimUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
