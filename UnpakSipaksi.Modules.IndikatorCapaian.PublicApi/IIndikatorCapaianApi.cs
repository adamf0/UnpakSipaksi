using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.IndikatorCapaian.PublicApi
{
    public interface IIndikatorCapaianApi
    {
        Task<IndikatorCapaianResponse?> GetAsync(Guid IndikatorCapaianUuid, CancellationToken cancellationToken = default);
    }

}
