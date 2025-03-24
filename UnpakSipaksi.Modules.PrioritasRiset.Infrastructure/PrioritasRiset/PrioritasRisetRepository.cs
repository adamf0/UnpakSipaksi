using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.PrioritasRiset.Infrastructure.Database;
using UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.Infrastructure.PrioritasRiset
{
    internal sealed class PrioritasRisetRepository(PrioritasRisetDbContext context) : IPrioritasRisetRepository
    {
        public async Task<Domain.PrioritasRiset.PrioritasRiset> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.PrioritasRiset.PrioritasRiset PrioritasRiset = await context.PrioritasRiset.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return PrioritasRiset;
        }

        public async Task DeleteAsync(Domain.PrioritasRiset.PrioritasRiset PrioritasRiset)
        {
            context.PrioritasRiset.Remove(PrioritasRiset);
        }

        public void Insert(Domain.PrioritasRiset.PrioritasRiset PrioritasRiset)
        {
            context.PrioritasRiset.Add(PrioritasRiset);
        }
    }
}
