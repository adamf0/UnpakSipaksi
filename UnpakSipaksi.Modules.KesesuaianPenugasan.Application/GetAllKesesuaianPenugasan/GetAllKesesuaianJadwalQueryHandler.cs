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
using UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetKesesuaianPenugasan;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetAllKesesuaianPenugasan
{
    internal sealed class GetAllKesesuaianPenugasanQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKesesuaianPenugasanQuery, List<KesesuaianPenugasanResponse>>
    {
        public async Task<Result<List<KesesuaianPenugasanResponse>>> Handle(GetAllKesesuaianPenugasanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 nilai as Nilai
            FROM kesesuaian_penugasan
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KesesuaianPenugasanResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KesesuaianPenugasanResponse>>(KesesuaianPenugasanErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
