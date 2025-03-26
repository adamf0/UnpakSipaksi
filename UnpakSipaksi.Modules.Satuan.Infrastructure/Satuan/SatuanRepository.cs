using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Satuan.Domain.Satuan;
using UnpakSipaksi.Modules.Satuan.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Satuan.Infrastructure.Satuan
{
    internal sealed class SatuanRepository(SatuanDbContext context) : ISatuanRepository
    {
        public async Task<Domain.Satuan.Satuan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Satuan.Satuan temaPenelitian = await context.Satuan.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return temaPenelitian;
        }

        public async Task DeleteAsync(Domain.Satuan.Satuan temaPenelitian)
        {
            context.Satuan.Remove(temaPenelitian);
        }

        public void Insert(Domain.Satuan.Satuan temaPenelitian)
        {
            context.Satuan.Add(temaPenelitian);
        }
    }
}
