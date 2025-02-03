using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Infrastructure.Database;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Infrastructure.AkurasiPenelitian
{
    internal sealed class AkurasiPenelitianRepository(AkurasiPenelitianDbContext context) : IAkurasiPenelitianRepository
    {
        public async Task<Domain.AkurasiPenelitian.AkurasiPenelitian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.AkurasiPenelitian.AkurasiPenelitian akurasiPenelitian = await context.AkurasiPenelitian.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.AkurasiPenelitian.AkurasiPenelitian akurasiPenelitian)
        {
            context.AkurasiPenelitian.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.AkurasiPenelitian.AkurasiPenelitian akurasiPenelitian)
        {
            context.AkurasiPenelitian.Add(akurasiPenelitian);
        }
    }
}
