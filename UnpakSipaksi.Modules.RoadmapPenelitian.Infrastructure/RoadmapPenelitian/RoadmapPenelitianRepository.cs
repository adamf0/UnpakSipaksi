using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.Infrastructure.Database;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Infrastructure.RoadmapPenelitian
{
    internal sealed class RoadmapPenelitianRepository(RoadmapPenelitianDbContext context) : IRoadmapPenelitianRepository
    {
        public async Task<Domain.RoadmapPenelitian.RoadmapPenelitian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.RoadmapPenelitian.RoadmapPenelitian RoadmapPenelitian = await context.RoadmapPenelitian.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return RoadmapPenelitian;
        }

        public async Task DeleteAsync(Domain.RoadmapPenelitian.RoadmapPenelitian RoadmapPenelitian)
        {
            context.RoadmapPenelitian.Remove(RoadmapPenelitian);
        }

        public void Insert(Domain.RoadmapPenelitian.RoadmapPenelitian RoadmapPenelitian)
        {
            context.RoadmapPenelitian.Add(RoadmapPenelitian);
        }
    }
}
