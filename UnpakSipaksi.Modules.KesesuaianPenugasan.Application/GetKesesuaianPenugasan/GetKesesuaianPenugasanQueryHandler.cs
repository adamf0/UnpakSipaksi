using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetKesesuaianPenugasan
{
    internal sealed class GetKesesuaianPenugasanQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKesesuaianPenugasanQuery, KesesuaianPenugasanResponse>
    {
        public async Task<Result<KesesuaianPenugasanResponse>> Handle(GetKesesuaianPenugasanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai as Nilai
                 FROM kesesuaian_penugasan 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KesesuaianPenugasanResponse?>(sql, new { Uuid = request.KesesuaianPenugasanUuid });
            if (result == null)
            {
                return Result.Failure<KesesuaianPenugasanResponse>(KesesuaianPenugasanErrors.NotFound(request.KesesuaianPenugasanUuid));
            }

            return result;
        }
    }
}
