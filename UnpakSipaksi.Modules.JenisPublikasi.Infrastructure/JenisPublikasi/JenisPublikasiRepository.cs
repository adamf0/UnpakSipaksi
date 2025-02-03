using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi;
using UnpakSipaksi.Modules.JenisPublikasi.Infrastructure.Database;

namespace UnpakSipaksi.Modules.JenisPublikasi.Infrastructure.JenisPublikasi
{
    internal sealed class JenisPublikasiRepository(JenisPublikasiDbContext context) : IJenisPublikasiRepository
    {
        public async Task<Domain.JenisPublikasi.JenisPublikasi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.JenisPublikasi.JenisPublikasi jenisPublikasi = await context.JenisPublikasi.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return jenisPublikasi;
        }

        public async Task DeleteAsync(Domain.JenisPublikasi.JenisPublikasi jenisPublikasi)
        {
            context.JenisPublikasi.Remove(jenisPublikasi);
        }

        public void Insert(Domain.JenisPublikasi.JenisPublikasi jenisPublikasi)
        {
            context.JenisPublikasi.Add(jenisPublikasi);
        }
    }
}
