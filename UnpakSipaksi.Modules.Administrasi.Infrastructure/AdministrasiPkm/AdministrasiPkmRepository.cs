using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiPkm;
using UnpakSipaksi.Modules.Administrasi.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Administrasi.Infrastructure.AdministrasiPkm
{
    internal sealed class AdministrasiPkmRepository(AdministrasiPkmDbContext context) : IAdministrasiPkmRepository
    {
        public async Task<Domain.AdministrasiPkm.AdministrasiPkm> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.AdministrasiPkm.AdministrasiPkm akurasiPenelitian = await context.AdministrasiPkm.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.AdministrasiPkm.AdministrasiPkm akurasiPenelitian)
        {
            context.AdministrasiPkm.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.AdministrasiPkm.AdministrasiPkm akurasiPenelitian)
        {
            context.AdministrasiPkm.Add(akurasiPenelitian);
        }
    }
}
