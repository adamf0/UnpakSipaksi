using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian;

namespace UnpakSipaksi.Modules.FokusPenelitian.Application.GetFokusPenelitian
{
    internal sealed class GetFokusPenelitianQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetFokusPenelitianQuery, FokusPenelitianResponse>
    {
        public async Task<Result<FokusPenelitianResponse>> Handle(GetFokusPenelitianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama  
                 FROM bidang_fokus_penelitian
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<FokusPenelitianResponse?>(sql, new { Uuid = request.FokusPenelitianUuid });
            if (result == null)
            {
                return Result.Failure<FokusPenelitianResponse>(FokusPenelitianErrors.NotFound(request.FokusPenelitianUuid));
            }

            return result;
        }
    }
}
