using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokRab.Domain.KelompokRab;

namespace UnpakSipaksi.Modules.KelompokRab.Application.GetKelompokRab
{
    internal sealed class GetKelompokRabDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKelompokRabDefaultQuery, KelompokRabDefaultResponse>
    {
        public async Task<Result<KelompokRabDefaultResponse>> Handle(GetKelompokRabDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama
                 FROM kelompok_rab
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KelompokRabDefaultResponse?>(sql, new { Uuid = request.KelompokRabUuid });
            if (result == null)
            {
                return Result.Failure<KelompokRabDefaultResponse>(KelompokRabErrors.NotFound(request.KelompokRabUuid));
            }

            return result;
        }
    }
}
