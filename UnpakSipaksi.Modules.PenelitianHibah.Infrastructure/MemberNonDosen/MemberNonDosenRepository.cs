using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberNonDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.MemberNonDosen
{
    internal sealed class MemberNonDosenRepository(MemberNonDosenDbContext context) : IMemberNonDosenRepository
    {
        public async Task<Domain.MemberNonDosen.MemberNonDosen> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.MemberNonDosen.MemberNonDosen memberDosen = await context.MemberNonDosen.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return memberDosen;
        }

        public async Task DeleteAsync(Domain.MemberNonDosen.MemberNonDosen memberDosen)
        {
            context.MemberNonDosen.Remove(memberDosen);
        }

        public void Insert(Domain.MemberNonDosen.MemberNonDosen memberDosen)
        {
            context.MemberNonDosen.Add(memberDosen);
        }
    }
}
