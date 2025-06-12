using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenLainnya;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.DokumenLainnya
{
    internal sealed class DokumenLainnyaRepository(DokumenLainnyaDbContext context, PenelitianPkmDbContext contextPenelitianPkm) : IDokumenLainnyaRepository
    {
        public async Task<Domain.DokumenLainnya.DokumenLainnya> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.DokumenLainnya.DokumenLainnya DokumenLainnya = await context.DokumenLainnya.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return DokumenLainnya;
        }

        public async Task DeleteAsync(Domain.DokumenLainnya.DokumenLainnya DokumenLainnya)
        {
            context.DokumenLainnya.Remove(DokumenLainnya);
        }

        public void Insert(Domain.DokumenLainnya.DokumenLainnya DokumenLainnya)
        {
            context.DokumenLainnya.Add(DokumenLainnya);
        }
    }
}
