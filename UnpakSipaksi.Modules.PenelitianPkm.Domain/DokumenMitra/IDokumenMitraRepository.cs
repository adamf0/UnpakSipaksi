using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenMitra
{
    public interface IDokumenMitraRepository
    {
        void Insert(DokumenMitra DokumenMitra);
        Task<DokumenMitra> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(DokumenMitra DokumenMitra);
    }
}
