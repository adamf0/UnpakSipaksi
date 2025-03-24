using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.GetLuaranArtikel
{
    internal sealed class GetLuaranArtikelQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetLuaranArtikelQuery, LuaranArtikelResponse>
    {
        public async Task<Result<LuaranArtikelResponse>> Handle(GetLuaranArtikelQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai as Nilai
                 FROM luaran_artikel 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<LuaranArtikelResponse?>(sql, new { Uuid = request.LuaranArtikelUuid });
            if (result == null)
            {
                return Result.Failure<LuaranArtikelResponse>(LuaranArtikelErrors.NotFound(request.LuaranArtikelUuid));
            }

            return result;
        }
    }
}
