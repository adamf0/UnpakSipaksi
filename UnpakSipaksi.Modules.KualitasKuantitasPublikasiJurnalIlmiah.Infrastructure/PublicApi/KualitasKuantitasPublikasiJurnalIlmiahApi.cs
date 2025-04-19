using MediatR;
using UnpakSipaksi.Common.Domain;

using IKualitasKuantitasPublikasiJurnalIlmiahApi = UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.PubliApi.IKualitasKuantitasPublikasiJurnalIlmiahApi;
using KualitasKuantitasPublikasiJurnalIlmiahResponseApi = UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.PubliApi.KualitasKuantitasPublikasiJurnalIlmiahResponse;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.GetKualitasKuantitasPublikasiJurnalIlmiah;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.GetBobotKualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Infrastructure.PublicApi
{
    internal sealed class KualitasKuantitasPublikasiJurnalIlmiahApi(ISender sender) : IKualitasKuantitasPublikasiJurnalIlmiahApi
    {
        public async Task<KualitasKuantitasPublikasiJurnalIlmiahResponseApi?> GetAsync(Guid KualitasKuantitasPublikasiJurnalIlmiahUuid, CancellationToken cancellationToken = default)
        {
            Result<KualitasKuantitasPublikasiJurnalIlmiahDefaultResponse> result = await sender.Send(new GetKualitasKuantitasPublikasiJurnalIlmiahDefaultQuery(KualitasKuantitasPublikasiJurnalIlmiahUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KualitasKuantitasPublikasiJurnalIlmiahResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotKualitasKuantitasPublikasiJurnalIlmiahQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
