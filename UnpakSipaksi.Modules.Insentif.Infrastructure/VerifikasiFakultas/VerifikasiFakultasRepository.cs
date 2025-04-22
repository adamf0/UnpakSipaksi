using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;
using UnpakSipaksi.Modules.Insentif.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Insentif.Infrastructure.VerifikasiFakultas
{
    internal sealed class VerifikasiFakultasRepository(VerifikasiFakultasDbContext context) : IVerifikasiFakultasRepository
    {
        public async Task<Domain.VerifikasiFakultas.VerifikasiFakultas> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.VerifikasiFakultas.VerifikasiFakultas akurasiPenelitian = await context.VerifikasiFakultas.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.VerifikasiFakultas.VerifikasiFakultas akurasiPenelitian)
        {
            context.VerifikasiFakultas.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.VerifikasiFakultas.VerifikasiFakultas akurasiPenelitian)
        {
            context.VerifikasiFakultas.Add(akurasiPenelitian);
        }
    }
}
