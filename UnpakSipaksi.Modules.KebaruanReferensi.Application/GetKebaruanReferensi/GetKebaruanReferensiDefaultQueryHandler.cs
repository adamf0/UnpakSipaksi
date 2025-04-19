using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Application.GetKebaruanReferensi
{
    internal sealed class GetKebaruanReferensiDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKebaruanReferensiDefaultQuery, KebaruanReferensiDefaultResponse>
    {
        public async Task<Result<KebaruanReferensiDefaultResponse>> Handle(GetKebaruanReferensiDefaultQuery request, CancellationToken cancellationToken)
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
                     skor as Skor
                 FROM kebaruan_referensi
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KebaruanReferensiDefaultResponse?>(sql, new { Uuid = request.KebaruanReferensiUuid });
            if (result == null)
            {
                return Result.Failure<KebaruanReferensiDefaultResponse>(KebaruanReferensiErrors.NotFound(request.KebaruanReferensiUuid));
            }

            return result;
        }
    }
}
