using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.UrgensiPenelitian.Domain.UrgensiPenelitian;
using UnpakSipaksi.Modules.UrgensiPenelitian.Infrastructure.Database;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Infrastructure.UrgensiPenelitian
{
    internal sealed class UrgensiPenelitianRepository(UrgensiPenelitianDbContext context) : IUrgensiPenelitianRepository
    {
        public async Task<Domain.UrgensiPenelitian.UrgensiPenelitian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.UrgensiPenelitian.UrgensiPenelitian UrgensiPenelitian = await context.UrgensiPenelitian.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return UrgensiPenelitian;
        }

        public async Task DeleteAsync(Domain.UrgensiPenelitian.UrgensiPenelitian UrgensiPenelitian)
        {
            context.UrgensiPenelitian.Remove(UrgensiPenelitian);
        }

        public void Insert(Domain.UrgensiPenelitian.UrgensiPenelitian UrgensiPenelitian)
        {
            context.UrgensiPenelitian.Add(UrgensiPenelitian);
        }
    }
}
