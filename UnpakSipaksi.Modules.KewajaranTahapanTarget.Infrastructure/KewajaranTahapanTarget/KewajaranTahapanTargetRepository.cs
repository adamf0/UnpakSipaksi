using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Infrastructure.KewajaranTahapanTarget
{
    internal sealed class KewajaranTahapanTargetRepository(KewajaranTahapanTargetDbContext context) : IKewajaranTahapanTargetRepository
    {
        public async Task<Domain.KewajaranTahapanTarget.KewajaranTahapanTarget> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KewajaranTahapanTarget.KewajaranTahapanTarget KewajaranTahapanTarget = await context.KewajaranTahapanTarget.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return KewajaranTahapanTarget;
        }

        public async Task DeleteAsync(Domain.KewajaranTahapanTarget.KewajaranTahapanTarget KewajaranTahapanTarget)
        {
            context.KewajaranTahapanTarget.Remove(KewajaranTahapanTarget);
        }

        public void Insert(Domain.KewajaranTahapanTarget.KewajaranTahapanTarget KewajaranTahapanTarget)
        {
            context.KewajaranTahapanTarget.Add(KewajaranTahapanTarget);
        }
    }
}
