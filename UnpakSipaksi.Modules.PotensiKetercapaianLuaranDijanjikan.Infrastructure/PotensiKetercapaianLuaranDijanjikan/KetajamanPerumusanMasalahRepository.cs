using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Infrastructure.Database;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Infrastructure.PotensiKetercapaianLuaranDijanjikan
{
    internal sealed class PotensiKetercapaianLuaranDijanjikanRepository(PotensiKetercapaianLuaranDijanjikanDbContext context) : IPotensiKetercapaianLuaranDijanjikanRepository
    {
        public async Task<Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan PotensiKetercapaianLuaranDijanjikan = await context.PotensiKetercapaianLuaranDijanjikan.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return PotensiKetercapaianLuaranDijanjikan;
        }

        public async Task DeleteAsync(Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan PotensiKetercapaianLuaranDijanjikan)
        {
            context.PotensiKetercapaianLuaranDijanjikan.Remove(PotensiKetercapaianLuaranDijanjikan);
        }

        public void Insert(Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan PotensiKetercapaianLuaranDijanjikan)
        {
            context.PotensiKetercapaianLuaranDijanjikan.Add(PotensiKetercapaianLuaranDijanjikan);
        }
    }
}
