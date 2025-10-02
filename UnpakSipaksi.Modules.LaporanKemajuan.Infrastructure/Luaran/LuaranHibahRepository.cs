using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.LaporanKemajuan.Domain.Luaran;
using UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Database;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Luaran
{
    internal sealed class LuaranHibahRepository(LuaranHibahContext context) : ILuaranHibahRepository
    {
        public async Task<Domain.Luaran.Luaran> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Luaran.Luaran Luaran = await context.LuaranHibah.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return Luaran;
        }

        public async Task DeleteAsync(Domain.Luaran.Luaran Luaran)
        {
            context.LuaranHibah.Remove(Luaran);
        }

        public void Insert(Domain.Luaran.Luaran Luaran)
        {
            context.LuaranHibah.Add(Luaran);
        }
    }
}
