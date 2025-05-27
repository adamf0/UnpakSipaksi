using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriLuaran.Application.GetKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori;

namespace UnpakSipaksi.Modules.KategoriLuaran.Application.GetAllKategoriLuaran
{
    internal sealed class GetAllKategoriLuaranQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKategoriLuaranQuery, List<KategoriLuaranResponse>>
    {
        public async Task<Result<List<KategoriLuaranResponse>>> Handle(GetAllKategoriLuaranQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(pikl.uuid, '') AS VARCHAR(36)) AS Uuid,
                CAST(NULLIF(pik.uuid, '') AS VARCHAR(36)) as uuidKategori,
                pikl.nama as Nama,
                pikl.status as Status
            FROM penelitian_internal_kategori_luaran pikl
            LEFT JOIN penelitian_internal_kategori pik ON pikl.id_pdp_kategori = pik.id
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KategoriLuaranResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KategoriLuaranResponse>>(KategoriLuaranErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
