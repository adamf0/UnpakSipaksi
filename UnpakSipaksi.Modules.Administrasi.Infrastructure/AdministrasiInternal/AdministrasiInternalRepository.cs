using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiInternal;
using UnpakSipaksi.Modules.Administrasi.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Administrasi.Infrastructure.AdministrasiInternal
{
    internal sealed class AdministrasiInternalRepository(AdministrasiInternalDbContext context) : IAdministrasiInternalRepository
    {
        public async Task<Domain.AdministrasiInternal.AdministrasiInternal> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.AdministrasiInternal.AdministrasiInternal akurasiPenelitian = await context.AdministrasiInternal.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.AdministrasiInternal.AdministrasiInternal akurasiPenelitian)
        {
            context.AdministrasiInternal.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.AdministrasiInternal.AdministrasiInternal akurasiPenelitian)
        {
            context.AdministrasiInternal.Add(akurasiPenelitian);
        }
    }
}
