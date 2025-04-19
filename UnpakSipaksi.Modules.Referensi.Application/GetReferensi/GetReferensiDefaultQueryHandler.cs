using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Referensi.Domain.Referensi;

namespace UnpakSipaksi.Modules.Referensi.Application.GetReferensi
{
    internal sealed class GetReferensiDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetReferensiDefaultQuery, ReferensiDefaultResponse>
    {
        public async Task<Result<ReferensiDefaultResponse>> Handle(GetReferensiDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     r.id as Id,
                     CAST(NULLIF(r.uuid, '') AS VARCHAR(36)) AS Uuid,
                     r.name as Nama,
                     kr.uuid as UuidKebaruanReferensi,
                     r.id_kebaruan_referensi AS KebaruanReferensiId,
                     rkr.uuid AS UuidRelevansiKualitasReferensi,
                     r.id_relevansi_kualitas_referensi AS RelevansiKualitasReferensiId,
                     r.skor AS Skor 
                 FROM referensi r 
                 LEFT JOIN kebaruan_referensi kr ON r.id_kebaruan_referensi = kr.id 
                 LEFT JOIN relevansi_kualitas_referensi rkr ON r.id_relevansi_kualitas_referensi = rkr.id 
                 WHERE r.uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<ReferensiDefaultResponse?>(sql, new { Uuid = request.ReferensiUuid });
            if (result == null)
            {
                return Result.Failure<ReferensiDefaultResponse>(ReferensiErrors.NotFound(request.ReferensiUuid));
            }

            return result;
        }
    }
}
