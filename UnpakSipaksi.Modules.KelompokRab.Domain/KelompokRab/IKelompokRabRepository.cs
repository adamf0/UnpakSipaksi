using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KelompokRab.Domain.KelompokRab
{
    public interface IKelompokRabRepository
    {
        void Insert(KelompokRab KelompokRab);
        Task<KelompokRab> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KelompokRab KelompokRab);
    }
}
