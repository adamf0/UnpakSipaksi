using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Infrastructure.Database;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Infrastructure.RumpunIlmu2
{
    internal sealed class RumpunIlmu2Repository(RumpunIlmu2DbContext context) : IRumpunIlmu2Repository
    {
        public async Task<Domain.RumpunIlmu2.RumpunIlmu2> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.RumpunIlmu2.RumpunIlmu2 temaPenelitian = await context.RumpunIlmu2.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return temaPenelitian;
        }

        public async Task DeleteAsync(Domain.RumpunIlmu2.RumpunIlmu2 temaPenelitian)
        {
            context.RumpunIlmu2.Remove(temaPenelitian);
        }

        public void Insert(Domain.RumpunIlmu2.RumpunIlmu2 temaPenelitian)
        {
            context.RumpunIlmu2.Add(temaPenelitian);
        }
    }
}
