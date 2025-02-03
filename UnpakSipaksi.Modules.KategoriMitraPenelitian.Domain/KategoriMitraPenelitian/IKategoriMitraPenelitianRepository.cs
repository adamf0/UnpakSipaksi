using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Domain.KategoriMitraPenelitian
{
    public interface IKategoriMitraPenelitianRepository
    {
        void Insert(KategoriMitraPenelitian KategoriMitraPenelitian);
        Task<KategoriMitraPenelitian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KategoriMitraPenelitian KategoriMitraPenelitian);
    }
}
