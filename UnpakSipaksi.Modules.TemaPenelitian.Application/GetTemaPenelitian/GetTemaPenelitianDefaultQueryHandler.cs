using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.TemaPenelitian.Domain.TemaPenelitian;

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.GetTemaPenelitian
{
    internal sealed class GetTemaPenelitianDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetTemaPenelitianDefaultQuery, TemaPenelitianDefaultResponse>
    {
        public async Task<Result<TemaPenelitianDefaultResponse>> Handle(GetTemaPenelitianDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     T.id as Id,
                     CAST(NULLIF(T.uuid, '') AS VARCHAR(36)) AS Uuid,
                     T.nama AS Nama,
                     F.FokusPenelitianUuid as FokusPenelitianUuid
                 FROM bidang_fokus_penelitian_tema AS T
                 LEFT JOIN (
                     SELECT uuid AS FokusPenelitianUuid, id
                     FROM bidang_fokus_penelitian
                 ) AS F ON F.id = T.id_bidang_fokus_penelitian
                 WHERE T.uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<TemaPenelitianDefaultResponse?>(sql, new { Uuid = request.TemaPenelitianUuid });
            if (result == null)
            {
                return Result.Failure<TemaPenelitianDefaultResponse>(TemaPenelitianErrors.NotFound(request.TemaPenelitianUuid));
            }

            return result;
        }
    }
}
