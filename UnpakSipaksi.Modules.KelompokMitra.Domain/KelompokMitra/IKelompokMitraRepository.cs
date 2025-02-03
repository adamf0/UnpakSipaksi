using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra
{
    public interface IKelompokMitraRepository
    {
        void Insert(KelompokMitra KelompokMitra);
        Task<KelompokMitra> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KelompokMitra KelompokMitra);
    }
}
