
using MediatR;
using UnpakSipaksi.Common.Domain;

using ISotaKebaharuanApi = UnpakSipaksi.Modules.SotaKebaharuan.PublicApi.ISotaKebaharuanApi;
using SotaKebaharuanResponseApi = UnpakSipaksi.Modules.SotaKebaharuan.PublicApi.SotaKebaharuanResponse;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.GetSotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.GetBobotSotaKebaharuan;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Infrastructure.PublicApi
{
    internal sealed class SotaKebaharuanApi(ISender sender) : ISotaKebaharuanApi
    {
        public async Task<SotaKebaharuanResponseApi?> GetAsync(Guid SotaKebaharuanUuid, CancellationToken cancellationToken = default)
        {
            Result<SotaKebaharuanDefaultResponse> result = await sender.Send(new GetSotaKebaharuanDefaultQuery(SotaKebaharuanUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new SotaKebaharuanResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotSotaKebaharuanQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
