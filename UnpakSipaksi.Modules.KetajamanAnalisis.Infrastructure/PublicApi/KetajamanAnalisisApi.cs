using MediatR;
using UnpakSipaksi.Common.Domain;

using IKetajamanAnalisisApi = UnpakSipaksi.Modules.KetajamanAnalisis.PublicApi.IKetajamanAnalisisApi;
using KetajamanAnalisisResponseApi = UnpakSipaksi.Modules.KetajamanAnalisis.PublicApi.KetajamanAnalisisResponse;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.GetBobotKetajamanAnalisis;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.GetKetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Infrastructure.PublicApi
{
    internal sealed class KetajamanAnalisisApi(ISender sender) : IKetajamanAnalisisApi
    {
        public async Task<KetajamanAnalisisResponseApi?> GetAsync(Guid KetajamanAnalisisUuid, CancellationToken cancellationToken = default)
        {
            Result<KetajamanAnalisisDefaultResponse> result = await sender.Send(new GetKetajamanAnalisisDefaultQuery(KetajamanAnalisisUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KetajamanAnalisisResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotKetajamanAnalisisQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
