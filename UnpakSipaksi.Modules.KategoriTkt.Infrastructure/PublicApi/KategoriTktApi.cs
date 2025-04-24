using MediatR;
using UnpakSipaksi.Common.Domain;

using IKategoriTktApi = UnpakSipaksi.Modules.KategoriTkt.PublicApi.IKategoriTktApi;
using KategoriTktResponseApi = UnpakSipaksi.Modules.KategoriTkt.PublicApi.KategoriTktResponse;
using UnpakSipaksi.Modules.KategoriTkt.Application.GetBobotKategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Application.GetKategoriTkt;

namespace UnpakSipaksi.Modules.KategoriTkt.Infrastructure.PublicApi
{
    internal sealed class KategoriTktApi(ISender sender) : IKategoriTktApi
    {
        public async Task<KategoriTktResponseApi?> GetAsync(Guid KategoriTktUuid, CancellationToken cancellationToken = default)
        {
            Result<KategoriTktDefaultResponse> result = await sender.Send(new GetKategoriTktDefaultQuery(KategoriTktUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KategoriTktResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotKategoriTktQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
