using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Metode.Infrastructure.Database;
using UnpakSipaksi.Modules.Metode.Domain.Metode;

namespace UnpakSipaksi.Modules.Metode.Infrastructure.Metode
{
    internal sealed class MetodeRepository(MetodeDbContext context) : IMetodeRepository
    {
        public async Task<Domain.Metode.Metode> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Metode.Metode Metode = await context.Metode.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return Metode;
        }

        public async Task DeleteAsync(Domain.Metode.Metode Metode)
        {
            context.Metode.Remove(Metode);
        }

        public void Insert(Domain.Metode.Metode Metode)
        {
            context.Metode.Add(Metode);
        }
    }
}
