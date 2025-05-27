using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.Domain.Kategori;

namespace UnpakSipaksi.Modules.Kategori.Application.GetKategori
{
    internal sealed class GetKategoriQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKategoriQuery, KategoriResponse>
    {
        public async Task<Result<KategoriResponse>> Handle(GetKategoriQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama 
                 FROM penelitian_internal_kategori 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KategoriResponse?>(sql, new { Uuid = Guid.Parse(request.Uuid) });
            if (result == null)
            {
                return Result.Failure<KategoriResponse>(KategoriErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            return result;
        }
    }
}
