using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.Luaran;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Luaran
{
    internal sealed class LuaranRepository(LuaranDbContext context, PenelitianHibahDbContext contextPenelitianHibah) : ILuaranRepository
    {
        public async Task<Domain.Luaran.Luaran> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Luaran.Luaran memberDosen = await context.Luaran.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return memberDosen;
        }

        public async Task DeleteAsync(Domain.Luaran.Luaran memberDosen)
        {
            context.Luaran.Remove(memberDosen);
        }

        public void Insert(Domain.Luaran.Luaran memberDosen)
        {
            context.Luaran.Add(memberDosen);
        }
    }
}
