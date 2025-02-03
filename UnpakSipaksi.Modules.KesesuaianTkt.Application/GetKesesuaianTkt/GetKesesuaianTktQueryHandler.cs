using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.GetKesesuaianTkt
{
    internal sealed class GetKesesuaianTktQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKesesuaianTktQuery, KesesuaianTktResponse>
    {
        public async Task<Result<KesesuaianTktResponse>> Handle(GetKesesuaianTktQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     skor as Skor
                 FROM kesesuaian_tkt 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KesesuaianTktResponse?>(sql, new { Uuid = request.KesesuaianTktUuid });
            if (result == null)
            {
                return Result.Failure<KesesuaianTktResponse>(KesesuaianTktErrors.NotFound(request.KesesuaianTktUuid));
            }

            return result;
        }
    }
}
