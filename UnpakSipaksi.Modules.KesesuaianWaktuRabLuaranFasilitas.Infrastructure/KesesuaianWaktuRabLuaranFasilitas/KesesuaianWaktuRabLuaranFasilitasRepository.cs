using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Infrastructure.KesesuaianWaktuRabLuaranFasilitas
{
    internal sealed class KesesuaianWaktuRabLuaranFasilitasRepository(KesesuaianWaktuRabLuaranFasilitasDbContext context) : IKesesuaianWaktuRabLuaranFasilitasRepository
    {
        public async Task<Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas kesesuaianWaktuRabLuaranFasilitas = await context.KesesuaianWaktuRabLuaranFasilitas.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return kesesuaianWaktuRabLuaranFasilitas;
        }

        public async Task DeleteAsync(Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas kesesuaianWaktuRabLuaranFasilitas)
        {
            context.KesesuaianWaktuRabLuaranFasilitas.Remove(kesesuaianWaktuRabLuaranFasilitas);
        }

        public void Insert(Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas kesesuaianWaktuRabLuaranFasilitas)
        {
            context.KesesuaianWaktuRabLuaranFasilitas.Add(kesesuaianWaktuRabLuaranFasilitas);
        }
    }
}
