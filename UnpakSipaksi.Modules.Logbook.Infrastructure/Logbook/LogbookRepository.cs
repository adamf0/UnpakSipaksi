using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Logbook.Domain.Logbook;
using UnpakSipaksi.Modules.Logbook.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Logbook.Infrastructure.Logbook
{
    internal sealed class LogbookRepository(LogbookDbContext context) : ILogbookRepository
    {
        public async Task<Domain.Logbook.Logbook> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Logbook.Logbook Logbook = await context.Logbook.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return Logbook;
        }

        public async Task<Domain.Logbook.Logbook> GetAsync(Guid Uuid, int idHibah, int idPkm, CancellationToken cancellationToken = default)
        {
            Domain.Logbook.Logbook Logbook = await context.Logbook.SingleOrDefaultAsync(e => ((e.Uuid == Uuid && e.PenelitianHibahId == idHibah) || (e.Uuid == Uuid && e.PenelitianPkmId == idPkm)), cancellationToken);
            return Logbook;
        }

        public async Task DeleteAsync(Domain.Logbook.Logbook Logbook)
        {
            context.Logbook.Remove(Logbook);
        }

        public void Insert(Domain.Logbook.Logbook Logbook)
        {
            context.Logbook.Add(Logbook);
        }
    }
}
