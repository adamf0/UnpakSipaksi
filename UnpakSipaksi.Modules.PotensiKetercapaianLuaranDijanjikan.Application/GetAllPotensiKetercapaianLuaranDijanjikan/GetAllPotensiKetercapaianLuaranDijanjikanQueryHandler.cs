using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetPotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetAllPotensiKetercapaianLuaranDijanjikan
{
    internal sealed class GetAllPotensiKetercapaianLuaranDijanjikanQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllPotensiKetercapaianLuaranDijanjikanQuery, List<PotensiKetercapaianLuaranDijanjikanResponse>>
    {
        public async Task<Result<List<PotensiKetercapaianLuaranDijanjikanResponse>>> Handle(GetAllPotensiKetercapaianLuaranDijanjikanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                name as Nama,
                skor AS Skor 
            FROM potensi_ketercapaian_luaran_dijanjikan
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<PotensiKetercapaianLuaranDijanjikanResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<PotensiKetercapaianLuaranDijanjikanResponse>>(PotensiKetercapaianLuaranDijanjikanErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
