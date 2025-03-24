using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Rirn.Domain.Rirn;
using UnpakSipaksi.Modules.Rirn.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Rirn.Infrastructure.Rirn
{
    internal sealed class RirnRepository(RirnDbContext context) : IRirnRepository
    {
        public async Task<Domain.Rirn.Rirn> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Rirn.Rirn temaPenelitian = await context.Rirn.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return temaPenelitian;
        }

        public async Task DeleteAsync(Domain.Rirn.Rirn temaPenelitian)
        {
            context.Rirn.Remove(temaPenelitian);
        }

        public void Insert(Domain.Rirn.Rirn temaPenelitian)
        {
            context.Rirn.Add(temaPenelitian);
        }
    }
}
