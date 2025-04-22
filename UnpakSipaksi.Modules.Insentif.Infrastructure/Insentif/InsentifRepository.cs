using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.Insentif.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Insentif.Infrastructure.Insentif
{
    internal sealed class InsentifRepository(InsentifDbContext context) : IInsentifRepository
    {
        public async Task<Domain.Insentif.Insentif> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Insentif.Insentif akurasiPenelitian = await context.Insentif.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.Insentif.Insentif akurasiPenelitian)
        {
            context.Insentif.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.Insentif.Insentif akurasiPenelitian)
        {
            context.Insentif.Add(akurasiPenelitian);
        }
    }
}
