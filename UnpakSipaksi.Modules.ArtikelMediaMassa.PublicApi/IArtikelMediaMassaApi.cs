using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.PublicApi
{
    public interface IArtikelMediaMassaApi
    {
        Task<ArtikelMediaMassaResponse?> GetAsync(Guid ArtikelMediaMassaUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default);
    }

}
