using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.Substansi;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetSubstansiUsulan
{
    internal sealed class GetSubstansiUsulanQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSubstansiUsulanQuery, SubstansiUsulanResponse>
    {
        public async Task<Result<SubstansiUsulanResponse>> Handle(GetSubstansiUsulanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $""""
                SELECT 
                    CAST(NULLIF(md.uuid, '') AS VARCHAR(36)) AS Uuid,
                    CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                    md.File AS file
                FROM penelitian_internal_substansi_usulan md 
                LEFT JOIN penelitian_internal pi ON md.id_pdp = pi.id
                WHERE md.uuid = @Uuid 
                """";

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<SubstansiUsulanResponse>(sql, new { Uuid = request.UuidPenelitianHibah });
            if (result == null)
            {
                return Result.Failure<SubstansiUsulanResponse>(SubstansiErrors.NotFound(Guid.Parse(request.UuidPenelitianHibah)));
            }

            return result;
        }
    }
}
