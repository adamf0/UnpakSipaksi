using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt
{
    public interface IKategoriTktRepository
    {
        void Insert(KategoriTkt KategoriTkt);
        Task<KategoriTkt> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KategoriTkt KategoriTkt);
    }
}
