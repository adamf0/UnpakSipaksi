using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1
{
    public interface IRumpunIlmu1Repository
    {
        void Insert(RumpunIlmu1 RumpunIlmu1);
        Task<RumpunIlmu1> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(RumpunIlmu1 RumpunIlmu1);
    }
}
