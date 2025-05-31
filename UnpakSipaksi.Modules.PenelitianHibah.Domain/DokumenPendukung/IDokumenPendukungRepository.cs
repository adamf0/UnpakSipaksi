using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenPendukung
{
    public interface IDokumenPendukungRepository
    {
        void Insert(DokumenPendukung DokumenPendukung);
        Task<DokumenPendukung> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(DokumenPendukung DokumenPendukung);
    }
}
