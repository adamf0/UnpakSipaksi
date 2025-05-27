using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Kategori.PublicApi
{
    public interface IKategoriApi
    {
        Task<KategoriResponse?> GetAsync(Guid KategoriUuid, CancellationToken cancellationToken = default);
    }

}
