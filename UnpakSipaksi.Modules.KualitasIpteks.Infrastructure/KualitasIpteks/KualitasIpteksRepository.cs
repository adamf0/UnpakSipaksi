using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks;
using UnpakSipaksi.Modules.KualitasIpteks.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KualitasIpteks.Infrastructure.KualitasIpteks
{
    internal sealed class KualitasIpteksRepository(KualitasIpteksDbContext context) : IKualitasIpteksRepository
    {
        public async Task<Domain.KualitasIpteks.KualitasIpteks> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KualitasIpteks.KualitasIpteks KualitasIpteks = await context.KualitasIpteks.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return KualitasIpteks;
        }

        public async Task DeleteAsync(Domain.KualitasIpteks.KualitasIpteks KualitasIpteks)
        {
            context.KualitasIpteks.Remove(KualitasIpteks);
        }

        public void Insert(Domain.KualitasIpteks.KualitasIpteks KualitasIpteks)
        {
            context.KualitasIpteks.Add(KualitasIpteks);
        }
    }
}
