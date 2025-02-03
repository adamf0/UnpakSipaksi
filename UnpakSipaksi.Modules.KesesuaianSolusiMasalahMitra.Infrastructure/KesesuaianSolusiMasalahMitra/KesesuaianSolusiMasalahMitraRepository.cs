using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Infrastructure.KesesuaianSolusiMasalahMitra
{
    internal sealed class KesesuaianSolusiMasalahMitraRepository(KesesuaianSolusiMasalahMitraDbContext context) : IKesesuaianSolusiMasalahMitraRepository
    {
        public async Task<Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra kesesuaianSolusiMasalahMitra = await context.KesesuaianSolusiMasalahMitra.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return kesesuaianSolusiMasalahMitra;
        }

        public async Task DeleteAsync(Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra kesesuaianSolusiMasalahMitra)
        {
            context.KesesuaianSolusiMasalahMitra.Remove(kesesuaianSolusiMasalahMitra);
        }

        public void Insert(Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra kesesuaianSolusiMasalahMitra)
        {
            context.KesesuaianSolusiMasalahMitra.Add(kesesuaianSolusiMasalahMitra);
        }
    }
}
