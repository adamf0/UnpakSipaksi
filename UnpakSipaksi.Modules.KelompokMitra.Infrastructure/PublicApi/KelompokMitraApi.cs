
using MediatR;
using UnpakSipaksi.Common.Domain;

using IKelompokMitraApi = UnpakSipaksi.Modules.KelompokMitra.PublicApi.IKelompokMitraApi;
using KelompokMitraResponseApi = UnpakSipaksi.Modules.KelompokMitra.PublicApi.KelompokMitraResponse;
using UnpakSipaksi.Modules.KelompokMitra.Application.GetKelompokMitra;

namespace UnpakSipaksi.Modules.KelompokMitra.Infrastructure.PublicApi
{
    internal sealed class KelompokMitraApi(ISender sender) : IKelompokMitraApi
    {
        public async Task<KelompokMitraResponseApi?> GetAsync(Guid KelompokMitraUuid, CancellationToken cancellationToken = default)
        {
            Result<KelompokMitraDefaultResponse> result = await sender.Send(new GetKelompokMitraDefaultQuery(KelompokMitraUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KelompokMitraResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }
    }

}
