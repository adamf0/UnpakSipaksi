using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSkema.Application.GetKategoriSkema;
using UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.GetAllKategoriSkema
{
    internal sealed class GetAllKategoriSkemaQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKategoriSkemaQuery, List<KategoriSkemaResponse>>
    {
        public async Task<Result<List<KategoriSkemaResponse>>> Handle(GetAllKategoriSkemaQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 rule as Rule
            FROM kategori_skema
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KategoriSkemaResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KategoriSkemaResponse>>(KategoriSkemaErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
