using MediatR;
using UnpakSipaksi.Common.Domain;

using IRelevansiProdukKepentinganNasionalApi = UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.PublicApi.IRelevansiProdukKepentinganNasionalApi;
using RelevansiProdukKepentinganNasionalResponseApi = UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.PublicApi.RelevansiProdukKepentinganNasionalResponse;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetRelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetBobotRelevansiProdukKepentinganNasional;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Infrastructure.PublicApi
{
    internal sealed class RelevansiProdukKepentinganNasionalApi(ISender sender) : IRelevansiProdukKepentinganNasionalApi
    {
        public async Task<RelevansiProdukKepentinganNasionalResponseApi?> GetAsync(Guid RelevansiProdukKepentinganNasionalUuid, CancellationToken cancellationToken = default)
        {
            Result<RelevansiProdukKepentinganNasionalDefaultResponse> result = await sender.Send(new GetRelevansiProdukKepentinganNasionalDefaultQuery(RelevansiProdukKepentinganNasionalUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new RelevansiProdukKepentinganNasionalResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.BobotPDP,
                result.Value.BobotTerapan,
                result.Value.BobotKerjasama,
                result.Value.BobotPenelitianDasar,
                result.Value.Skor
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotRelevansiProdukKepentinganNasionalQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
