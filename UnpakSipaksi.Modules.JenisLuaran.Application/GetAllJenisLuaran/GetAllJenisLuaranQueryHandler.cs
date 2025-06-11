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
using UnpakSipaksi.Modules.JenisLuaran.Application.GetJenisLuaran;
using UnpakSipaksi.Modules.JenisLuaran.Domain;

namespace UnpakSipaksi.Modules.JenisLuaran.Application.GetAllJenisLuaran
{
    internal sealed class GetAllJenisLuaranQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllJenisLuaranQuery, List<JenisLuaranResponse>>
    {
        public async Task<Result<List<JenisLuaranResponse>>> Handle(GetAllJenisLuaranQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama as Nama 
            FROM pkm_jenis_luaran 
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<JenisLuaranResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<JenisLuaranResponse>>(JenisLuaranErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
