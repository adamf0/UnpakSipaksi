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
using UnpakSipaksi.Modules.KelompokRab.Domain.KelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Application.GetKelompokRab;

namespace UnpakSipaksi.Modules.KelompokRab.Application.GetAllKelompokRab
{
    internal sealed class GetAllKelompokRabQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKelompokRabQuery, List<KelompokRabResponse>>
    {
        public async Task<Result<List<KelompokRabResponse>>> Handle(GetAllKelompokRabQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama as Nama
            FROM kelompok_rab
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KelompokRabResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KelompokRabResponse>>(KelompokRabErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
