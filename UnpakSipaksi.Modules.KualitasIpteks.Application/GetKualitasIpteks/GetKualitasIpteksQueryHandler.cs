using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.GetKualitasIpteks
{
    internal sealed class GetKualitasIpteksQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKualitasIpteksQuery, KualitasIpteksResponse>
    {
        public async Task<Result<KualitasIpteksResponse>> Handle(GetKualitasIpteksQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai as Nilai
                 FROM kualitas_ipteks 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KualitasIpteksResponse?>(sql, new { Uuid = request.KualitasIpteksUuid });
            if (result == null)
            {
                return Result.Failure<KualitasIpteksResponse>(KualitasIpteksErrors.NotFound(request.KualitasIpteksUuid));
            }

            return result;
        }
    }
}
