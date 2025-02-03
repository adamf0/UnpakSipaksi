using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Infrastructure.Database;

namespace UnpakSipaksi.Modules.FokusPengabdian.Infrastructure.FokusPengabdian
{
    internal sealed class FokusPengabdianRepository(FokusPengabdianDbContext context) : IFokusPengabdianRepository
    {
        public async Task<Domain.FokusPengabdian.FokusPengabdian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.FokusPengabdian.FokusPengabdian fokusPengabdian = await context.FokusPengabdian.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return fokusPengabdian;
        }

        public async Task DeleteAsync(Domain.FokusPengabdian.FokusPengabdian fokusPengabdian)
        {
            context.FokusPengabdian.Remove(fokusPengabdian);
        }

        public void Insert(Domain.FokusPengabdian.FokusPengabdian fokusPengabdian)
        {
            context.FokusPengabdian.Add(fokusPengabdian);
        }
    }
}
