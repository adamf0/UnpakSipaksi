using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.PublicApi
{
    public interface IKredibilitasMitraDukunganApi
    {
        Task<KredibilitasMitraDukunganResponse?> GetAsync(Guid KredibilitasMitraDukunganUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
