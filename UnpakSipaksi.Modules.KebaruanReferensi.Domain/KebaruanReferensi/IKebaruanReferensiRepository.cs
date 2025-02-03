using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi
{
    public interface IKebaruanReferensiRepository
    {
        void Insert(KebaruanReferensi KebaruanReferensi);
        Task<KebaruanReferensi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KebaruanReferensi KebaruanReferensi);
    }
}
