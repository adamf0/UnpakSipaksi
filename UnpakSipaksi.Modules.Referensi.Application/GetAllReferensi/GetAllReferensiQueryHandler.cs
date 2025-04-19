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
using UnpakSipaksi.Modules.Referensi.Application.GetReferensi;
using UnpakSipaksi.Modules.Referensi.Application.GetReferensi;
using UnpakSipaksi.Modules.Referensi.Domain.Referensi;

namespace UnpakSipaksi.Modules.Referensi.Application.GetAllReferensi
{
    internal sealed class GetAllReferensiQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllReferensiQuery, List<ReferensiResponse>>
    {
        public async Task<Result<List<ReferensiResponse>>> Handle(GetAllReferensiQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(r.uuid, '') AS VARCHAR(36)) AS Uuid,
                r.name as Nama,
                kr.uuid as UuidKebaruanReferensi,
                rkr.uuid AS UuidRelevansiKualitasReferensi,
                r.skor AS Skor 
            FROM referensi r 
            LEFT JOIN kebaruan_referensi kr ON r.id_kebaruan_referensi = kr.id 
            LEFT JOIN relevansi_kualitas_referensi rkr ON r.id_relevansi_kualitas_referensi = rkr.id
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<ReferensiResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<ReferensiResponse>>(ReferensiErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
