using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Komponen.Domain.Komponen;
using UnpakSipaksi.Modules.Komponen.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Komponen.Infrastructure.Komponen
{
    internal sealed class KomponenRepository(KomponenContext context) : IKomponenRepository
    {
        public async Task<Domain.Komponen.Komponen> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Komponen.Komponen Komponen = await context.Komponen.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return Komponen;
        }

        public async Task DeleteAsync(Domain.Komponen.Komponen Komponen)
        {
            context.Komponen.Remove(Komponen);
        }

        public void Insert(Domain.Komponen.Komponen Komponen)
        {
            context.Komponen.Add(Komponen);
        }
    }
}
