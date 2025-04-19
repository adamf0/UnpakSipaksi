using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetModelFeasibilityStudys
{
    internal sealed class GetModelFeasibilityStudysQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetModelFeasibilityStudysQuery, ModelFeasibilityStudysResponse>
    {
        public async Task<Result<ModelFeasibilityStudysResponse>> Handle(GetModelFeasibilityStudysQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     skor AS Skor 
                 FROM model_feasibility_study 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<ModelFeasibilityStudysResponse?>(sql, new { Uuid = request.ModelFeasibilityStudysUuid });
            if (result == null)
            {
                return Result.Failure<ModelFeasibilityStudysResponse>(ModelFeasibilityStudysErrors.NotFound(request.ModelFeasibilityStudysUuid));
            }

            return result;
        }
    }
}
