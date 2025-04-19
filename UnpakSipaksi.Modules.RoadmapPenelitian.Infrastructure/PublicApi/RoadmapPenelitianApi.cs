using MediatR;
using UnpakSipaksi.Common.Domain;

using IRoadmapPenelitianApi = UnpakSipaksi.Modules.RoadmapPenelitian.PublicApi.IRoadmapPenelitianApi;
using RoadmapPenelitianResponseApi = UnpakSipaksi.Modules.RoadmapPenelitian.PublicApi.RoadmapPenelitianResponse;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetRoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetBobotRoadmapPenelitian;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Infrastructure.PublicApi
{
    internal sealed class RoadmapPenelitianApi(ISender sender) : IRoadmapPenelitianApi
    {
        public async Task<RoadmapPenelitianResponseApi?> GetAsync(Guid RoadmapPenelitianUuid, CancellationToken cancellationToken = default)
        {
            Result<RoadmapPenelitianDefaultResponse> result = await sender.Send(new GetRoadmapPenelitianDefaultQuery(RoadmapPenelitianUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new RoadmapPenelitianResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.BobotPDP,
                result.Value.BobotTerapan,
                result.Value.BobotKerjasama,
                result.Value.BobotPenelitianDasar,
                result.Value.Skor
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotRoadmapPenelitianQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
