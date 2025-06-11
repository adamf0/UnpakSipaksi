using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.MemberMahasiswa
{
    internal sealed class MemberMahasiswaRepository(MemberMahasiswaDbContext context, PenelitianPkmDbContext contextPenelitanHibah) : IMemberMahasiswaRepository
    {
        public async Task<Domain.MemberMahasiswa.MemberMahasiswa> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.MemberMahasiswa.MemberMahasiswa MemberMahasiswa = await context.MemberMahasiswa.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return MemberMahasiswa;
        }

        public async Task<Domain.MemberMahasiswa.MemberMahasiswa> GetAsync(Guid Uuid, string Npm, CancellationToken cancellationToken = default)
        {
            Domain.MemberMahasiswa.MemberMahasiswa MemberMahasiswa = await context.MemberMahasiswa.SingleOrDefaultAsync(e => e.Uuid == Uuid && e.NPM == Npm, cancellationToken);
            return MemberMahasiswa;
        }

        public async Task DeleteAsync(Domain.MemberMahasiswa.MemberMahasiswa MemberMahasiswa)
        {
            context.MemberMahasiswa.Remove(MemberMahasiswa);
        }

        public void Insert(Domain.MemberMahasiswa.MemberMahasiswa MemberMahasiswa)
        {
            context.MemberMahasiswa.Add(MemberMahasiswa);
        }

        public async Task<int> CheckUniqueDataAsync(int PenelitianPkmId, string NPM, CancellationToken cancellationToken = default)
        {
            var count = await context.MemberMahasiswa
                .Where(e => e.PenelitianPkmId == PenelitianPkmId && e.NPM == NPM)
                .CountAsync(cancellationToken);

            return count;
        }

        public async Task<Domain.MemberMahasiswa.MemberMahasiswa> GetAsync(Guid Uuid, Guid UuidPenelitianPkm, string Npm, CancellationToken cancellationToken = default)
        {
            var penelitian = await contextPenelitanHibah.PenelitianPkm
                .Where(p => p.Uuid == UuidPenelitianPkm)
                .Select(p => p.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (penelitian == 0)
                return null;

            var memberMahasiswa = await context.MemberMahasiswa
                .Where(m => m.Uuid == Uuid && m.NPM == Npm && m.PenelitianPkmId == penelitian)
                .SingleOrDefaultAsync(cancellationToken);

            return memberMahasiswa;

        }
    }
}
