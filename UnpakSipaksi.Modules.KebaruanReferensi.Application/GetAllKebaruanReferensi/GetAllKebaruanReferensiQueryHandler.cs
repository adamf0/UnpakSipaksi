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
using UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.GetKebaruanReferensi;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Application.GetAllKebaruanReferensi
{
    internal sealed class GetAllKebaruanReferensiQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKebaruanReferensiQuery, List<KebaruanReferensiResponse>>
    {
        public async Task<Result<List<KebaruanReferensiResponse>>> Handle(GetAllKebaruanReferensiQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                name as Nama,
                skor as Skor
            FROM kebaruan_referensi
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KebaruanReferensiResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KebaruanReferensiResponse>>(KebaruanReferensiErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
