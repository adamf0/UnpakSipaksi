using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Infrastructure.Database;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Infrastructure.RumusanPrioritasMitra
{
    internal sealed class RumusanPrioritasMitraRepository(RumusanPrioritasMitraDbContext context) : IRumusanPrioritasMitraRepository
    {
        public async Task<Domain.RumusanPrioritasMitra.RumusanPrioritasMitra> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.RumusanPrioritasMitra.RumusanPrioritasMitra RumusanPrioritasMitra = await context.RumusanPrioritasMitra.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return RumusanPrioritasMitra;
        }

        public async Task DeleteAsync(Domain.RumusanPrioritasMitra.RumusanPrioritasMitra RumusanPrioritasMitra)
        {
            context.RumusanPrioritasMitra.Remove(RumusanPrioritasMitra);
        }

        public void Insert(Domain.RumusanPrioritasMitra.RumusanPrioritasMitra RumusanPrioritasMitra)
        {
            context.RumusanPrioritasMitra.Add(RumusanPrioritasMitra);
        }
    }
}
