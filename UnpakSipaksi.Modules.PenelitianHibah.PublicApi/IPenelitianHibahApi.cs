using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.PublicApi
{
    public interface IPenelitianHibahApi
    {
        Task<PenelitianHibahResponse?> GetAsync(Guid PenelitianHibahUuid, CancellationToken cancellationToken = default);
    }

}
