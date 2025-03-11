using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetKuantitasStatusKi;
using UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetAllKuantitasStatusKi
{
    internal sealed class GetAllKuantitasStatusKiQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKuantitasStatusKiQuery, List<KuantitasStatusKiResponse>>
    {
        public async Task<Result<List<KuantitasStatusKiResponse>>> Handle(GetAllKuantitasStatusKiQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 nilai as Nilai
            FROM kuantitas_status_ki
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KuantitasStatusKiResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KuantitasStatusKiResponse>>(KuantitasStatusKiErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
