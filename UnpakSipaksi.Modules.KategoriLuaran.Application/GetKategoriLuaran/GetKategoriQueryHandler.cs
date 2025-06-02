using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori;

namespace UnpakSipaksi.Modules.KategoriLuaran.Application.GetKategoriLuaran
{
    internal sealed class GetKategoriLuaranQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKategoriLuaranQuery, KategoriLuaranResponse>
    {
        public async Task<Result<KategoriLuaranResponse>> Handle(GetKategoriLuaranQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(pikl.uuid, '') AS VARCHAR(36)) AS Uuid,
                     CAST(NULLIF(pik.uuid, '') AS VARCHAR(36)) as uuidKategori,
                     pikl.nama as Nama,
                     pikl.status AS Status
                 FROM penelitian_internal_kategori_luaran pikl
                 LEFT JOIN penelitian_internal_kategori pik ON pikl.id_pdp_kategori = pik.id
                 WHERE pikl.uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KategoriLuaranResponse?>(sql, new { Uuid = Guid.Parse(request.Uuid) });
            if (result == null)
            {
                return Result.Failure<KategoriLuaranResponse>(KategoriLuaranErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            return result;
        }
    }
}
