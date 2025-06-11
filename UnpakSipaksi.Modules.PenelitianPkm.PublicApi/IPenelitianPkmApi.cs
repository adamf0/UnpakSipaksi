using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.PublicApi
{
    public interface IPenelitianPkmApi
    {
        Task<PenelitianPkmResponse?> GetAsync(Guid PenelitianPkmUuid, CancellationToken cancellationToken = default);
    }

}
