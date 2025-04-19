using MediatR;
using UnpakSipaksi.Common.Domain;

using IKesesuaianTktApi = UnpakSipaksi.Modules.KesesuaianTkt.PublicApi.IKesesuaianTktApi;
using KesesuaianTktResponseApi = UnpakSipaksi.Modules.KesesuaianTkt.PublicApi.KesesuaianTktResponse;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.GetKesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.GetBobotKesesuaianTkt;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Infrastructure.PublicApi
{
    internal sealed class KesesuaianTktApi(ISender sender) : IKesesuaianTktApi
    {
        public async Task<KesesuaianTktResponseApi?> GetAsync(Guid KesesuaianTktUuid, CancellationToken cancellationToken = default)
        {
            Result<KesesuaianTktDefaultResponse> result = await sender.Send(new GetKesesuaianTktDefaultQuery(KesesuaianTktUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KesesuaianTktResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotKesesuaianTktQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
