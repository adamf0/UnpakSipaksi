using MediatR;
using UnpakSipaksi.Common.Domain;

using IKategoriSkemaApi = UnpakSipaksi.Modules.KategoriSkema.PublicApi.IKategoriSkemaApi;
using KategoriSkemaResponseApi = UnpakSipaksi.Modules.KategoriSkema.PublicApi.KategoriSkemaResponse;
using UnpakSipaksi.Modules.KategoriSkema.Application.GetKategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.Infrastructure.PublicApi
{
    internal sealed class KategoriSkemaApi(ISender sender) : IKategoriSkemaApi
    {
        public async Task<KategoriSkemaResponseApi?> GetAsync(Guid KategoriSkemaUuid, CancellationToken cancellationToken = default)
        {
            Result<KategoriSkemaDefaultResponse> result = await sender.Send(new GetKategoriSkemaDefaultQuery(KategoriSkemaUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KategoriSkemaResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.RuleObject
            );
        }
    }

}
