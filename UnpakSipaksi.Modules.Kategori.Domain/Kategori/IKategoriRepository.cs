using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Kategori.Domain.Kategori
{
    public interface IKategoriRepository
    {
        void Insert(Kategori kategori);
        Task<Kategori> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Kategori kategori);
    }
}
