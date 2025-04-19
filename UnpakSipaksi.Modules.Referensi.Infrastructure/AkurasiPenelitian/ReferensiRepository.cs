using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Referensi.Domain.Referensi;
using UnpakSipaksi.Modules.Referensi.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Referensi.Infrastructure.Referensi
{
    internal sealed class ReferensiRepository(ReferensiDbContext context) : IReferensiRepository
    {
        public async Task<Domain.Referensi.Referensi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Referensi.Referensi Referensi = await context.Referensi.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return Referensi;
        }

        public async Task DeleteAsync(Domain.Referensi.Referensi Referensi)
        {
            context.Referensi.Remove(Referensi);
        }

        public void Insert(Domain.Referensi.Referensi Referensi)
        {
            context.Referensi.Add(Referensi);
        }
    }
}
