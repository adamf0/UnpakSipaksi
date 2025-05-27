using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.Substansi;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Substansi
{
    internal sealed class SubstansiRepository(SubstansiDbContext context, PenelitianHibahDbContext contextPenelitianHibah) : ISubstansiRepository
    {
        public async Task<Domain.Substansi.Substansi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.Substansi.Substansi memberDosen = await context.Substansi.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return memberDosen;
        }

        public async Task DeleteAsync(Domain.Substansi.Substansi memberDosen)
        {
            context.Substansi.Remove(memberDosen);
        }

        public void Insert(Domain.Substansi.Substansi memberDosen)
        {
            context.Substansi.Add(memberDosen);
        }
    }
}
