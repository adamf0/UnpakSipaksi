using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.IndikatorCapaian.Domain;
using UnpakSipaksi.Modules.IndikatorCapaian.Infrastructure.Database;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Infrastructure.IndikatorCapaian
{
    internal sealed class IndikatorCapaianRepository(IndikatorCapaianDbContext context) : IIndikatorCapaianRepository
    {
        public async Task<Domain.IndikatorCapaian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.IndikatorCapaian akurasiPenelitian = await context.IndikatorCapaian.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.IndikatorCapaian akurasiPenelitian)
        {
            context.IndikatorCapaian.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.IndikatorCapaian akurasiPenelitian)
        {
            context.IndikatorCapaian.Add(akurasiPenelitian);
        }
    }
}
