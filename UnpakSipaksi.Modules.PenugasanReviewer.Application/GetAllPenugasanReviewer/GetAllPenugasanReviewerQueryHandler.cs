using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.GetPenugasanReviewer;
using UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.GetAllPenugasanReviewer
{
    internal sealed class GetAllPenugasanReviewerQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllPenugasanReviewerQuery, List<PenugasanReviewerResponse>>
    {
        public async Task<Result<List<PenugasanReviewerResponse>>> Handle(GetAllPenugasanReviewerQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 NIDN as Nidn,
                 status as Status
            FROM penugasan_reviewer
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<PenugasanReviewerResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<PenugasanReviewerResponse>>(PenugasanReviewerErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
