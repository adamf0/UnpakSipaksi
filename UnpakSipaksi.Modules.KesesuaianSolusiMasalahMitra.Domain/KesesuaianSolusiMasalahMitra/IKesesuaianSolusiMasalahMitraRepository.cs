using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra
{
    public interface IKesesuaianSolusiMasalahMitraRepository
    {
        void Insert(KesesuaianSolusiMasalahMitra KesesuaianSolusiMasalahMitra);
        Task<KesesuaianSolusiMasalahMitra> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KesesuaianSolusiMasalahMitra KesesuaianSolusiMasalahMitra);
    }
}
