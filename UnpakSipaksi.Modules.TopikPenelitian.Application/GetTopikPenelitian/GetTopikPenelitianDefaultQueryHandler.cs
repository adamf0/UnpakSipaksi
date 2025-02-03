using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.TopikPenelitian.Domain.TopikPenelitian;

namespace UnpakSipaksi.Modules.TopikPenelitian.Application.GetTopikPenelitian
{
    internal sealed class GetTopikPenelitianDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetTopikPenelitianDefaultQuery, TopikPenelitianDefaultResponse>
    {
        public async Task<Result<TopikPenelitianDefaultResponse>> Handle(GetTopikPenelitianDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     `To`.id as Id,
                     CAST(NULLIF(`To`.uuid, '') AS VARCHAR(36)) AS Uuid,
                     `To`.nama AS Nama,
                     `Te`.TemaPenelitianUuid
                 FROM bidang_fokus_penelitian_tema_topik AS `To`
                 LEFT JOIN (
                     SELECT uuid AS TemaPenelitianUuid, id
                     FROM bidang_fokus_penelitian_tema
                 ) AS `Te` ON `Te`.id = `To`.id_bidang_fokus_penelitian_tema
                 WHERE `To`.uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<TopikPenelitianDefaultResponse?>(sql, new { Uuid = request.TopikPenelitianUuid });
            if (result == null)
            {
                return Result.Failure<TopikPenelitianDefaultResponse>(TopikPenelitianErrors.NotFound(request.TopikPenelitianUuid));
            }

            return result;
        }
    }
}
