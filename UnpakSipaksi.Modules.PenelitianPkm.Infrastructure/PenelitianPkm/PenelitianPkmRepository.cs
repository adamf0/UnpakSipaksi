using Microsoft.EntityFrameworkCore;
using Minio.DataModel;
using System;
using FuzzySharp;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.PenelitianPkm
{
    internal sealed class PenelitianPkmRepository(PenelitianPkmDbContext context) : IPenelitianPkmRepository
    {
        public async Task<Domain.PenelitianPkm.PenelitianPkm> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.PenelitianPkm.PenelitianPkm akurasiPenelitian = await context.PenelitianPkm.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task<Domain.PenelitianPkm.PenelitianPkm> GetAsync(Guid Uuid, string Nidn, CancellationToken cancellationToken = default)
        {
            Domain.PenelitianPkm.PenelitianPkm akurasiPenelitian = await context.PenelitianPkm.SingleOrDefaultAsync(e => e.Uuid == Uuid && e.NIDN == Nidn, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.PenelitianPkm.PenelitianPkm akurasiPenelitian)
        {
            context.PenelitianPkm.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.PenelitianPkm.PenelitianPkm akurasiPenelitian)
        {
            context.PenelitianPkm.Add(akurasiPenelitian);
        }

        public async Task<bool> HasUniqueDataAsync(Guid? Uuid, string NIDN, string Judul, CancellationToken cancellationToken = default)
        {
            int count;

            if (Uuid != null)
            {
                count = await context.PenelitianPkm
                .Where(e => e.Uuid != Uuid && e.NIDN == NIDN && EF.Functions.Like(e.Judul, $"%{Judul}%"))
                .CountAsync(cancellationToken);

                return count == 0;
            }

            count = await context.PenelitianPkm
                .Where(e => e.NIDN == NIDN && EF.Functions.Like(e.Judul, $"%{Judul}%"))
                .CountAsync(cancellationToken);

            return count == 0;
        }


    }
}
