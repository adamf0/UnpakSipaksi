using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Kategori.Domain.Kategori;
using UnpakSipaksi.Modules.Kategori.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Kategori.Infrastructure.Kategori
{
    internal sealed class KategoriRepository(KategoriDbContext context) : IKategoriRepository
    {
        public async Task<Domain.Kategori.Kategori> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Kategori.Kategori akurasiPenelitian = await context.Kategori.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.Kategori.Kategori akurasiPenelitian)
        {
            context.Kategori.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.Kategori.Kategori akurasiPenelitian)
        {
            context.Kategori.Add(akurasiPenelitian);
        }
    }
}
