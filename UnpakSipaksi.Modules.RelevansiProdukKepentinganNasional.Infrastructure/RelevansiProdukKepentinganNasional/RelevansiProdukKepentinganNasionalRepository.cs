using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Infrastructure.Database;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Infrastructure.RelevansiProdukKepentinganNasional
{
    internal sealed class RelevansiProdukKepentinganNasionalRepository(RelevansiProdukKepentinganNasionalDbContext context) : IRelevansiProdukKepentinganNasionalRepository
    {
        public async Task<Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional RelevansiProdukKepentinganNasional = await context.RelevansiProdukKepentinganNasional.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return RelevansiProdukKepentinganNasional;
        }

        public async Task DeleteAsync(Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional RelevansiProdukKepentinganNasional)
        {
            context.RelevansiProdukKepentinganNasional.Remove(RelevansiProdukKepentinganNasional);
        }

        public void Insert(Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional RelevansiProdukKepentinganNasional)
        {
            context.RelevansiProdukKepentinganNasional.Add(RelevansiProdukKepentinganNasional);
        }
    }
}
