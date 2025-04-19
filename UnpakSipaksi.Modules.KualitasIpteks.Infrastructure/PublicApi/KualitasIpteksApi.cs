using MediatR;
using UnpakSipaksi.Common.Domain;

using IKualitasIpteksApi = UnpakSipaksi.Modules.KualitasIpteks.PublicApi.IKualitasIpteksApi;
using KualitasIpteksResponseApi = UnpakSipaksi.Modules.KualitasIpteks.PublicApi.KualitasIpteksResponse;
using UnpakSipaksi.Modules.KualitasIpteks.Application.GetKualitasIpteks;
using UnpakSipaksi.Modules.KualitasIpteks.Application.GetBobotKualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.Infrastructure.PublicApi
{
    internal sealed class KualitasIpteksApi(ISender sender) : IKualitasIpteksApi
    {
        public async Task<KualitasIpteksResponseApi?> GetAsync(Guid KualitasIpteksUuid, CancellationToken cancellationToken = default)
        {
            Result<KualitasIpteksDefaultResponse> result = await sender.Send(new GetKualitasIpteksDefaultQuery(KualitasIpteksUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KualitasIpteksResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotKualitasIpteksQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
