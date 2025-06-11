using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberNonDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.MemberDosen
{
    internal sealed class MemberDosenRepository(MemberDosenDbContext context, PenelitianPkmDbContext contextPenelitianPkm) : IMemberDosenRepository
    {
        public async Task<Domain.MemberDosen.MemberDosen> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.MemberDosen.MemberDosen memberDosen = await context.MemberDosen.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return memberDosen;
        }

        public async Task<Domain.MemberDosen.MemberDosen> GetAsync(Guid Uuid, string Nidn, CancellationToken cancellationToken = default)
        {
            Domain.MemberDosen.MemberDosen memberDosen = await context.MemberDosen.SingleOrDefaultAsync(e => e.Uuid == Uuid && e.NIDN == Nidn, cancellationToken);
            return memberDosen;
        }

        public async Task DeleteAsync(Domain.MemberDosen.MemberDosen memberDosen)
        {
            context.MemberDosen.Remove(memberDosen);
        }

        public void Insert(Domain.MemberDosen.MemberDosen memberDosen)
        {
            context.MemberDosen.Add(memberDosen);
        }

        public async Task<int> CheckUniqueDataAsync(int PenelitianPkmId, string NIDN, CancellationToken cancellationToken = default)
        {
            var count = await context.MemberDosen
                .Where(e => e.PenelitianPkmId == PenelitianPkmId && e.NIDN == NIDN)
                .CountAsync(cancellationToken);

            return count;
        }

        public async Task<Domain.MemberDosen.MemberDosen> GetAsync(Guid dosenUuid, Guid penelitianHibahUuid, CancellationToken cancellationToken = default)
        {
            var penelitian = await contextPenelitianPkm.PenelitianPkm
                .Where(p => p.Uuid == penelitianHibahUuid)
                .Select(p => p.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (penelitian == 0)
                return null;

            var memberDosen = await context.MemberDosen
                .SingleOrDefaultAsync(dosen => dosen.Uuid == dosenUuid && dosen.PenelitianPkmId == penelitian, cancellationToken);

            return memberDosen;
        }


    }
}
