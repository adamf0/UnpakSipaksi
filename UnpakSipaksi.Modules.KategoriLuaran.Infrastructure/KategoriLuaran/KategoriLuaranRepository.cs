using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori;
using UnpakSipaksi.Modules.KategoriLuaran.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KategoriLuaran.Infrastructure.KategoriLuaran
{
    internal sealed class KategoriLuaranRepository(KategoriLuaranDbContext context) : IKategoriLuaranRepository
    {
        public async Task<Domain.KategoriLuaran.KategoriLuaran> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KategoriLuaran.KategoriLuaran akurasiPenelitian = await context.KategoriLuaran.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.KategoriLuaran.KategoriLuaran akurasiPenelitian)
        {
            context.KategoriLuaran.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.KategoriLuaran.KategoriLuaran akurasiPenelitian)
        {
            context.KategoriLuaran.Add(akurasiPenelitian);
        }
    }
}
