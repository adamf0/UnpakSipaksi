using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Substansi.Domain.SubstansiInternal;
using UnpakSipaksi.Modules.Substansi.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Substansi.Infrastructure.SubstansiInternal
{
    internal sealed class SubstansiInternalRepository(SubstansiInternalDbContext context) : ISubstansiInternalRepository
    {
        public async Task<Domain.SubstansiInternal.SubstansiInternal> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.SubstansiInternal.SubstansiInternal Substansi = await context.Substansi.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return Substansi;
        }

        public async Task DeleteAsync(Domain.SubstansiInternal.SubstansiInternal Substansi)
        {
            context.Substansi.Remove(Substansi);
        }

        public void Insert(Domain.SubstansiInternal.SubstansiInternal Substansi)
        {
            context.Substansi.Add(Substansi);
        }
    }
}
