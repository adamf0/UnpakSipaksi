using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.PublicApi
{
    public interface IModelFeasibilityStudysApi
    {
        Task<ModelFeasibilityStudysResponse?> GetAsync(Guid ModelFeasibilityStudysUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
