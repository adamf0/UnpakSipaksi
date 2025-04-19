using MediatR;
using UnpakSipaksi.Common.Domain;

using IKuantitasStatusKiApi = UnpakSipaksi.Modules.KuantitasStatusKi.PublicApi.IKuantitasStatusKiApi;
using KuantitasStatusKiResponseApi = UnpakSipaksi.Modules.KuantitasStatusKi.PublicApi.KuantitasStatusKiResponse;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetKuantitasStatusKi;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetBobotKuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Infrastructure.PublicApi
{
    internal sealed class KuantitasStatusKiApi(ISender sender) : IKuantitasStatusKiApi
    {
        public async Task<KuantitasStatusKiResponseApi?> GetAsync(Guid KuantitasStatusKiUuid, CancellationToken cancellationToken = default)
        {
            Result<KuantitasStatusKiDefaultResponse> result = await sender.Send(new GetKuantitasStatusKiDefaultQuery(KuantitasStatusKiUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KuantitasStatusKiResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotKuantitasStatusKiQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
