using MediatR;
using UnpakSipaksi.Common.Domain;

using IKesesuaianSolusiMasalahMitraApi = UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.PublicApi.IKesesuaianSolusiMasalahMitraApi;
using KesesuaianSolusiMasalahMitraResponseApi = UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.PublicApi.KesesuaianSolusiMasalahMitraResponse;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetKesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetBobotKesesuaianSolusiMasalahMitra;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Infrastructure.PublicApi
{
    internal sealed class KesesuaianSolusiMasalahMitraApi(ISender sender) : IKesesuaianSolusiMasalahMitraApi
    {
        public async Task<KesesuaianSolusiMasalahMitraResponseApi?> GetAsync(Guid KesesuaianSolusiMasalahMitraUuid, CancellationToken cancellationToken = default)
        {
            Result<KesesuaianSolusiMasalahMitraDefaultResponse> result = await sender.Send(new GetKesesuaianSolusiMasalahMitraDefaultQuery(KesesuaianSolusiMasalahMitraUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KesesuaianSolusiMasalahMitraResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotKesesuaianSolusiMasalahMitraQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
