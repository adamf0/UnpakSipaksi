using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Infrastructure.KesesuaianTkt
{
    internal sealed class KesesuaianTktRepository(KesesuaianTktDbContext context) : IKesesuaianTktRepository
    {
        public async Task<Domain.KesesuaianTkt.KesesuaianTkt> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KesesuaianTkt.KesesuaianTkt kesesuaianTkt = await context.KesesuaianTkt.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return kesesuaianTkt;
        }

        public async Task DeleteAsync(Domain.KesesuaianTkt.KesesuaianTkt kesesuaianTkt)
        {
            context.KesesuaianTkt.Remove(kesesuaianTkt);
        }

        public void Insert(Domain.KesesuaianTkt.KesesuaianTkt kesesuaianTkt)
        {
            context.KesesuaianTkt.Add(kesesuaianTkt);
        }
    }
}
