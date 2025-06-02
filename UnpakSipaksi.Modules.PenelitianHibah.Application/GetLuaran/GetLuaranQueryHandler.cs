using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.Luaran;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetLuaran
{
    internal sealed class GetLuaranQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetLuaranQuery, LuaranResponse>
    {
        public async Task<Result<LuaranResponse>> Handle(GetLuaranQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $""""
                SELECT 
                    CAST(NULLIF(pil.uuid, '') AS VARCHAR(36)) AS Uuid,
                    CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                    
                    CAST(NULLIF(pik.uuid, '') AS VARCHAR(36)) AS UuidKategori,
                    pik.nama AS Kategori,
                    
                    CAST(NULLIF(pikl.uuid, '') AS VARCHAR(36)) AS UuidKategoriLuaran,
                    pikl.nama AS KategoriLuaran,

                    pil.keterangan AS Keterangan,
                    pil.link AS Link,
                    pil.jenis AS Jenis
                FROM penelitian_internal_luaran pil 
                LEFT JOIN penelitian_internal pi ON pil.id_pdp = pi.id
                LEFT JOIN penelitian_internal_kategori pik ON pil.id_pdp_kategori = pik.id
                LEFT JOIN penelitian_internal_kategori_luaran pikl ON pil.id_pdp_kategori_luaran = pikl.id
                WHERE pil.uuid = @Uuid and pi.uuid = @UuidPenelitianHibah
                """";

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync(sql, new { Uuid = request.Uuid, UuidPenelitianHibah = request.UuidPenelitianHibah });
            if (result == null)
            {
                return Result.Failure<LuaranResponse>(LuaranErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            LuaranResponse response = new LuaranResponse {
                Uuid = result.Uuid?.ToString(),
                UuidPenelitianHibah = result.UuidPenelitianHibah?.ToString(),
                Kategori = (string.IsNullOrEmpty(result?.UuidKategori) && string.IsNullOrEmpty(result?.Kategori))? null : new KategoriResponse { 
                    Uuid = result.UuidKategori.ToString(),
                    Kategori = result.Kategori.ToString()
                },
                KategoriLuaran = (string.IsNullOrEmpty(result?.UuidKategoriLuaran) && string.IsNullOrEmpty(result?.KategoriLuaran)) ? null : new KategoriLuaranResponse {
                    Uuid = result.UuidKategoriLuaran.ToString(),
                    KategoriLuaran = result.KategoriLuaran.ToString()
                }
            };

            return result;
        }
    }
}
