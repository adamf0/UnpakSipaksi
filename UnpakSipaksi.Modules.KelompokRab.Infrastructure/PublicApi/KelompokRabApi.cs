using MediatR;
using UnpakSipaksi.Common.Domain;

using IKelompokRabApi = UnpakSipaksi.Modules.KelompokRab.PublicApi.IKelompokRabApi;
using KelompokRabResponseApi = UnpakSipaksi.Modules.KelompokRab.PublicApi.KelompokRabResponse;
using UnpakSipaksi.Modules.KelompokRab.Application.GetKelompokRab;

namespace UnpakSipaksi.Modules.KelompokRab.Infrastructure.PublicApi
{
    internal sealed class KelompokRabApi(ISender sender) : IKelompokRabApi
    {
        public async Task<KelompokRabResponseApi?> GetAsync(Guid KelompokRabUuid, CancellationToken cancellationToken = default)
        {
            Result<KelompokRabDefaultResponse> result = await sender.Send(new GetKelompokRabDefaultQuery(KelompokRabUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KelompokRabResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }
    }

}
