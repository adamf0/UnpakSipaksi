using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2
{
    public interface IRumpunIlmu2Repository
    {
        void Insert(RumpunIlmu2 RumpunIlmu2);
        Task<RumpunIlmu2> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(RumpunIlmu2 RumpunIlmu2);
    }
}
