using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Infrastructure.Database;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Infrastructure.RelevansiKualitasReferensi
{
    internal sealed class RelevansiKualitasReferensiRepository(RelevansiKualitasReferensiDbContext context) : IRelevansiKualitasReferensiRepository
    {
        public async Task<Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi RelevansiKualitasReferensi = await context.RelevansiKualitasReferensi.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return RelevansiKualitasReferensi;
        }

        public async Task DeleteAsync(Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi RelevansiKualitasReferensi)
        {
            context.RelevansiKualitasReferensi.Remove(RelevansiKualitasReferensi);
        }

        public void Insert(Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi RelevansiKualitasReferensi)
        {
            context.RelevansiKualitasReferensi.Add(RelevansiKualitasReferensi);
        }
    }
}
