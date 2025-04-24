using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.MemberMahasiswa
{
    internal sealed class MemberMahasiswaRepository(MemberMahasiswaDbContext context, PenelitianHibahDbContext contextPenelitanHibah) : IMemberMahasiswaRepository
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

        public async Task<int> CheckUniqueDataAsync(int PenelitianHibahId, string NPM, CancellationToken cancellationToken = default)
        {
            var count = await context.MemberMahasiswa
                .Where(e => e.PenelitianHibahId == PenelitianHibahId && e.NPM == NPM)
                .CountAsync(cancellationToken);

            return count;
        }

        public async Task<Domain.MemberMahasiswa.MemberMahasiswa> GetAsync(Guid Uuid, Guid UuidPenelitianHibah, string Npm, CancellationToken cancellationToken = default)
        {
            var penelitian = await contextPenelitanHibah.PenelitianHibah
                .Where(p => p.Uuid == UuidPenelitianHibah)
                .Select(p => p.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (penelitian == 0)
                return null;

            var memberMahasiswa = await context.MemberMahasiswa
                .Where(m => m.Uuid == Uuid && m.NPM == Npm && m.PenelitianHibahId == penelitian)
                .SingleOrDefaultAsync(cancellationToken);

            return memberMahasiswa;

        }
    }
}
