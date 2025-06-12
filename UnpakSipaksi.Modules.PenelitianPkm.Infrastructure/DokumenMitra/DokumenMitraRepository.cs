using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenMitra;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.DokumenMitra
{
    internal sealed class DokumenMitraRepository(DokumenMitraDbContext context, PenelitianPkmDbContext contextPenelitianPkm) : IDokumenMitraRepository
    {
        public async Task<Domain.DokumenMitra.DokumenMitra> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.DokumenMitra.DokumenMitra DokumenMitra = await context.DokumenMitra.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return DokumenMitra;
        }

        public async Task DeleteAsync(Domain.DokumenMitra.DokumenMitra DokumenMitra)
        {
            context.DokumenMitra.Remove(DokumenMitra);
        }

        public void Insert(Domain.DokumenMitra.DokumenMitra DokumenMitra)
        {
            context.DokumenMitra.Add(DokumenMitra);
        }
    }
}
