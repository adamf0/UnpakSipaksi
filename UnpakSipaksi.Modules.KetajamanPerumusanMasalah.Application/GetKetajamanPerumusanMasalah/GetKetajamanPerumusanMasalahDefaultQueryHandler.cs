using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.GetKetajamanPerumusanMasalah
{
    internal sealed class GetKetajamanPerumusanMasalahDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKetajamanPerumusanMasalahDefaultQuery, KetajamanPerumusanMasalahDefaultResponse>
    {
        public async Task<Result<KetajamanPerumusanMasalahDefaultResponse>> Handle(GetKetajamanPerumusanMasalahDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     bobot_pdp AS BobotPDP,
                     bobot_terapan AS BobotTerapan,
                     bobot_kerjasama AS BobotKerjasama,
                     bobot_penelitian_dasar AS BobotPenelitianDasar,
                     skor AS Skor 
                 FROM jumlah_kolaborator_publikasi_bereputasi 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KetajamanPerumusanMasalahDefaultResponse?>(sql, new { Uuid = request.KetajamanPerumusanMasalahUuid });
            if (result == null)
            {
                return Result.Failure<KetajamanPerumusanMasalahDefaultResponse>(KetajamanPerumusanMasalahErrors.NotFound(request.KetajamanPerumusanMasalahUuid));
            }

            return result;
        }
    }
}
