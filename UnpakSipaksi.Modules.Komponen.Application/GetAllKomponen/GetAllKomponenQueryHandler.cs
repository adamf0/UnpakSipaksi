using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Komponen.Domain.Komponen;
using UnpakSipaksi.Modules.Komponen.Application.GetKomponen;

namespace UnpakSipaksi.Modules.Komponen.Application.GetAllKomponen
{
    internal sealed class GetAllKomponenQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKomponenQuery, List<KomponenResponse>>
    {
        public async Task<Result<List<KomponenResponse>>> Handle(GetAllKomponenQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama as Nama,
                max_biaya AS MaxBiaya
            FROM komponen
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KomponenResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KomponenResponse>>(KomponenErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
