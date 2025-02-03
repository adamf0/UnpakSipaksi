using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan
{
    public interface IKesesuaianPenugasanRepository
    {
        void Insert(KesesuaianPenugasan KesesuaianPenugasan);
        Task<KesesuaianPenugasan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KesesuaianPenugasan KesesuaianPenugasan);
    }
}
