using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.TopikPenelitian.Domain.TopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.Infrastructure.Database;

namespace UnpakSipaksi.Modules.TopikPenelitian.Infrastructure.TopikPenelitian
{
    internal sealed class TopikPenelitianRepository(TopikPenelitianDbContext context) : ITopikPenelitianRepository
    {
        public async Task<Domain.TopikPenelitian.TopikPenelitian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.TopikPenelitian.TopikPenelitian temaPenelitian = await context.TopikPenelitian.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return temaPenelitian;
        }

        public async Task DeleteAsync(Domain.TopikPenelitian.TopikPenelitian temaPenelitian)
        {
            context.TopikPenelitian.Remove(temaPenelitian);
        }

        public void Insert(Domain.TopikPenelitian.TopikPenelitian temaPenelitian)
        {
            context.TopikPenelitian.Add(temaPenelitian);
        }
    }
}
