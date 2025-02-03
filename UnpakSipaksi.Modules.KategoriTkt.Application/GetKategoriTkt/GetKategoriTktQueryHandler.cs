using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt;

namespace UnpakSipaksi.Modules.KategoriTkt.Application.GetKategoriTkt
{
    internal sealed class GetKategoriTktQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKategoriTktQuery, KategoriTktResponse>
    {
        public async Task<Result<KategoriTktResponse>> Handle(GetKategoriTktQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama
                 FROM kategori_tkt
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KategoriTktResponse?>(sql, new { Uuid = request.KategoriTktUuid });
            if (result == null)
            {
                return Result.Failure<KategoriTktResponse>(KategoriTktErrors.NotFound(request.KategoriTktUuid));
            }

            return result;
        }
    }
}
