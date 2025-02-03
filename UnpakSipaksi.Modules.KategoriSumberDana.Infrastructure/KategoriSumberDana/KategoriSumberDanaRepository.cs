using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana;
using UnpakSipaksi.Modules.KategoriSumberDana.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Infrastructure.KategoriSumberDana
{
    internal sealed class KategoriSumberDanaRepository(KategoriSumberDanaDbContext context) : IKategoriSumberDanaRepository
    {
        public async Task<Domain.KategoriSumberDana.KategoriSumberDana> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KategoriSumberDana.KategoriSumberDana kategoriSumberDana = await context.KategoriSumberDana.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return kategoriSumberDana;
        }

        public async Task DeleteAsync(Domain.KategoriSumberDana.KategoriSumberDana kategoriSumberDana)
        {
            context.KategoriSumberDana.Remove(kategoriSumberDana);
        }

        public void Insert(Domain.KategoriSumberDana.KategoriSumberDana kategoriSumberDana)
        {
            context.KategoriSumberDana.Add(kategoriSumberDana);
        }
    }
}
