using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi;
using UnpakSipaksi.Modules.KuantitasStatusKi.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Infrastructure.KuantitasStatusKi
{
    internal sealed class KuantitasStatusKiRepository(KuantitasStatusKiDbContext context) : IKuantitasStatusKiRepository
    {
        public async Task<Domain.KuantitasStatusKi.KuantitasStatusKi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KuantitasStatusKi.KuantitasStatusKi KuantitasStatusKi = await context.KuantitasStatusKi.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return KuantitasStatusKi;
        }

        public async Task DeleteAsync(Domain.KuantitasStatusKi.KuantitasStatusKi KuantitasStatusKi)
        {
            context.KuantitasStatusKi.Remove(KuantitasStatusKi);
        }

        public void Insert(Domain.KuantitasStatusKi.KuantitasStatusKi KuantitasStatusKi)
        {
            context.KuantitasStatusKi.Add(KuantitasStatusKi);
        }
    }
}
