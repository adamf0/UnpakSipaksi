using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.JenisLuaran.Domain
{
    public interface IJenisLuaranRepository
    {
        void Insert(JenisLuaran JenisLuaran);
        Task<JenisLuaran> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(JenisLuaran JenisLuaran);
    }
}
