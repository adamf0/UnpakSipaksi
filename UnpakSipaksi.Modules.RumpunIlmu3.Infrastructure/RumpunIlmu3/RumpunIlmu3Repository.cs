using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Infrastructure.Database;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Infrastructure.RumpunIlmu3
{
    internal sealed class RumpunIlmu3Repository(RumpunIlmu3DbContext context) : IRumpunIlmu3Repository
    {
        public async Task<Domain.RumpunIlmu3.RumpunIlmu3> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.RumpunIlmu3.RumpunIlmu3 temaPenelitian = await context.RumpunIlmu3.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return temaPenelitian;
        }

        public async Task DeleteAsync(Domain.RumpunIlmu3.RumpunIlmu3 temaPenelitian)
        {
            context.RumpunIlmu3.Remove(temaPenelitian);
        }

        public void Insert(Domain.RumpunIlmu3.RumpunIlmu3 temaPenelitian)
        {
            context.RumpunIlmu3.Add(temaPenelitian);
        }
    }
}
