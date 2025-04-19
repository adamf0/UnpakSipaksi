using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.GetKesesuaianTkt
{
    internal sealed class GetKesesuaianTktDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKesesuaianTktDefaultQuery, KesesuaianTktDefaultResponse>
    {
        public async Task<Result<KesesuaianTktDefaultResponse>> Handle(GetKesesuaianTktDefaultQuery request, CancellationToken cancellationToken)
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
                 FROM kesesuaian_tkt 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KesesuaianTktDefaultResponse?>(sql, new { Uuid = request.KesesuaianTktUuid });
            if (result == null)
            {
                return Result.Failure<KesesuaianTktDefaultResponse>(KesesuaianTktErrors.NotFound(request.KesesuaianTktUuid));
            }

            return result;
        }
    }
}
