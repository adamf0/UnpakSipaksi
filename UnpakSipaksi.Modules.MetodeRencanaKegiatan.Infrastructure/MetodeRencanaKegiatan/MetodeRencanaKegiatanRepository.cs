using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Infrastructure.Database;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Infrastructure.MetodeRencanaKegiatan
{
    internal sealed class MetodeRencanaKegiatanRepository(MetodeRencanaKegiatanDbContext context) : IMetodeRencanaKegiatanRepository
    {
        public async Task<Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan MetodeRencanaKegiatan = await context.MetodeRencanaKegiatan.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return MetodeRencanaKegiatan;
        }

        public async Task DeleteAsync(Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan MetodeRencanaKegiatan)
        {
            context.MetodeRencanaKegiatan.Remove(MetodeRencanaKegiatan);
        }

        public void Insert(Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan MetodeRencanaKegiatan)
        {
            context.MetodeRencanaKegiatan.Add(MetodeRencanaKegiatan);
        }
    }
}
