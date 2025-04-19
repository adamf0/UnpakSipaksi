using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.PublicApi
{
    public interface IRumusanPrioritasMitraApi
    {
        Task<RumusanPrioritasMitraResponse?> GetAsync(Guid RumusanPrioritasMitraUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
