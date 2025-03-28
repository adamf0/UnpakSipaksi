using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.GetPenugasanReviewer
{
    internal sealed class GetPenugasanReviewerQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPenugasanReviewerQuery, PenugasanReviewerResponse>
    {
        public async Task<Result<PenugasanReviewerResponse>> Handle(GetPenugasanReviewerQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     NIDN as Nidn,
                     status as Status
                 FROM penugasan_reviewer 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<PenugasanReviewerResponse?>(sql, new { Uuid = request.PenugasanReviewerUuid });
            if (result == null)
            {
                return Result.Failure<PenugasanReviewerResponse>(PenugasanReviewerErrors.NotFound(request.PenugasanReviewerUuid));
            }

            return result;
        }
    }
}
