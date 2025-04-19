using MediatR;
using UnpakSipaksi.Common.Domain;

using IKesesuaianWaktuRabLuaranFasilitasApi = UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.PublicApi.IKesesuaianWaktuRabLuaranFasilitasApi;
using KesesuaianWaktuRabLuaranFasilitasResponseApi = UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.PublicApi.KesesuaianWaktuRabLuaranFasilitasResponse;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.GetKesesuaianWaktuRabLuaranFasilitas;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.GetBobotKesesuaianWaktuRabLuaranFasilitas;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Infrastructure.PublicApi
{
    internal sealed class KesesuaianWaktuRabLuaranFasilitasApi(ISender sender) : IKesesuaianWaktuRabLuaranFasilitasApi
    {
        public async Task<KesesuaianWaktuRabLuaranFasilitasResponseApi?> GetAsync(Guid KesesuaianWaktuRabLuaranFasilitasUuid, CancellationToken cancellationToken = default)
        {
            Result<KesesuaianWaktuRabLuaranFasilitasDefaultResponse> result = await sender.Send(new GetKesesuaianWaktuRabLuaranFasilitasDefaultQuery(KesesuaianWaktuRabLuaranFasilitasUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KesesuaianWaktuRabLuaranFasilitasResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotKesesuaianWaktuRabLuaranFasilitasQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
