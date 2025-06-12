using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.FokusPengabdian.PublicApi
{
    public interface IFokusPengabdianApi
    {
        Task<FokusPengabdianResponse?> GetAsync(Guid FokusPengabdianUuid, CancellationToken cancellationToken = default);
    }

}
