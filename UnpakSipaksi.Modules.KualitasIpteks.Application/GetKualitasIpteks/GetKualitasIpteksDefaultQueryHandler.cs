

using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.GetKualitasIpteks
{
    internal sealed class GetKualitasIpteksDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKualitasIpteksDefaultQuery, KualitasIpteksDefaultResponse>
    {
        public async Task<Result<KualitasIpteksDefaultResponse>> Handle(GetKualitasIpteksDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     nilai AS Nilai 
                 FROM kualitas_ipteks 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KualitasIpteksDefaultResponse?>(sql, new { Uuid = request.KualitasIpteksUuid });
            if (result == null)
            {
                return Result.Failure<KualitasIpteksDefaultResponse>(KualitasIpteksErrors.NotFound(request.KualitasIpteksUuid));
            }

            return result;
        }
    }
}
