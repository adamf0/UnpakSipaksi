using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.TopikPenelitian.PublicApi
{
    public interface ITopikPenelitianApi
    {
        Task<TopikPenelitianResponse?> GetAsync(Guid TopikPenelitianUuid, CancellationToken cancellationToken = default);
    }

}
