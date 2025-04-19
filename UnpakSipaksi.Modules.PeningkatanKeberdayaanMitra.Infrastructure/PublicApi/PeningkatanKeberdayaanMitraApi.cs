using MediatR;
using UnpakSipaksi.Common.Domain;

using IPeningkatanKeberdayaanMitraApi = UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.PublicApi.IPeningkatanKeberdayaanMitraApi;
using PeningkatanKeberdayaanMitraResponseApi = UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.PublicApi.PeningkatanKeberdayaanMitraResponse;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetBobotPeningkatanKeberdayaanMitra;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetPeningkatanKeberdayaanMitra;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Infrastructure.PublicApi
{
    internal sealed class PeningkatanKeberdayaanMitraApi(ISender sender) : IPeningkatanKeberdayaanMitraApi
    {
        public async Task<PeningkatanKeberdayaanMitraResponseApi?> GetAsync(Guid PeningkatanKeberdayaanMitraUuid, CancellationToken cancellationToken = default)
        {
            Result<PeningkatanKeberdayaanMitraDefaultResponse> result = await sender.Send(new GetPeningkatanKeberdayaanMitraDefaultQuery(PeningkatanKeberdayaanMitraUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new PeningkatanKeberdayaanMitraResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotPeningkatanKeberdayaanMitraQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
