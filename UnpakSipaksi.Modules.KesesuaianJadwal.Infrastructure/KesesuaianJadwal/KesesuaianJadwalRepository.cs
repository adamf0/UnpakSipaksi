using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Infrastructure.KesesuaianJadwal
{
    internal sealed class KesesuaianJadwalRepository(KesesuaianJadwalDbContext context) : IKesesuaianJadwalRepository
    {
        public async Task<Domain.KesesuaianJadwal.KesesuaianJadwal> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KesesuaianJadwal.KesesuaianJadwal kesesuaianJadwal = await context.KesesuaianJadwal.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return kesesuaianJadwal;
        }

        public async Task DeleteAsync(Domain.KesesuaianJadwal.KesesuaianJadwal kesesuaianJadwal)
        {
            context.KesesuaianJadwal.Remove(kesesuaianJadwal);
        }

        public void Insert(Domain.KesesuaianJadwal.KesesuaianJadwal kesesuaianJadwal)
        {
            context.KesesuaianJadwal.Add(kesesuaianJadwal);
        }
    }
}
