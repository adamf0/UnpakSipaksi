
using MediatR;
using UnpakSipaksi.Common.Domain;

using IKategoriLuaranApi = UnpakSipaksi.Modules.KategoriLuaran.PublicApi.IKategoriLuaranApi;
using KategoriLuaranResponseApi = UnpakSipaksi.Modules.KategoriLuaran.PublicApi.KategoriLuaranResponse;
using UnpakSipaksi.Modules.KategoriLuaran.Application.GetKategoriLuaran;

namespace UnpakSipaksi.Modules.KategoriLuaran.Infrastructure.PublicApi
{
    internal sealed class KategoriLuaranApi(ISender sender) : IKategoriLuaranApi
    {
        public async Task<KategoriLuaranResponseApi?> GetAsync(Guid KategoriUuid, CancellationToken cancellationToken = default)
        {
            Result<KategoriLuaranDefaultResponse> result = await sender.Send(new GetKategoriLuaranDefaultQuery(KategoriUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KategoriLuaranResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.uuidKategori,
                result.Value.KategoriId,
                result.Value.Nama,
                result.Value.Status
            );
        }
    }

}
