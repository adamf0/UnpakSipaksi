using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetLuaran;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllLuaran
{
    internal sealed class GetAllLuaranQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllLuaranQuery, List<LuaranResponse>>
    {
        public async Task<Result<List<LuaranResponse>>> Handle(GetAllLuaranQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
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
            WHERE pi.uuid = @UuidPenelitianHibah
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync(sql, new { UuidPenelitianHibah = request.UuidPenelitianHibah });

            if (result == null || !result.Any())
            {
                return Result.Failure<List<LuaranResponse>>(PenelitianHibahErrors.EmptyData());
            }

            IEnumerable<LuaranResponse> list = result.Select(row => new LuaranResponse
            {
                Uuid = row.Uuid?.ToString(),
                UuidPenelitianHibah = row.UuidPenelitianHibah?.ToString(),
                Kategori = (string.IsNullOrEmpty(row?.UuidKategori) && string.IsNullOrEmpty(row?.Kategori)) ? null : new KategoriResponse
                {
                    Uuid = row.UuidKategori?.ToString(),
                    Kategori = row.Kategori?.ToString()
                },
                KategoriLuaran = (string.IsNullOrEmpty(row?.UuidKategoriLuaran) && string.IsNullOrEmpty(row?.KategoriLuaran)) ? null : new KategoriLuaranResponse
                {
                    Uuid = row.UuidKategoriLuaran?.ToString(),
                    KategoriLuaran = row.KategoriLuaran?.ToString()
                }
            });

            return Result.Success(list.ToList());
        }
    }
}
