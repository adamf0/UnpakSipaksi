using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenPendukung;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.DokumenPendukung
{
    internal sealed class DokumenPendukungRepository(DokumenPendukungDbContext context, PenelitianHibahDbContext contextPenelitianHibah) : IDokumenPendukungRepository
    {
        public async Task<Domain.DokumenPendukung.DokumenPendukung> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.DokumenPendukung.DokumenPendukung DokumenPendukung = await context.DokumenPendukung.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return DokumenPendukung;
        }

        public async Task DeleteAsync(Domain.DokumenPendukung.DokumenPendukung DokumenPendukung)
        {
            context.DokumenPendukung.Remove(DokumenPendukung);
        }

        public void Insert(Domain.DokumenPendukung.DokumenPendukung DokumenPendukung)
        {
            context.DokumenPendukung.Add(DokumenPendukung);
        }
    }
}
