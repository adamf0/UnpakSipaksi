using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Satuan.Domain.Satuan
{
    public interface ISatuanRepository
    {
        void Insert(Satuan Satuan);
        Task<Satuan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Satuan Satuan);
    }
}
