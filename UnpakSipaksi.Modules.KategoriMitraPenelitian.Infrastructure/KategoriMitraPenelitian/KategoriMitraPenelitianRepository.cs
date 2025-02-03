using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Domain.KategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Infrastructure.KategoriMitraPenelitian
{
    internal sealed class KategoriMitraPenelitianRepository(KategoriMitraPenelitianDbContext context) : IKategoriMitraPenelitianRepository
    {
        public async Task<Domain.KategoriMitraPenelitian.KategoriMitraPenelitian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KategoriMitraPenelitian.KategoriMitraPenelitian kategoriMitraPenelitian = await context.KategoriMitraPenelitian.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return kategoriMitraPenelitian;
        }

        public async Task DeleteAsync(Domain.KategoriMitraPenelitian.KategoriMitraPenelitian kategoriMitraPenelitian)
        {
            context.KategoriMitraPenelitian.Remove(kategoriMitraPenelitian);
        }

        public void Insert(Domain.KategoriMitraPenelitian.KategoriMitraPenelitian kategoriMitraPenelitian)
        {
            context.KategoriMitraPenelitian.Add(kategoriMitraPenelitian);
        }
    }
}
