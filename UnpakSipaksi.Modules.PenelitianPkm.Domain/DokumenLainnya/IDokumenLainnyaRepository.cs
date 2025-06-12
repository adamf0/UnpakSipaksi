using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenLainnya
{
    public interface IDokumenLainnyaRepository
    {
        void Insert(DokumenLainnya DokumenLainnya);
        Task<DokumenLainnya> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(DokumenLainnya DokumenLainnya);
    }
}
