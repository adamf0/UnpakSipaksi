using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis;
using UnpakSipaksi.Modules.KetajamanAnalisis.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Infrastructure.KetajamanAnalisis
{
    internal sealed class KetajamanAnalisisRepository(KetajamanAnalisisDbContext context) : IKetajamanAnalisisRepository
    {
        public async Task<Domain.KetajamanAnalisis.KetajamanAnalisis> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KetajamanAnalisis.KetajamanAnalisis KetajamanAnalisis = await context.KetajamanAnalisis.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return KetajamanAnalisis;
        }

        public async Task DeleteAsync(Domain.KetajamanAnalisis.KetajamanAnalisis KetajamanAnalisis)
        {
            context.KetajamanAnalisis.Remove(KetajamanAnalisis);
        }

        public void Insert(Domain.KetajamanAnalisis.KetajamanAnalisis KetajamanAnalisis)
        {
            context.KetajamanAnalisis.Add(KetajamanAnalisis);
        }
    }
}
