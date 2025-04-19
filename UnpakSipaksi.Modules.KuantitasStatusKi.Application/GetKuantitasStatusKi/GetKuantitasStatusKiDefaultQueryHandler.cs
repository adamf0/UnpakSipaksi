using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetKuantitasStatusKi
{
    internal sealed class GetKuantitasStatusKiDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKuantitasStatusKiDefaultQuery, KuantitasStatusKiDefaultResponse>
    {
        public async Task<Result<KuantitasStatusKiDefaultResponse>> Handle(GetKuantitasStatusKiDefaultQuery request, CancellationToken cancellationToken)
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
                 FROM kuantitas_status_ki 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KuantitasStatusKiDefaultResponse?>(sql, new { Uuid = request.KuantitasStatusKiUuid });
            if (result == null)
            {
                return Result.Failure<KuantitasStatusKiDefaultResponse>(KuantitasStatusKiErrors.NotFound(request.KuantitasStatusKiUuid));
            }

            return result;
        }
    }
}
