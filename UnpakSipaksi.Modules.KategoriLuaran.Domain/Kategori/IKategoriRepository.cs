using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori
{
    public interface IKategoriLuaranRepository
    {
        void Insert(Domain.KategoriLuaran.KategoriLuaran kategori);
        Task<Domain.KategoriLuaran.KategoriLuaran> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Domain.KategoriLuaran.KategoriLuaran kategori);
    }
}
