using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Infrastructure.KategoriProgramPengabdian
{
    internal sealed class KategoriProgramPengabdianRepository(KategoriProgramPengabdianDbContext context) : IKategoriProgramPengabdianRepository
    {
        public async Task<Domain.KategoriProgramPengabdian.KategoriProgramPengabdian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KategoriProgramPengabdian.KategoriProgramPengabdian KategoriProgramPengabdian = await context.KategoriProgramPengabdian.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return KategoriProgramPengabdian;
        }

        public async Task DeleteAsync(Domain.KategoriProgramPengabdian.KategoriProgramPengabdian KategoriProgramPengabdian)
        {
            context.KategoriProgramPengabdian.Remove(KategoriProgramPengabdian);
        }

        public void Insert(Domain.KategoriProgramPengabdian.KategoriProgramPengabdian KategoriProgramPengabdian)
        {
            context.KategoriProgramPengabdian.Add(KategoriProgramPengabdian);
        }
    }
}
