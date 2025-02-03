using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Infrastructure.KebaruanReferensi
{
    internal sealed class KebaruanReferensiRepository(KebaruanReferensiDbContext context) : IKebaruanReferensiRepository
    {
        public async Task<Domain.KebaruanReferensi.KebaruanReferensi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KebaruanReferensi.KebaruanReferensi kebaruanReferensi = await context.KebaruanReferensi.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return kebaruanReferensi;
        }

        public async Task DeleteAsync(Domain.KebaruanReferensi.KebaruanReferensi kebaruanReferensi)
        {
            context.KebaruanReferensi.Remove(kebaruanReferensi);
        }

        public void Insert(Domain.KebaruanReferensi.KebaruanReferensi kebaruanReferensi)
        {
            context.KebaruanReferensi.Add(kebaruanReferensi);
        }
    }
}
