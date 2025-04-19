using MediatR;
using UnpakSipaksi.Common.Domain;

using IInovasiPemecahanMasalahApi = UnpakSipaksi.Modules.InovasiPemecahanMasalah.PublicApi.IInovasiPemecahanMasalahApi;
using InovasiPemecahanMasalahResponseApi = UnpakSipaksi.Modules.InovasiPemecahanMasalah.PublicApi.InovasiPemecahanMasalahResponse;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.GetInovasiPemecahanMasalah;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.GetBobotInovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Infrastructure.PublicApi
{
    internal sealed class InovasiPemecahanMasalahApi(ISender sender) : IInovasiPemecahanMasalahApi
    {
        public async Task<InovasiPemecahanMasalahResponseApi?> GetAsync(Guid InovasiPemecahanMasalahUuid, CancellationToken cancellationToken = default)
        {
            Result<InovasiPemecahanMasalahDefaultResponse> result = await sender.Send(new GetInovasiPemecahanMasalahDefaultQuery(InovasiPemecahanMasalahUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new InovasiPemecahanMasalahResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotInovasiPemecahanMasalahQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
