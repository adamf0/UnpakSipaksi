using MediatR;
using UnpakSipaksi.Common.Domain;

using IKualitasKuantitasPublikasiProsidingApi = UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.PubliApi.IKualitasKuantitasPublikasiProsidingApi;
using KualitasKuantitasPublikasiProsidingResponseApi = UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.PubliApi.KualitasKuantitasPublikasiProsidingResponse;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetKualitasKuantitasPublikasiProsiding;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetBobotKualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Infrastructure.PublicApi
{
    internal sealed class KualitasKuantitasPublikasiProsidingApi(ISender sender) : IKualitasKuantitasPublikasiProsidingApi
    {
        public async Task<KualitasKuantitasPublikasiProsidingResponseApi?> GetAsync(Guid KualitasKuantitasPublikasiProsidingUuid, CancellationToken cancellationToken = default)
        {
            Result<KualitasKuantitasPublikasiProsidingDefaultResponse> result = await sender.Send(new GetKualitasKuantitasPublikasiProsidingDefaultQuery(KualitasKuantitasPublikasiProsidingUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KualitasKuantitasPublikasiProsidingResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotKualitasKuantitasPublikasiProsidingQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
