using MediatR;
using UnpakSipaksi.Common.Domain;

using IVideoKegiatanApi = UnpakSipaksi.Modules.VideoKegiatan.PublicApi.IVideoKegiatanApi;
using VideoKegiatanResponseApi = UnpakSipaksi.Modules.VideoKegiatan.PublicApi.VideoKegiatanResponse;
using UnpakSipaksi.Modules.VideoKegiatan.Application.GetBobotVideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Application.GetVideoKegiatan;

namespace UnpakSipaksi.Modules.VideoKegiatan.Infrastructure.PublicApi
{
    internal sealed class VideoKegiatanApi(ISender sender) : IVideoKegiatanApi
    {
        public async Task<VideoKegiatanResponseApi?> GetAsync(Guid VideoKegiatanUuid, CancellationToken cancellationToken = default)
        {
            Result<VideoKegiatanDefaultResponse> result = await sender.Send(new GetVideoKegiatanDefaultQuery(VideoKegiatanUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new VideoKegiatanResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotVideoKegiatanQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
