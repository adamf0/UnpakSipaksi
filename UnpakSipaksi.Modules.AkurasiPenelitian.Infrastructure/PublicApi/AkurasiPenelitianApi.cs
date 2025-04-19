using MediatR;
using UnpakSipaksi.Common.Domain;

using IAkurasiPenelitianApi = UnpakSipaksi.Modules.AkurasiPenelitian.PublicApi.IAkurasiPenelitianApi;
using AkurasiPenelitianResponseApi = UnpakSipaksi.Modules.AkurasiPenelitian.PublicApi.AkurasiPenelitianResponse;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetBobotAkurasiPenelitian;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Infrastructure.PublicApi
{
    internal sealed class AkurasiPenelitianApi(ISender sender) : IAkurasiPenelitianApi
    {
        public async Task<AkurasiPenelitianResponseApi?> GetAsync(Guid AkurasiPenelitianUuid, CancellationToken cancellationToken = default)
        {
            Result<AkurasiPenelitianDefaultResponse> result = await sender.Send(new GetAkurasiPenelitianDefaultQuery(AkurasiPenelitianUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new AkurasiPenelitianResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotAkurasiPenelitianQuery(KategoriSkema), cancellationToken);

            //[Review] ini masih bingung apa di throw atau default null
            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
