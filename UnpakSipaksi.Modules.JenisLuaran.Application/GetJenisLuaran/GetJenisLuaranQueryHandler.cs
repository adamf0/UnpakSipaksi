using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisLuaran.Domain;

namespace UnpakSipaksi.Modules.JenisLuaran.Application.GetJenisLuaran
{
    internal sealed class GetJenisLuaranQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetJenisLuaranQuery, JenisLuaranResponse>
    {
        public async Task<Result<JenisLuaranResponse>> Handle(GetJenisLuaranQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama 
                 FROM pkm_jenis_luaran 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<JenisLuaranResponse?>(sql, new { Uuid = request.JenisLuaranUuid });
            if (result == null)
            {
                return Result.Failure<JenisLuaranResponse>(JenisLuaranErrors.NotFound(request.JenisLuaranUuid));
            }

            return result;
        }
    }
}
