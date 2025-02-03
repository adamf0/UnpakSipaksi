using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Infrastructure.KesesuaianPenugasan
{
    internal sealed class KesesuaianPenugasanRepository(KesesuaianPenugasanDbContext context) : IKesesuaianPenugasanRepository
    {
        public async Task<Domain.KesesuaianPenugasan.KesesuaianPenugasan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KesesuaianPenugasan.KesesuaianPenugasan kesesuaianPenugasan = await context.KesesuaianPenugasan.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return kesesuaianPenugasan;
        }

        public async Task DeleteAsync(Domain.KesesuaianPenugasan.KesesuaianPenugasan kesesuaianPenugasan)
        {
            context.KesesuaianPenugasan.Remove(kesesuaianPenugasan);
        }

        public void Insert(Domain.KesesuaianPenugasan.KesesuaianPenugasan kesesuaianPenugasan)
        {
            context.KesesuaianPenugasan.Add(kesesuaianPenugasan);
        }
    }
}
