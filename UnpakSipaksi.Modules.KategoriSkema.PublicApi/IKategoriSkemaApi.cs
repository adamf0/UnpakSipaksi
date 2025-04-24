using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriSkema.PublicApi
{
    public interface IKategoriSkemaApi
    {
        Task<KategoriSkemaResponse?> GetAsync(Guid KategoriSkemaUuid, CancellationToken cancellationToken = default);
    }

}
