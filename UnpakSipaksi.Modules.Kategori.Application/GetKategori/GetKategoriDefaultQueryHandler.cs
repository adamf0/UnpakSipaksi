using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.Domain.Kategori;

namespace UnpakSipaksi.Modules.Kategori.Application.GetKategori
{
    internal sealed class GetKategoriDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKategoriDefaultQuery, KategoriDefaultResponse>
    {
        public async Task<Result<KategoriDefaultResponse>> Handle(GetKategoriDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama 
                 FROM penelitian_internal_kategori 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KategoriDefaultResponse?>(sql, new { Uuid = request.KategoriUuid });
            if (result == null)
            {
                return Result.Failure<KategoriDefaultResponse>(KategoriErrors.NotFound(request.KategoriUuid));
            }

            return result;
        }
    }
}
