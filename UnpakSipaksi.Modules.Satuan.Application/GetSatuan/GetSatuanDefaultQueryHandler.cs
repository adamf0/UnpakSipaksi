using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Satuan.Domain.Satuan;

namespace UnpakSipaksi.Modules.Satuan.Application.GetSatuan
{
    internal sealed class GetSatuanDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSatuanDefaultQuery, SatuanDefaultResponse>
    {
        public async Task<Result<SatuanDefaultResponse>> Handle(GetSatuanDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id AS Id
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama AS Nama
                 FROM satuan
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<SatuanDefaultResponse?>(sql, new { Uuid = request.SatuanUuid });
            if (result == null)
            {
                return Result.Failure<SatuanDefaultResponse>(SatuanErrors.NotFound(request.SatuanUuid));
            }

            return result;
        }
    }
}
