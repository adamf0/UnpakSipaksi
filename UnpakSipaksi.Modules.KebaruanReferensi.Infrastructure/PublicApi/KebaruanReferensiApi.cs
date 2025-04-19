using MediatR;
using UnpakSipaksi.Common.Domain;

using IKebaruanReferensiApi = UnpakSipaksi.Modules.KebaruanReferensi.PublicApi.IKebaruanReferensiApi;
using KebaruanReferensiResponseApi= UnpakSipaksi.Modules.KebaruanReferensi.PublicApi.KebaruanReferensiResponse;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.GetKebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.GetBobotKebaruanReferensi;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Infrastructure.PublicApi
{
    internal sealed class KebaruanReferensiApi(ISender sender) : IKebaruanReferensiApi
    {
        public async Task<KebaruanReferensiResponseApi?> GetAsync(Guid KebaruanReferensiUuid, CancellationToken cancellationToken = default)
        {
            Result<KebaruanReferensiDefaultResponse> result = await sender.Send(new GetKebaruanReferensiDefaultQuery(KebaruanReferensiUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KebaruanReferensiResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotKebaruanReferensiQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
