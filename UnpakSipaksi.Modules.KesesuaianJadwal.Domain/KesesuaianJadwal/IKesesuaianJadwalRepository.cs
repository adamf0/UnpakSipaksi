using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal
{
    public interface IKesesuaianJadwalRepository
    {
        void Insert(KesesuaianJadwal KesesuaianJadwal);
        Task<KesesuaianJadwal> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KesesuaianJadwal KesesuaianJadwal);
    }
}
