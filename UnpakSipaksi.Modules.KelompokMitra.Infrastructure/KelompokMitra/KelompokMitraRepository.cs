using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KelompokMitra.Infrastructure.Database;
using UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra;

namespace UnpakSipaksi.Modules.KelompokMitra.Infrastructure.KelompokMitra
{
    internal sealed class KelompokMitraRepository(KelompokMitraDbContext context) : IKelompokMitraRepository
    {
        public async Task<Domain.KelompokMitra.KelompokMitra> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KelompokMitra.KelompokMitra kelompokMitra = await context.KelompokMitra.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return kelompokMitra;
        }

        public async Task DeleteAsync(Domain.KelompokMitra.KelompokMitra kelompokMitra)
        {
            context.KelompokMitra.Remove(kelompokMitra);
        }

        public void Insert(Domain.KelompokMitra.KelompokMitra kelompokMitra)
        {
            context.KelompokMitra.Add(kelompokMitra);
        }
    }
}
