using MediatR;
using UnpakSipaksi.Common.Domain;

using IIndikatorCapaianApi = UnpakSipaksi.Modules.IndikatorCapaian.PublicApi.IIndikatorCapaianApi;
using IndikatorCapaianResponseApi = UnpakSipaksi.Modules.IndikatorCapaian.PublicApi.IndikatorCapaianResponse;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.GetIndikatorCapaian;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Infrastructure.PublicApi
{
    internal sealed class IndikatorCapaianApi(ISender sender) : IIndikatorCapaianApi
    {
        public async Task<IndikatorCapaianResponseApi?> GetAsync(Guid IndikatorCapaianUuid, CancellationToken cancellationToken = default)
        {
            Result<IndikatorCapaianDefaultResponse> result = await sender.Send(new GetIndikatorCapaianDefaultQuery(IndikatorCapaianUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new IndikatorCapaianResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.JenisLuaranId,
                result.Value.UuidJenisLuaran,
                result.Value.Nama,
                result.Value.Status
            );
        }
    }

}
