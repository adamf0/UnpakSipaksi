using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Infrastructure.Database;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Infrastructure.ArtikelMediaMassa
{
    internal sealed class ArtikelMediaMassaRepository(ArtikelMediaMassaDbContext context) : IArtikelMediaMassaRepository
    {
        public async Task<Domain.ArtikelMediaMassa.ArtikelMediaMassa> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.ArtikelMediaMassa.ArtikelMediaMassa akurasiPenelitian = await context.ArtikelMediaMassa.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.ArtikelMediaMassa.ArtikelMediaMassa akurasiPenelitian)
        {
            context.ArtikelMediaMassa.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.ArtikelMediaMassa.ArtikelMediaMassa akurasiPenelitian)
        {
            context.ArtikelMediaMassa.Add(akurasiPenelitian);
        }
    }
}
