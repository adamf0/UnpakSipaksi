using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Infrastructure.Database;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Infrastructure.ModelFeasibilityStudys
{
    internal sealed class ModelFeasibilityStudysRepository(ModelFeasibilityStudysDbContext context) : IModelFeasibilityStudysRepository
    {
        public async Task<Domain.ModelFeasibilityStudys.ModelFeasibilityStudys> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.ModelFeasibilityStudys.ModelFeasibilityStudys ModelFeasibilityStudys = await context.ModelFeasibilityStudys.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return ModelFeasibilityStudys;
        }

        public async Task DeleteAsync(Domain.ModelFeasibilityStudys.ModelFeasibilityStudys ModelFeasibilityStudys)
        {
            context.ModelFeasibilityStudys.Remove(ModelFeasibilityStudys);
        }

        public void Insert(Domain.ModelFeasibilityStudys.ModelFeasibilityStudys ModelFeasibilityStudys)
        {
            context.ModelFeasibilityStudys.Add(ModelFeasibilityStudys);
        }
    }
}
