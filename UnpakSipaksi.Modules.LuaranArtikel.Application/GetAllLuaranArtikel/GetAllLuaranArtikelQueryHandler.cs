using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LuaranArtikel.Application.GetLuaranArtikel;
using UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.GetAllLuaranArtikel
{
    internal sealed class GetAllLuaranArtikelQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllLuaranArtikelQuery, List<LuaranArtikelResponse>>
    {
        public async Task<Result<List<LuaranArtikelResponse>>> Handle(GetAllLuaranArtikelQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 nilai as Nilai
            FROM luaran_artikel
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<LuaranArtikelResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<LuaranArtikelResponse>>(LuaranArtikelErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
