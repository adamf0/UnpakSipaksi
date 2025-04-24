using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema;
using UnpakSipaksi.Modules.KategoriSkema.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KategoriSkema.Infrastructure.KategoriSkema
{
    internal sealed class KategoriSkemaRepository(KategoriSkemaDbContext context) : IKategoriSkemaRepository
    {
        public async Task<Domain.KategoriSkema.KategoriSkema> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KategoriSkema.KategoriSkema KategoriSkema = await context.KategoriSkema.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return KategoriSkema;
        }

        public async Task DeleteAsync(Domain.KategoriSkema.KategoriSkema KategoriSkema)
        {
            context.KategoriSkema.Remove(KategoriSkema);
        }

        public void Insert(Domain.KategoriSkema.KategoriSkema KategoriSkema)
        {
            context.KategoriSkema.Add(KategoriSkema);
        }
    }
}
