using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Rirn.Domain.Rirn;

namespace UnpakSipaksi.Modules.Rirn.Application.GetRirn
{
    internal sealed class GetRirnDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRirnDefaultQuery, RirnDefaultResponse>
    {
        public async Task<Result<RirnDefaultResponse>> Handle(GetRirnDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama AS Nama
                 FROM rirn
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<RirnDefaultResponse?>(sql, new { Uuid = request.RirnUuid });
            if (result == null)
            {
                return Result.Failure<RirnDefaultResponse>(RirnErrors.NotFound(request.RirnUuid));
            }

            return result;
        }
    }
}
