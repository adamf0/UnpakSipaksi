using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman;
using UnpakSipaksi.Modules.Pengumuman.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Pengumuman.Infrastructure.Pengumuman
{
    internal sealed class PengumumanRepository(PengumumanDbContext context) : IPengumumanRepository
    {
        public async Task<Domain.Pengumuman.Pengumuman> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Pengumuman.Pengumuman Pengumuman = await context.Pengumuman.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return Pengumuman;
        }

        public async Task DeleteAsync(Domain.Pengumuman.Pengumuman Pengumuman)
        {
            context.Pengumuman.Remove(Pengumuman);
        }

        public void Insert(Domain.Pengumuman.Pengumuman Pengumuman)
        {
            context.Pengumuman.Add(Pengumuman);
        }
    }
}
