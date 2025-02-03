using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Infrastructure.Database;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Infrastructure.JumlahKolaboratorPublikasBereputasi
{
    internal sealed class JumlahKolaboratorPublikasBereputasiRepository(JumlahKolaboratorPublikasBereputasiDbContext context) : IJumlahKolaboratorPublikasBereputasiRepository
    {
        public async Task<Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi jumlahKolaboratorPublikasBereputasi = await context.JumlahKolaboratorPublikasBereputasi.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return jumlahKolaboratorPublikasBereputasi;
        }

        public async Task DeleteAsync(Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi jumlahKolaboratorPublikasBereputasi)
        {
            context.JumlahKolaboratorPublikasBereputasi.Remove(jumlahKolaboratorPublikasBereputasi);
        }

        public void Insert(Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi jumlahKolaboratorPublikasBereputasi)
        {
            context.JumlahKolaboratorPublikasBereputasi.Add(jumlahKolaboratorPublikasBereputasi);
        }
    }
}
