using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetModelFeasibilityStudys;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetAllModelFeasibilityStudys
{
    internal sealed class GetAllModelFeasibilityStudysQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllModelFeasibilityStudysQuery, List<ModelFeasibilityStudysResponse>>
    {
        public async Task<Result<List<ModelFeasibilityStudysResponse>>> Handle(GetAllModelFeasibilityStudysQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                name as Nama,
                skor AS Skor 
            FROM model_feasibility_study
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<ModelFeasibilityStudysResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<ModelFeasibilityStudysResponse>>(ModelFeasibilityStudysErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
