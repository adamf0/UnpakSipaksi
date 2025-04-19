using MediatR;
using UnpakSipaksi.Common.Domain;

using IKesesuaianPenugasanApi = UnpakSipaksi.Modules.KesesuaianPenugasan.PublicApi.IKesesuaianPenugasanApi;
using KesesuaianPenugasanResponseApi = UnpakSipaksi.Modules.KesesuaianPenugasan.PublicApi.KesesuaianPenugasanResponse;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetBobotKesesuaianPenugasan;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Infrastructure.PublicApi
{
    internal sealed class KesesuaianPenugasanApi(ISender sender) : IKesesuaianPenugasanApi
    {
        public async Task<KesesuaianPenugasanResponseApi?> GetAsync(Guid KesesuaianPenugasanUuid, CancellationToken cancellationToken = default)
        {
            Result<KesesuaianPenugasanDefaultResponse> result = await sender.Send(new GetKesesuaianPenugasanDefaultQuery(KesesuaianPenugasanUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KesesuaianPenugasanResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotKesesuaianPenugasanQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
