

using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetKesesuaianPenugasan
{
    internal sealed class GetKesesuaianPenugasanDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKesesuaianPenugasanDefaultQuery, KesesuaianPenugasanDefaultResponse>
    {
        public async Task<Result<KesesuaianPenugasanDefaultResponse>> Handle(GetKesesuaianPenugasanDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     nilai AS Nilai 
                 FROM kesesuaian_penugasan 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KesesuaianPenugasanDefaultResponse?>(sql, new { Uuid = request.KesesuaianPenugasanUuid });
            if (result == null)
            {
                return Result.Failure<KesesuaianPenugasanDefaultResponse>(KesesuaianPenugasanErrors.NotFound(request.KesesuaianPenugasanUuid));
            }

            return result;
        }
    }
}
