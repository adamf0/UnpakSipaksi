using MediatR;
using UnpakSipaksi.Common.Domain;

using IKewajaranTahapanTargetApi = UnpakSipaksi.Modules.KewajaranTahapanTarget.PublicApi.IKewajaranTahapanTargetApi;
using KewajaranTahapanTargetResponseApi = UnpakSipaksi.Modules.KewajaranTahapanTarget.PublicApi.KewajaranTahapanTargetResponse;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetBobotKewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetKewajaranTahapanTarget;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Infrastructure.PublicApi
{
    internal sealed class KewajaranTahapanTargetApi(ISender sender) : IKewajaranTahapanTargetApi
    {
        public async Task<KewajaranTahapanTargetResponseApi?> GetAsync(Guid KewajaranTahapanTargetUuid, CancellationToken cancellationToken = default)
        {
            Result<KewajaranTahapanTargetDefaultResponse> result = await sender.Send(new GetKewajaranTahapanTargetDefaultQuery(KewajaranTahapanTargetUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KewajaranTahapanTargetResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotKewajaranTahapanTargetQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
