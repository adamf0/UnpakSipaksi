using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Infrastructure.KetajamanPerumusanMasalah
{
    internal sealed class KetajamanPerumusanMasalahRepository(KetajamanPerumusanMasalahDbContext context) : IKetajamanPerumusanMasalahRepository
    {
        public async Task<Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah KetajamanPerumusanMasalah = await context.KetajamanPerumusanMasalah.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return KetajamanPerumusanMasalah;
        }

        public async Task DeleteAsync(Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah KetajamanPerumusanMasalah)
        {
            context.KetajamanPerumusanMasalah.Remove(KetajamanPerumusanMasalah);
        }

        public void Insert(Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah KetajamanPerumusanMasalah)
        {
            context.KetajamanPerumusanMasalah.Add(KetajamanPerumusanMasalah);
        }
    }
}
