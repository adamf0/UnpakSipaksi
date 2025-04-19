using MediatR;
using UnpakSipaksi.Common.Domain;

using IKesesuaianJadwalApi = UnpakSipaksi.Modules.KesesuaianJadwal.PublicApi.IKesesuaianJadwalApi;
using KesesuaianJadwalResponseApi = UnpakSipaksi.Modules.KesesuaianJadwal.PublicApi.KesesuaianJadwalResponse;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetBobotKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetKesesuaianJadwal;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Infrastructure.PublicApi
{
    internal sealed class KesesuaianJadwalApi(ISender sender) : IKesesuaianJadwalApi
    {
        public async Task<KesesuaianJadwalResponseApi?> GetAsync(Guid KesesuaianJadwalUuid, CancellationToken cancellationToken = default)
        {
            Result<KesesuaianJadwalDefaultResponse> result = await sender.Send(new GetKesesuaianJadwalDefaultQuery(KesesuaianJadwalUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KesesuaianJadwalResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotKesesuaianJadwalQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
