using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana
{
    public interface IKategoriSumberDanaRepository
    {
        void Insert(KategoriSumberDana KategoriSumberDana);
        Task<KategoriSumberDana> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KategoriSumberDana KategoriSumberDana);
    }
}
