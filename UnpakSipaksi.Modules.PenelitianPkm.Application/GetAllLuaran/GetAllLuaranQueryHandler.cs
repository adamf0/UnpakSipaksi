using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetLuaran;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllLuaran
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
                CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianPkm,
                
                CAST(NULLIF(jl.uuid, '') AS VARCHAR(36)) AS UuidLuaran,
                jl.nama AS NamaLuaran,
                
                CAST(NULLIF(ic.uuid, '') AS VARCHAR(36)) AS UuidCapaian,
                ic.nama AS NamaCapaian,
                ic.status AS StatusCapaian,
            
                pil.keterangan AS Keterangan,
                pil.link AS Link,
                pil.jenis AS Jenis
            FROM pkm_luaran pil 
            LEFT JOIN pkm pi ON pil.id_pkm = pi.id
            LEFT JOIN pkm_jenis_luaran jl ON pil.id_jenis_luaran= jl.id
            LEFT JOIN pkm_indikator_capaian ic ON pil.id_indikator_capaian = ic.id
            WHERE pi.uuid = @UuidPenelitianPkm
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync(sql, new { request.UuidPenelitianPkm });

            if (result == null || !result.Any())
            {
                return Result.Failure<List<LuaranResponse>>(PenelitianPkmErrors.EmptyData());
            }

            IEnumerable<LuaranResponse> list = result.Select(row => new LuaranResponse
            {
                Uuid = row.Uuid?.ToString(),
                UuidPenelitianPkm = row.UuidPenelitianPkm?.ToString(),
                JenisLuaran = string.IsNullOrEmpty(row?.UuidLuaran) ? null : new JenisLuaranResponse
                {
                    Uuid = row.UuidLuaran.ToString(),
                    Nama = row.NamaLuaran.ToString()
                },
                IndikatorCapaian = string.IsNullOrEmpty(row?.UuidCapaian) ? null : new IndikatorCapaianResponse
                {
                    Uuid = row.UuidCapaian.ToString(),
                    Nama = row.NamaCapaian.ToString(),
                    Status = row.StatusCapaian.ToString()
                }
            });

            return Result.Success(list.ToList());
        }
    }
}
