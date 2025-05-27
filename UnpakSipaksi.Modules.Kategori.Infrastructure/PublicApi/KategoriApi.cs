
using MediatR;
using UnpakSipaksi.Common.Domain;

using IKategoriApi = UnpakSipaksi.Modules.Kategori.PublicApi.IKategoriApi;
using KategoriResponseApi = UnpakSipaksi.Modules.Kategori.PublicApi.KategoriResponse;
using UnpakSipaksi.Modules.Kategori.Application.GetKategori;

namespace UnpakSipaksi.Modules.Kategori.Infrastructure.PublicApi
{
    internal sealed class KategoriApi(ISender sender) : IKategoriApi
    {
        public async Task<KategoriResponseApi?> GetAsync(Guid KategoriUuid, CancellationToken cancellationToken = default)
        {
            Result<KategoriDefaultResponse> result = await sender.Send(new GetKategoriDefaultQuery(KategoriUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KategoriResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }
    }

}
