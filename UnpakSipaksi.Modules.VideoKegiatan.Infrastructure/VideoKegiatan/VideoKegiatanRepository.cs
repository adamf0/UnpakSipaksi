using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Infrastructure.Database;

namespace UnpakSipaksi.Modules.VideoKegiatan.Infrastructure.VideoKegiatan
{
    internal sealed class VideoKegiatanRepository(VideoKegiatanDbContext context) : IVideoKegiatanRepository
    {
        public async Task<Domain.VideoKegiatan.VideoKegiatan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.VideoKegiatan.VideoKegiatan VideoKegiatan = await context.VideoKegiatan.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return VideoKegiatan;
        }

        public async Task DeleteAsync(Domain.VideoKegiatan.VideoKegiatan VideoKegiatan)
        {
            context.VideoKegiatan.Remove(VideoKegiatan);
        }

        public void Insert(Domain.VideoKegiatan.VideoKegiatan VideoKegiatan)
        {
            context.VideoKegiatan.Add(VideoKegiatan);
        }
    }
}
