using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.TopikPenelitian.Domain.TopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.Application.GetTopikPenelitian;

namespace UnpakSipaksi.Modules.TopikPenelitian.Application.GetAllTopikPenelitian
{
    internal sealed class GetAllTopikPenelitianQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllTopikPenelitianQuery, List<TopikPenelitianResponse>>
    {
        public async Task<Result<List<TopikPenelitianResponse>>> Handle(GetAllTopikPenelitianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(`To`.uuid, '') AS VARCHAR(36)) AS Uuid,
                `To`.nama AS Nama,
                `Te`.TemaPenelitianUuid
            FROM bidang_fokus_penelitian_tema_topik AS `To`
            LEFT JOIN (
                SELECT uuid AS TemaPenelitianUuid, id
                FROM bidang_fokus_penelitian_tema
            ) AS `Te` ON `Te`.id = `To`.id_bidang_fokus_penelitian_tema
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<TopikPenelitianResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<TopikPenelitianResponse>>(TopikPenelitianErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
