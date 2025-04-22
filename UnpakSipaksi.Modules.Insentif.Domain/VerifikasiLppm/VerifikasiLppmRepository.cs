using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Domain.VerifikasiLppm
{
    public interface IVerifikasiLppmRepository
    {
        void Insert(VerifikasiLppm VerifikasiLppm);
        Task<VerifikasiLppm> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(VerifikasiLppm VerifikasiLppm);
    }
}
