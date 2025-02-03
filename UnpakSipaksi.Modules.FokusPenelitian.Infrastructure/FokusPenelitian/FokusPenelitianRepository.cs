using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Infrastructure.Database;

namespace UnpakSipaksi.Modules.FokusPenelitian.Infrastructure.FokusPenelitian
{
    internal sealed class FokusPenelitianRepository(FokusPenelitianDbContext context) : IFokusPenelitianRepository
    {
        public async Task<Domain.FokusPenelitian.FokusPenelitian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.FokusPenelitian.FokusPenelitian fokusPenelitian = await context.FokusPenelitian.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return fokusPenelitian;
        }

        public async Task DeleteAsync(Domain.FokusPenelitian.FokusPenelitian fokusPenelitian)
        {
            context.FokusPenelitian.Remove(fokusPenelitian);
        }

        public void Insert(Domain.FokusPenelitian.FokusPenelitian fokusPenelitian)
        {
            context.FokusPenelitian.Add(fokusPenelitian);
        }
    }
}
