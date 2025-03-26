using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.Infrastructure.Database;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Infrastructure.SotaKebaharuan
{
    internal sealed class SotaKebaharuanRepository(SotaKebaharuanDbContext context) : ISotaKebaharuanRepository
    {
        public async Task<Domain.SotaKebaharuan.SotaKebaharuan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.SotaKebaharuan.SotaKebaharuan SotaKebaharuan = await context.SotaKebaharuan.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return SotaKebaharuan;
        }

        public async Task DeleteAsync(Domain.SotaKebaharuan.SotaKebaharuan SotaKebaharuan)
        {
            context.SotaKebaharuan.Remove(SotaKebaharuan);
        }

        public void Insert(Domain.SotaKebaharuan.SotaKebaharuan SotaKebaharuan)
        {
            context.SotaKebaharuan.Add(SotaKebaharuan);
        }
    }
}
