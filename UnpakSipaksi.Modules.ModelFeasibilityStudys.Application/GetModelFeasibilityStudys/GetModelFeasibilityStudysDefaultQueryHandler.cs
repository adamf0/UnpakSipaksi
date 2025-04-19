using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetModelFeasibilityStudys
{
    internal sealed class GetModelFeasibilityStudysDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetModelFeasibilityStudysDefaultQuery, ModelFeasibilityStudysDefaultResponse>
    {
        public async Task<Result<ModelFeasibilityStudysDefaultResponse>> Handle(GetModelFeasibilityStudysDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     bobot_pdp AS BobotPDP,
                     bobot_terapan AS BobotTerapan,
                     bobot_kerjasama AS BobotKerjasama,
                     bobot_penelitian_dasar AS BobotPenelitianDasar,
                     skor AS Skor 
                 FROM model_feasibility_study 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<ModelFeasibilityStudysDefaultResponse?>(sql, new { Uuid = request.ModelFeasibilityStudysUuid });
            if (result == null)
            {
                return Result.Failure<ModelFeasibilityStudysDefaultResponse>(ModelFeasibilityStudysErrors.NotFound(request.ModelFeasibilityStudysUuid));
            }

            return result;
        }
    }
}
