using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenKontrak;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.DokumenKontrak
{
    internal sealed class DokumenKontrakRepository(DokumenKontrakDbContext context, PenelitianHibahDbContext contextPenelitianHibah) : IDokumenKontrakRepository
    {
        public async Task<Domain.DokumenKontrak.DokumenKontrak> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.DokumenKontrak.DokumenKontrak DokumenKontrak = await context.DokumenKontrak.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return DokumenKontrak;
        }

        public async Task DeleteAsync(Domain.DokumenKontrak.DokumenKontrak DokumenKontrak)
        {
            context.DokumenKontrak.Remove(DokumenKontrak);
        }

        public void Insert(Domain.DokumenKontrak.DokumenKontrak DokumenKontrak)
        {
            context.DokumenKontrak.Add(DokumenKontrak);
        }
    }
}
