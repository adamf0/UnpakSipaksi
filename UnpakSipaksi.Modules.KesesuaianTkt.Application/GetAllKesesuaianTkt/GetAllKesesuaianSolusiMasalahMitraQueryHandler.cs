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
using UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.GetKesesuaianTkt;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.GetAllKesesuaianTkt
{
    internal sealed class GetAllKesesuaianTktQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKesesuaianTktQuery, List<KesesuaianTktResponse>>
    {
        public async Task<Result<List<KesesuaianTktResponse>>> Handle(GetAllKesesuaianTktQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 name as Nama,
                 skor as Skor
            FROM kesesuaian_tkt
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KesesuaianTktResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KesesuaianTktResponse>>(KesesuaianTktErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
