using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.GetSotaKebaharuan
{
    internal sealed class GetSotaKebaharuanQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSotaKebaharuanQuery, SotaKebaharuanResponse>
    {
        public async Task<Result<SotaKebaharuanResponse>> Handle(GetSotaKebaharuanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     bobot_pdp AS BobotPDP,
                     bobot_terapan AS BobotTerapan,
                     bobot_kerjasama AS BobotKerjasama,
                     bobot_penelitian_dasar AS BobotPenelitianDasar,
                     skor AS Skor 
                 FROM sota_kebaharuan 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<SotaKebaharuanResponse?>(sql, new { Uuid = request.SotaKebaharuanUuid });
            if (result == null)
            {
                return Result.Failure<SotaKebaharuanResponse>(SotaKebaharuanErrors.NotFound(request.SotaKebaharuanUuid));
            }

            return result;
        }
    }
}
