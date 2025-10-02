using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.LaporanKemajuan.Domain.Dokumen;
using UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Database;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Dokumen
{
    internal sealed class DokumenPkmRepository(DokumenPkmContext context) : IDokumenPkmRepository
    {
        public async Task<Domain.Dokumen.Dokumen> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Dokumen.Dokumen Dokumen = await context.DokumenPkm.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return Dokumen;
        }

        public async Task DeleteAsync(Domain.Dokumen.Dokumen Dokumen)
        {
            context.DokumenPkm.Remove(Dokumen);
        }

        public void Insert(Domain.Dokumen.Dokumen Dokumen)
        {
            context.DokumenPkm.Add(Dokumen);
        }
    }
}
