using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Infrastructure.KredibilitasMitraDukungan
{
    internal sealed class KredibilitasMitraDukunganRepository(KredibilitasMitraDukunganDbContext context) : IKredibilitasMitraDukunganRepository
    {
        public async Task<Domain.KredibilitasMitraDukungan.KredibilitasMitraDukungan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KredibilitasMitraDukungan.KredibilitasMitraDukungan KredibilitasMitraDukungan = await context.KredibilitasMitraDukungan.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return KredibilitasMitraDukungan;
        }

        public async Task DeleteAsync(Domain.KredibilitasMitraDukungan.KredibilitasMitraDukungan KredibilitasMitraDukungan)
        {
            context.KredibilitasMitraDukungan.Remove(KredibilitasMitraDukungan);
        }

        public void Insert(Domain.KredibilitasMitraDukungan.KredibilitasMitraDukungan KredibilitasMitraDukungan)
        {
            context.KredibilitasMitraDukungan.Add(KredibilitasMitraDukungan);
        }
    }
}
