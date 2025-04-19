
using MediatR;
using UnpakSipaksi.Common.Domain;

using IMetodeRencanaKegiatanApi = UnpakSipaksi.Modules.MetodeRencanaKegiatan.PublicApi.IMetodeRencanaKegiatanApi;
using MetodeRencanaKegiatanResponseApi = UnpakSipaksi.Modules.MetodeRencanaKegiatan.PublicApi.MetodeRencanaKegiatanResponse;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetMetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetBobotMetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Infrastructure.PublicApi
{
    internal sealed class MetodeRencanaKegiatanApi(ISender sender) : IMetodeRencanaKegiatanApi
    {
        public async Task<MetodeRencanaKegiatanResponseApi?> GetAsync(Guid MetodeRencanaKegiatanUuid, CancellationToken cancellationToken = default)
        {
            Result<MetodeRencanaKegiatanDefaultResponse> result = await sender.Send(new GetMetodeRencanaKegiatanDefaultQuery(MetodeRencanaKegiatanUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new MetodeRencanaKegiatanResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotMetodeRencanaKegiatanQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
