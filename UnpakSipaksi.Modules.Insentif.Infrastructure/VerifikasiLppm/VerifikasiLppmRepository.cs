using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiLppm;
using UnpakSipaksi.Modules.Insentif.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Insentif.Infrastructure.VerifikasiLppm
{
    internal sealed class VerifikasiLppmRepository(VerifikasiLppmDbContext context) : IVerifikasiLppmRepository
    {
        public async Task<Domain.VerifikasiLppm.VerifikasiLppm> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.VerifikasiLppm.VerifikasiLppm akurasiPenelitian = await context.VerifikasiLppm.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.VerifikasiLppm.VerifikasiLppm akurasiPenelitian)
        {
            context.VerifikasiLppm.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.VerifikasiLppm.VerifikasiLppm akurasiPenelitian)
        {
            context.VerifikasiLppm.Add(akurasiPenelitian);
        }
    }
}
