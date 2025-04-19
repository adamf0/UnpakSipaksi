using MediatR;
using UnpakSipaksi.Common.Domain;

using IKejelasanPembagianTugasTimApi = UnpakSipaksi.Modules.KejelasanPembagianTugasTim.PublicApi.IKejelasanPembagianTugasTimApi;
using KejelasanPembagianTugasTimResponseApi = UnpakSipaksi.Modules.KejelasanPembagianTugasTim.PublicApi.KejelasanPembagianTugasTimResponse;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetBobotKejelasanPembagianTugasTim;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Infrastructure.PublicApi
{
    internal sealed class KejelasanPembagianTugasTimApi(ISender sender) : IKejelasanPembagianTugasTimApi
    {
        public async Task<KejelasanPembagianTugasTimResponseApi?> GetAsync(Guid KejelasanPembagianTugasTimUuid, CancellationToken cancellationToken = default)
        {
            Result<KejelasanPembagianTugasTimDefaultResponse> result = await sender.Send(new GetKejelasanPembagianTugasTimDefaultQuery(KejelasanPembagianTugasTimUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KejelasanPembagianTugasTimResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotKejelasanPembagianTugasTimQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
