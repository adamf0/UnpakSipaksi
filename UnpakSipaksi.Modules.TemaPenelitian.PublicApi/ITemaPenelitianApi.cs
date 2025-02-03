using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.TemaPenelitian.PublicApi
{
    public interface ITemaPenelitianApi
    {
        Task<TemaPenelitianResponse?> GetAsync(Guid TemaPenelitianUuid, CancellationToken cancellationToken = default);
    }

}
