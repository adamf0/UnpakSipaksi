using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Infrastructure.KualitasKuantitasPublikasiJurnalIlmiah
{
    internal sealed class KualitasKuantitasPublikasiJurnalIlmiahRepository(KualitasKuantitasPublikasiJurnalIlmiahDbContext context) : IKualitasKuantitasPublikasiJurnalIlmiahRepository
    {
        public async Task<Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah KualitasKuantitasPublikasiJurnalIlmiah = await context.KualitasKuantitasPublikasiJurnalIlmiah.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return KualitasKuantitasPublikasiJurnalIlmiah;
        }

        public async Task DeleteAsync(Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah KualitasKuantitasPublikasiJurnalIlmiah)
        {
            context.KualitasKuantitasPublikasiJurnalIlmiah.Remove(KualitasKuantitasPublikasiJurnalIlmiah);
        }

        public void Insert(Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah KualitasKuantitasPublikasiJurnalIlmiah)
        {
            context.KualitasKuantitasPublikasiJurnalIlmiah.Add(KualitasKuantitasPublikasiJurnalIlmiah);
        }
    }
}
