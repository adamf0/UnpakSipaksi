using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.RAB;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.RAB
{
    internal sealed class RABRepository(RABDbContext context, PenelitianHibahDbContext contextPenelitianHibah) : IRABRepository
    {
        public async Task<Domain.RAB.RAB> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.RAB.RAB rab = await context.RAB.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return rab;
        }

        public async Task DeleteAsync(Domain.RAB.RAB rab)
        {
            context.RAB.Remove(rab);
        }

        public void Insert(Domain.RAB.RAB rab)
        {
            context.RAB.Add(rab);
        }
    }
}
