using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KategoriTkt.Infrastructure.KategoriTkt
{
    internal sealed class KategoriTktRepository(KategoriTktDbContext context) : IKategoriTktRepository
    {
        public async Task<Domain.KategoriTkt.KategoriTkt> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KategoriTkt.KategoriTkt kategoriTkt = await context.KategoriTkt.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return kategoriTkt;
        }

        public async Task DeleteAsync(Domain.KategoriTkt.KategoriTkt kategoriTkt)
        {
            context.KategoriTkt.Remove(kategoriTkt);
        }

        public void Insert(Domain.KategoriTkt.KategoriTkt kategoriTkt)
        {
            context.KategoriTkt.Add(kategoriTkt);
        }
    }
}
