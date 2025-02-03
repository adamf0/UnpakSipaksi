using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Infrastructure.Database;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Infrastructure.InovasiPemecahanMasalah
{
    internal sealed class InovasiPemecahanMasalahRepository(InovasiPemecahanMasalahDbContext context) : IInovasiPemecahanMasalahRepository
    {
        public async Task<Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah inovasiPemecahanMasalah = await context.InovasiPemecahanMasalah.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return inovasiPemecahanMasalah;
        }

        public async Task DeleteAsync(Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah inovasiPemecahanMasalah)
        {
            context.InovasiPemecahanMasalah.Remove(inovasiPemecahanMasalah);
        }

        public void Insert(Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah inovasiPemecahanMasalah)
        {
            context.InovasiPemecahanMasalah.Add(inovasiPemecahanMasalah);
        }
    }
}
