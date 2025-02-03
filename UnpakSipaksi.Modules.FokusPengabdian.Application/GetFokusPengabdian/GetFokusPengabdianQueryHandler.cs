using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian;

namespace UnpakSipaksi.Modules.FokusPengabdian.Application.GetFokusPengabdian
{
    internal sealed class GetFokusPengabdianQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetFokusPengabdianQuery, FokusPengabdianResponse>
    {
        public async Task<Result<FokusPengabdianResponse>> Handle(GetFokusPengabdianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama 
                 FROM fokus_pengabdian 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<FokusPengabdianResponse?>(sql, new { Uuid = request.FokusPengabdianUuid });
            if (result == null)
            {
                return Result.Failure<FokusPengabdianResponse>(FokusPengabdianErrors.NotFound(request.FokusPengabdianUuid));
            }

            return result;
        }
    }
}
