using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset
{
    public interface IPrioritasRisetRepository
    {
        void Insert(PrioritasRiset PrioritasRiset);
        Task<PrioritasRiset> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(PrioritasRiset PrioritasRiset);
    }
}
