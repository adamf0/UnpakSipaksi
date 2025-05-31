using MediatR;
using UnpakSipaksi.Common.Domain;

using IKomponenApi = UnpakSipaksi.Modules.Komponen.PublicApi.IKomponenApi;
using KomponenResponseApi = UnpakSipaksi.Modules.Komponen.PublicApi.KomponenResponse;
using UnpakSipaksi.Modules.Komponen.Application.GetKomponen;

namespace UnpakSipaksi.Modules.Komponen.Infrastructure.PublicApi
{
    internal sealed class KomponenApi(ISender sender) : IKomponenApi
    {
        public async Task<KomponenResponseApi?> GetAsync(Guid KomponenUuid, CancellationToken cancellationToken = default)
        {
            Result<KomponenDefaultResponse> result = await sender.Send(new GetKomponenDefaultQuery(KomponenUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KomponenResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.MaxBiaya
            );
        }
    }
}
