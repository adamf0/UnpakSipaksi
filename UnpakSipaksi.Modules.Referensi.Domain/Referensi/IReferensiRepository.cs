using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Referensi.Domain.Referensi
{
    public interface IReferensiRepository
    {
        void Insert(Referensi Referensi);
        Task<Referensi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Referensi Referensi);
    }
}
