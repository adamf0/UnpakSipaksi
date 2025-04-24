using Microsoft.EntityFrameworkCore;
using Minio.DataModel;
using System;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database;
using FuzzySharp;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.PenelitianHibah
{
    internal sealed class PenelitianHibahRepository(PenelitianHibahDbContext context) : IPenelitianHibahRepository
    {
        public async Task<Domain.PenelitianHibah.PenelitianHibah> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.PenelitianHibah.PenelitianHibah akurasiPenelitian = await context.PenelitianHibah.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task<Domain.PenelitianHibah.PenelitianHibah> GetAsync(Guid Uuid, string Nidn, CancellationToken cancellationToken = default)
        {
            Domain.PenelitianHibah.PenelitianHibah akurasiPenelitian = await context.PenelitianHibah.SingleOrDefaultAsync(e => e.Uuid == Uuid && e.NIDN == Nidn, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.PenelitianHibah.PenelitianHibah akurasiPenelitian)
        {
            context.PenelitianHibah.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.PenelitianHibah.PenelitianHibah akurasiPenelitian)
        {
            context.PenelitianHibah.Add(akurasiPenelitian);
        }

        public async Task<bool> HasUniqueDataAsync(Guid? Uuid, string NIDN, string Judul, CancellationToken cancellationToken = default)
        {
            int count;

            if (Uuid != null) {
                count = await context.PenelitianHibah
                .Where(e => e.Uuid != Uuid && e.NIDN == NIDN && EF.Functions.Like(e.Judul, $"%{Judul}%"))
                .CountAsync(cancellationToken);

                return count == 0;
            }

            count = await context.PenelitianHibah
                .Where(e => e.NIDN == NIDN && EF.Functions.Like(e.Judul, $"%{Judul}%"))
                .CountAsync(cancellationToken);

            return count==0;
        }


    }
}
