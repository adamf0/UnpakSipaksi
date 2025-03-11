using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetKewajaranTahapanTarget;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetAllKewajaranTahapanTarget
{
    internal sealed class GetAllKewajaranTahapanTargetQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKewajaranTahapanTargetQuery, List<KewajaranTahapanTargetResponse>>
    {
        public async Task<Result<List<KewajaranTahapanTargetResponse>>> Handle(GetAllKewajaranTahapanTargetQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama as Nama,
                nilai AS Nilai
            FROM kewajaran_tahapan_target
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KewajaranTahapanTargetResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KewajaranTahapanTargetResponse>>(KewajaranTahapanTargetErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
