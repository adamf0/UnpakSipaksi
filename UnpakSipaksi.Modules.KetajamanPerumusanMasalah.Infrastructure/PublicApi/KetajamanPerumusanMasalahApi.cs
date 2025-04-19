using MediatR;
using UnpakSipaksi.Common.Domain;

using IKetajamanPerumusanMasalahApi = UnpakSipaksi.Modules.KetajamanPerumusanMasalah.PublicApi.IKetajamanPerumusanMasalahApi;
using KetajamanPerumusanMasalahResponseApi = UnpakSipaksi.Modules.KetajamanPerumusanMasalah.PublicApi.KetajamanPerumusanMasalahResponse;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.GetKetajamanPerumusanMasalah;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.GetBobotKetajamanPerumusanMasalah;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Infrastructure.PublicApi
{
    internal sealed class KetajamanPerumusanMasalahApi(ISender sender) : IKetajamanPerumusanMasalahApi
    {
        public async Task<KetajamanPerumusanMasalahResponseApi?> GetAsync(Guid KetajamanPerumusanMasalahUuid, CancellationToken cancellationToken = default)
        {
            Result<KetajamanPerumusanMasalahDefaultResponse> result = await sender.Send(new GetKetajamanPerumusanMasalahDefaultQuery(KetajamanPerumusanMasalahUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KetajamanPerumusanMasalahResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotKetajamanPerumusanMasalahQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
