using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Roadmap.Domain.Roadmap;
using UnpakSipaksi.Modules.Roadmap.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Roadmap.Infrastructure.Roadmap
{
    internal sealed class RoadmapRepository(RoadmapDbContext context) : IRoadmapRepository
    {
        public async Task<Domain.Roadmap.Roadmap> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Roadmap.Roadmap Roadmap = await context.Roadmap.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return Roadmap;
        }

        public async Task DeleteAsync(Domain.Roadmap.Roadmap Roadmap)
        {
            context.Roadmap.Remove(Roadmap);
        }

        public void Insert(Domain.Roadmap.Roadmap Roadmap)
        {
            context.Roadmap.Add(Roadmap);
        }
    }
}
