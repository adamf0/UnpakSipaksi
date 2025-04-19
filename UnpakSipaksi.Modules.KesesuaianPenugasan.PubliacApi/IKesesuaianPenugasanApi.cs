using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.PubliacApi
{
    public interface IKesesuaianPenugasanApi
    {
        Task<KesesuaianPenugasanResponse?> GetAsync(Guid KesesuaianPenugasanUuid, CancellationToken cancellationToken = default);
    }

}
