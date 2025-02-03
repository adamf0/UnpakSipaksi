using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KelompokRab.Domain.KelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KelompokRab.Infrastructure.KelompokRab
{
    internal sealed class KelompokRabRepository(KelompokRabDbContext context) : IKelompokRabRepository
    {
        public async Task<Domain.KelompokRab.KelompokRab> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KelompokRab.KelompokRab kelompokRab = await context.KelompokRab.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return kelompokRab;
        }

        public async Task DeleteAsync(Domain.KelompokRab.KelompokRab kelompokRab)
        {
            context.KelompokRab.Remove(kelompokRab);
        }

        public void Insert(Domain.KelompokRab.KelompokRab kelompokRab)
        {
            context.KelompokRab.Add(kelompokRab);
        }
    }
}
