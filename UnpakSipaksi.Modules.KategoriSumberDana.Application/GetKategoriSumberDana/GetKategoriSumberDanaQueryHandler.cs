using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Application.GetKategoriSumberDana
{
    internal sealed class GetKategoriSumberDanaQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKategoriSumberDanaQuery, KategoriSumberDanaResponse>
    {
        public async Task<Result<KategoriSumberDanaResponse>> Handle(GetKategoriSumberDanaQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama
                 FROM kategori_sumber_dana 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KategoriSumberDanaResponse?>(sql, new { Uuid = request.KategoriSumberDanaUuid });
            if (result == null)
            {
                return Result.Failure<KategoriSumberDanaResponse>(KategoriSumberDanaErrors.NotFound(request.KategoriSumberDanaUuid));
            }

            return result;
        }
    }
}
