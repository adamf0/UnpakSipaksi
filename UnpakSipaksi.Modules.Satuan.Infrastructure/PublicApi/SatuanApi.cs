using MediatR;
using UnpakSipaksi.Common.Domain;

using ISatuanApi = UnpakSipaksi.Modules.Satuan.PublicApi.ISatuanApi;
using SatuanResponseApi = UnpakSipaksi.Modules.Satuan.PublicApi.SatuanResponse;
using UnpakSipaksi.Modules.Satuan.Application.GetSatuan;

namespace UnpakSipaksi.Modules.Satuan.Infrastructure.PublicApi
{
    internal sealed class SatuanApi(ISender sender) : ISatuanApi
    {
        public async Task<SatuanResponseApi?> GetAsync(Guid SatuanUuid, CancellationToken cancellationToken = default)
        {
            Result<SatuanDefaultResponse> result = await sender.Send(new GetSatuanDefaultQuery(SatuanUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new SatuanResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }
    }

}
