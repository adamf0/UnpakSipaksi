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
using UnpakSipaksi.Modules.TemaPenelitian.Application.GetTemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Domain.TemaPenelitian;

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.GetAllTemaPenelitian
{
    internal sealed class GetAllTemaPenelitianQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllTemaPenelitianQuery, List<TemaPenelitianResponse>>
    {
        public async Task<Result<List<TemaPenelitianResponse>>> Handle(GetAllTemaPenelitianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(T.uuid, '') AS VARCHAR(36)) AS Uuid,
                T.nama AS Nama,
                F.FokusPenelitianUuid as FokusPenelitianUuid
            FROM bidang_fokus_penelitian_tema AS T
            LEFT JOIN (
                SELECT uuid AS FokusPenelitianUuid, id
                FROM bidang_fokus_penelitian
            ) AS F ON F.id = T.id_bidang_fokus_penelitian
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<TemaPenelitianResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<TemaPenelitianResponse>>(TemaPenelitianErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
