using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetRelevansiKualitasReferensi
{
    internal sealed class GetRelevansiKualitasReferensiDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRelevansiKualitasReferensiDefaultQuery, RelevansiKualitasReferensiDefaultResponse>
    {
        public async Task<Result<RelevansiKualitasReferensiDefaultResponse>> Handle(GetRelevansiKualitasReferensiDefaultQuery request, CancellationToken cancellationToken)
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
                 FROM relevansi_kualitas_referensi 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<RelevansiKualitasReferensiDefaultResponse?>(sql, new { Uuid = request.RelevansiKualitasReferensiUuid });
            if (result == null)
            {
                return Result.Failure<RelevansiKualitasReferensiDefaultResponse>(RelevansiKualitasReferensiErrors.NotFound(request.RelevansiKualitasReferensiUuid));
            }

            return result;
        }
    }
}
