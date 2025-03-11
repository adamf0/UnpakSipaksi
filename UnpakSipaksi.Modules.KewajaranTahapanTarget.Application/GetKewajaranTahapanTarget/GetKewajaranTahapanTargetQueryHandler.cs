using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetKewajaranTahapanTarget
{
    internal sealed class GetKewajaranTahapanTargetQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKewajaranTahapanTargetQuery, KewajaranTahapanTargetResponse>
    {
        public async Task<Result<KewajaranTahapanTargetResponse>> Handle(GetKewajaranTahapanTargetQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai AS Nilai
                 FROM kewajaran_tahapan_target 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KewajaranTahapanTargetResponse?>(sql, new { Uuid = request.KewajaranTahapanTargetUuid });
            if (result == null)
            {
                return Result.Failure<KewajaranTahapanTargetResponse>(KewajaranTahapanTargetErrors.NotFound(request.KewajaranTahapanTargetUuid));
            }

            return result;
        }
    }
}
