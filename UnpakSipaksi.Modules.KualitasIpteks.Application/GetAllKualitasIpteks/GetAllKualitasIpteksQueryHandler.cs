using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasIpteks.Application.GetKualitasIpteks;
using UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.GetAllKualitasIpteks
{
    internal sealed class GetAllKualitasIpteksQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKualitasIpteksQuery, List<KualitasIpteksResponse>>
    {
        public async Task<Result<List<KualitasIpteksResponse>>> Handle(GetAllKualitasIpteksQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 nilai as Nilai
            FROM kualitas_ipteks
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KualitasIpteksResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KualitasIpteksResponse>>(KualitasIpteksErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
