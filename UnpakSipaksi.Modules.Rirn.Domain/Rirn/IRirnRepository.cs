using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Rirn.Domain.Rirn
{
    public interface IRirnRepository
    {
        void Insert(Rirn Rirn);
        Task<Rirn> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Rirn Rirn);
    }
}
