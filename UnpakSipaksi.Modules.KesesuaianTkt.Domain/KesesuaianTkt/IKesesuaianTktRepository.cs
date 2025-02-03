using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt
{
    public interface IKesesuaianTktRepository
    {
        void Insert(KesesuaianTkt KesesuaianTkt);
        Task<KesesuaianTkt> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KesesuaianTkt KesesuaianTkt);
    }
}
