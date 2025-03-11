using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Infrastructure.KualitasKuantitasPublikasiProsiding
{
    internal sealed class KualitasKuantitasPublikasiProsidingRepository(KualitasKuantitasPublikasiProsidingDbContext context) : IKualitasKuantitasPublikasiProsidingRepository
    {
        public async Task<Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding KualitasKuantitasPublikasiProsiding = await context.KualitasKuantitasPublikasiProsiding.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return KualitasKuantitasPublikasiProsiding;
        }

        public async Task DeleteAsync(Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding KualitasKuantitasPublikasiProsiding)
        {
            context.KualitasKuantitasPublikasiProsiding.Remove(KualitasKuantitasPublikasiProsiding);
        }

        public void Insert(Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding KualitasKuantitasPublikasiProsiding)
        {
            context.KualitasKuantitasPublikasiProsiding.Add(KualitasKuantitasPublikasiProsiding);
        }
    }
}
