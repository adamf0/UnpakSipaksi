using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.RAB
{
    public interface IRABRepository
    {
        void Insert(RAB RAB);
        Task<RAB> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(RAB RAB);
    }
}
