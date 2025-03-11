using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetKuantitasStatusKi
{
    internal sealed class GetKuantitasStatusKiQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKuantitasStatusKiQuery, KuantitasStatusKiResponse>
    {
        public async Task<Result<KuantitasStatusKiResponse>> Handle(GetKuantitasStatusKiQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai as Nilai
                 FROM kuantitas_status_ki 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KuantitasStatusKiResponse?>(sql, new { Uuid = request.KuantitasStatusKiUuid });
            if (result == null)
            {
                return Result.Failure<KuantitasStatusKiResponse>(KuantitasStatusKiErrors.NotFound(request.KuantitasStatusKiUuid));
            }

            return result;
        }
    }
}
