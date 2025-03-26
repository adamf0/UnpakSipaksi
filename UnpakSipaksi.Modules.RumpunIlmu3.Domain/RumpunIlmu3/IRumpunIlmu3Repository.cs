using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3
{
    public interface IRumpunIlmu3Repository
    {
        void Insert(RumpunIlmu3 RumpunIlmu3);
        Task<RumpunIlmu3> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(RumpunIlmu3 RumpunIlmu3);
    }
}
