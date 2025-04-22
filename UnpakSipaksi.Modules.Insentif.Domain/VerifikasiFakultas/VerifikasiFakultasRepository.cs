using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas
{
    public interface IVerifikasiFakultasRepository
    {
        void Insert(VerifikasiFakultas VerifikasiFakultas);
        Task<VerifikasiFakultas> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(VerifikasiFakultas VerifikasiFakultas);
    }
}
