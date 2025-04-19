using MediatR;
using UnpakSipaksi.Common.Domain;

using IRelevansiKualitasReferensiApi = UnpakSipaksi.Modules.RelevansiKualitasReferensi.PublicApi.IRelevansiKualitasReferensiApi;
using RelevansiKualitasReferensiResponseApi = UnpakSipaksi.Modules.RelevansiKualitasReferensi.PublicApi.RelevansiKualitasReferensiResponse;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetRelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetBobotRelevansiKualitasReferensi;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Infrastructure.PublicApi
{
    internal sealed class RelevansiKualitasReferensiApi(ISender sender) : IRelevansiKualitasReferensiApi
    {
        public async Task<RelevansiKualitasReferensiResponseApi?> GetAsync(Guid RelevansiKualitasReferensiUuid, CancellationToken cancellationToken = default)
        {
            Result<RelevansiKualitasReferensiDefaultResponse> result = await sender.Send(new GetRelevansiKualitasReferensiDefaultQuery(RelevansiKualitasReferensiUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new RelevansiKualitasReferensiResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotRelevansiKualitasReferensiQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
