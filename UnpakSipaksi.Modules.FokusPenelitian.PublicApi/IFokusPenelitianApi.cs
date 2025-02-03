using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.FokusPenelitian.PublicApi
{
    public interface IFokusPenelitianApi
    {
        Task<FokusPenelitianResponse?> GetAsync(Guid FokusPenelitianUuid, CancellationToken cancellationToken = default);
    }

}
