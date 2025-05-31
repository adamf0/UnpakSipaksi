using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Komponen.PublicApi
{
    public interface IKomponenApi
    {
        Task<KomponenResponse?> GetAsync(Guid KomponenUuid, CancellationToken cancellationToken = default);
    }

}
