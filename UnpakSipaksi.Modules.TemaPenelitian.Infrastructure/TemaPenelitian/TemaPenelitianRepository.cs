using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.TemaPenelitian.Domain.TemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Infrastructure.Database;

namespace UnpakSipaksi.Modules.TemaPenelitian.Infrastructure.TemaPenelitian
{
    internal sealed class TemaPenelitianRepository(TemaPenelitianDbContext context) : ITemaPenelitianRepository
    {
        public async Task<Domain.TemaPenelitian.TemaPenelitian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.TemaPenelitian.TemaPenelitian temaPenelitian = await context.TemaPenelitian.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return temaPenelitian;
        }

        public async Task DeleteAsync(Domain.TemaPenelitian.TemaPenelitian temaPenelitian)
        {
            context.TemaPenelitian.Remove(temaPenelitian);
        }

        public void Insert(Domain.TemaPenelitian.TemaPenelitian temaPenelitian)
        {
            context.TemaPenelitian.Add(temaPenelitian);
        }
    }
}
