using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas
{
    public interface IKesesuaianWaktuRabLuaranFasilitasRepository
    {
        void Insert(KesesuaianWaktuRabLuaranFasilitas KesesuaianWaktuRabLuaranFasilitas);
        Task<KesesuaianWaktuRabLuaranFasilitas> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KesesuaianWaktuRabLuaranFasilitas KesesuaianWaktuRabLuaranFasilitas);
    }
}
