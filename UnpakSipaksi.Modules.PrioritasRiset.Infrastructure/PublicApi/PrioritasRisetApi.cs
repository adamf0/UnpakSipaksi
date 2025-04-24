using MediatR;
using UnpakSipaksi.Common.Domain;

using IPrioritasRisetApi = UnpakSipaksi.Modules.PrioritasRiset.PublicApi.IPrioritasRisetApi;
using PrioritasRisetResponseApi = UnpakSipaksi.Modules.PrioritasRiset.PublicApi.PrioritasRisetResponse;
using UnpakSipaksi.Modules.PrioritasRiset.Application.GetPrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.Infrastructure.PublicApi
{
    internal sealed class PrioritasRisetApi(ISender sender) : IPrioritasRisetApi
    {
        public async Task<PrioritasRisetResponseApi?> GetAsync(Guid PrioritasRisetUuid, CancellationToken cancellationToken = default)
        {
            Result<PrioritasRisetDefaultResponse> result = await sender.Send(new GetPrioritasRisetDefaultQuery(PrioritasRisetUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new PrioritasRisetResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }
    }

}
