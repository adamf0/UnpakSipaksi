using MediatR;
using UnpakSipaksi.Common.Domain;

using ILuaranArtikelApi = UnpakSipaksi.Modules.LuaranArtikel.PublicApi.ILuaranArtikelApi;
using LuaranArtikelResponseApi = UnpakSipaksi.Modules.LuaranArtikel.PublicApi.LuaranArtikelResponse;
using UnpakSipaksi.Modules.LuaranArtikel.Application.GetBobotLuaranArtikel;
using UnpakSipaksi.Modules.LuaranArtikel.Application.GetLuaranArtikel;

namespace UnpakSipaksi.Modules.LuaranArtikel.Infrastructure.PublicApi
{
    internal sealed class LuaranArtikelApi(ISender sender) : ILuaranArtikelApi
    {
        public async Task<LuaranArtikelResponseApi?> GetAsync(Guid LuaranArtikelUuid, CancellationToken cancellationToken = default)
        {
            Result<LuaranArtikelDefaultResponse> result = await sender.Send(new GetLuaranArtikelDefaultQuery(LuaranArtikelUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new LuaranArtikelResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotLuaranArtikelQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
