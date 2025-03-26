using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Satuan.Domain.Satuan;

namespace UnpakSipaksi.Modules.Satuan.Application.GetSatuan
{
    internal sealed class GetSatuanQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSatuanQuery, SatuanResponse>
    {
        public async Task<Result<SatuanResponse>> Handle(GetSatuanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama AS Nama
                 FROM satuan
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<SatuanResponse?>(sql, new { request.Uuid });
            if (result == null)
            {
                return Result.Failure<SatuanResponse>(SatuanErrors.NotFound(request.Uuid));
            }

            return result;
        }
    }
}
