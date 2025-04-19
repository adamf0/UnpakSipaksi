
using MediatR;
using UnpakSipaksi.Common.Domain;

using IArtikelMediaMassaApi = UnpakSipaksi.Modules.ArtikelMediaMassa.PublicApi.IArtikelMediaMassaApi;
using ArtikelMediaMassaResponseApi = UnpakSipaksi.Modules.ArtikelMediaMassa.PublicApi.ArtikelMediaMassaResponse;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetBobotArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetArtikelMediaMassa;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Infrastructure.PublicApi
{
    internal sealed class ArtikelMediaMassaApi(ISender sender) : IArtikelMediaMassaApi
    {
        public async Task<ArtikelMediaMassaResponseApi?> GetAsync(Guid ArtikelMediaMassaUuid, CancellationToken cancellationToken = default)
        {
            Result<ArtikelMediaMassaDefaultResponse> result = await sender.Send(new GetArtikelMediaMassaDefaultQuery(ArtikelMediaMassaUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new ArtikelMediaMassaResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotArtikelMediaMassaQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
