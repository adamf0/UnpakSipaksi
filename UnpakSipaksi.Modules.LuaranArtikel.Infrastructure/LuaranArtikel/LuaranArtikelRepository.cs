using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel;
using UnpakSipaksi.Modules.LuaranArtikel.Infrastructure.Database;

namespace UnpakSipaksi.Modules.LuaranArtikel.Infrastructure.LuaranArtikel
{
    internal sealed class LuaranArtikelRepository(LuaranArtikelDbContext context) : ILuaranArtikelRepository
    {
        public async Task<Domain.LuaranArtikel.LuaranArtikel> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.LuaranArtikel.LuaranArtikel LuaranArtikel = await context.LuaranArtikel.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return LuaranArtikel;
        }

        public async Task DeleteAsync(Domain.LuaranArtikel.LuaranArtikel LuaranArtikel)
        {
            context.LuaranArtikel.Remove(LuaranArtikel);
        }

        public void Insert(Domain.LuaranArtikel.LuaranArtikel LuaranArtikel)
        {
            context.LuaranArtikel.Add(LuaranArtikel);
        }
    }
}
