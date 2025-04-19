using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.GetBobotLuaranArtikel
{
    internal sealed class GetBobotLuaranArtikelQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotLuaranArtikelQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotLuaranArtikelQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM luaran_artikel
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(LuaranArtikelErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(LuaranArtikelErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
