using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KelompokMitra.PublicApi
{
    public interface IKelompokMitraApi
    {
        Task<KelompokMitraResponse?> GetAsync(Guid KelompokMitraUuid, CancellationToken cancellationToken = default);
    }

}
