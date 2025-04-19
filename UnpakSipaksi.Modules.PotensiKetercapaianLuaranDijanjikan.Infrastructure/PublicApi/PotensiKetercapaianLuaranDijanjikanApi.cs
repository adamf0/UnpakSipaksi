using MediatR;
using UnpakSipaksi.Common.Domain;

using IPotensiKetercapaianLuaranDijanjikanApi = UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.PublicApi.IPotensiKetercapaianLuaranDijanjikanApi;
using PotensiKetercapaianLuaranDijanjikanResponseApi = UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.PublicApi.PotensiKetercapaianLuaranDijanjikanResponse;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetPotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetBobotPotensiKetercapaianLuaranDijanjikan;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Infrastructure.PublicApi
{
    internal sealed class PotensiKetercapaianLuaranDijanjikanApi(ISender sender) : IPotensiKetercapaianLuaranDijanjikanApi
    {
        public async Task<PotensiKetercapaianLuaranDijanjikanResponseApi?> GetAsync(Guid PotensiKetercapaianLuaranDijanjikanUuid, CancellationToken cancellationToken = default)
        {
            Result<PotensiKetercapaianLuaranDijanjikanDefaultResponse> result = await sender.Send(new GetPotensiKetercapaianLuaranDijanjikanDefaultQuery(PotensiKetercapaianLuaranDijanjikanUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new PotensiKetercapaianLuaranDijanjikanResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotPotensiKetercapaianLuaranDijanjikanQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
