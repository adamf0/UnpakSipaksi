using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra;

namespace UnpakSipaksi.Modules.KelompokMitra.Application.GetKelompokMitra
{
    internal sealed class GetKelompokMitraDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKelompokMitraDefaultQuery, KelompokMitraDefaultResponse>
    {
        public async Task<Result<KelompokMitraDefaultResponse>> Handle(GetKelompokMitraDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama 
                 FROM kelompokmitra 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KelompokMitraDefaultResponse?>(sql, new { Uuid = request.KelompokMitraUuid });
            if (result == null)
            {
                return Result.Failure<KelompokMitraDefaultResponse>(KelompokMitraErrors.NotFound(request.KelompokMitraUuid));
            }

            return result;
        }
    }
}
