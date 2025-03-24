using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Infrastructure.Database;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Infrastructure.RumpunIlmu1
{
    internal sealed class RumpunIlmu1Repository(RumpunIlmu1DbContext context) : IRumpunIlmu1Repository
    {
        public async Task<Domain.RumpunIlmu1.RumpunIlmu1> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.RumpunIlmu1.RumpunIlmu1 temaPenelitian = await context.RumpunIlmu1.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return temaPenelitian;
        }

        public async Task DeleteAsync(Domain.RumpunIlmu1.RumpunIlmu1 temaPenelitian)
        {
            context.RumpunIlmu1.Remove(temaPenelitian);
        }

        public void Insert(Domain.RumpunIlmu1.RumpunIlmu1 temaPenelitian)
        {
            context.RumpunIlmu1.Add(temaPenelitian);
        }
    }
}
