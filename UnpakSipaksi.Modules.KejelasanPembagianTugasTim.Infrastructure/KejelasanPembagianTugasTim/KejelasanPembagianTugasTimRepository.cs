using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Infrastructure.KejelasanPembagianTugasTim
{
    internal sealed class KejelasanPembagianTugasTimRepository(KejelasanPembagianTugasTimDbContext context) : IKejelasanPembagianTugasTimRepository
    {
        public async Task<Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim kejelasanPembagianTugasTim = await context.KejelasanPembagianTugasTim.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return kejelasanPembagianTugasTim;
        }

        public async Task DeleteAsync(Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim kejelasanPembagianTugasTim)
        {
            context.KejelasanPembagianTugasTim.Remove(kejelasanPembagianTugasTim);
        }

        public void Insert(Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim kejelasanPembagianTugasTim)
        {
            context.KejelasanPembagianTugasTim.Add(kejelasanPembagianTugasTim);
        }
    }
}
