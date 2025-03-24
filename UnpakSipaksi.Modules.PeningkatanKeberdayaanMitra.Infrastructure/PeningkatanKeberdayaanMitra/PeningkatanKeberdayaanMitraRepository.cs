using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Infrastructure.Database;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Infrastructure.PeningkatanKeberdayaanMitra
{
    internal sealed class PeningkatanKeberdayaanMitraRepository(PeningkatanKeberdayaanMitraDbContext context) : IPeningkatanKeberdayaanMitraRepository
    {
        public async Task<Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra PeningkatanKeberdayaanMitra = await context.PeningkatanKeberdayaanMitra.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return PeningkatanKeberdayaanMitra;
        }

        public async Task DeleteAsync(Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra PeningkatanKeberdayaanMitra)
        {
            context.PeningkatanKeberdayaanMitra.Remove(PeningkatanKeberdayaanMitra);
        }

        public void Insert(Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra PeningkatanKeberdayaanMitra)
        {
            context.PeningkatanKeberdayaanMitra.Add(PeningkatanKeberdayaanMitra);
        }
    }
}
