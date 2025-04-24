using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.GetKategoriSkema
{
    internal sealed class GetKategoriSkemaDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKategoriSkemaDefaultQuery, KategoriSkemaDefaultResponse>
    {
        public async Task<Result<KategoriSkemaDefaultResponse>> Handle(GetKategoriSkemaDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     rule as Rule
                 FROM kategori_skema 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KategoriSkemaDefaultResponse?>(sql, new { Uuid = request.KategoriSkemaUuid });
            if (result == null)
            {
                return Result.Failure<KategoriSkemaDefaultResponse>(KategoriSkemaErrors.NotFound(request.KategoriSkemaUuid));
            }

            return result;
        }
    }
}
