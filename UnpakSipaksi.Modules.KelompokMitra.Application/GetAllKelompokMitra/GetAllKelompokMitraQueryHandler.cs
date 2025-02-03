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
using UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Application.GetKelompokMitra;

namespace UnpakSipaksi.Modules.KelompokMitra.Application.GetAllKelompokMitra
{
    internal sealed class GetAllKelompokMitraQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKelompokMitraQuery, List<KelompokMitraResponse>>
    {
        public async Task<Result<List<KelompokMitraResponse>>> Handle(GetAllKelompokMitraQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama as Nama
            FROM kelompokmitra
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KelompokMitraResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KelompokMitraResponse>>(KelompokMitraErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
