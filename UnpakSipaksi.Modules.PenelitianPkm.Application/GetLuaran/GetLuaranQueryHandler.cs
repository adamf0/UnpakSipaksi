using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.Luaran;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetLuaran
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
                WHERE pil.uuid = @Uuid and pi.uuid = @UuidPenelitianPkm
                """";

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync(sql, new { request.Uuid, request.UuidPenelitianPkm });
            if (result == null)
            {
                return Result.Failure<LuaranResponse>(LuaranErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            LuaranResponse response = new LuaranResponse
            {
                Uuid = result.Uuid?.ToString(),
                UuidPenelitianPkm = result.UuidPenelitianPkm?.ToString(),
                JenisLuaran = string.IsNullOrEmpty(result?.UuidLuaran) ? null : new JenisLuaranResponse
                {
                    Uuid = result.UuidLuaran.ToString(),
                    Nama = result.NamaLuaran.ToString()
                },
                IndikatorCapaian = string.IsNullOrEmpty(result?.UuidCapaian) ? null : new IndikatorCapaianResponse
                {
                    Uuid = result.UuidCapaian.ToString(),
                    Nama = result.NamaCapaian.ToString(),
                    Status = result.StatusCapaian.ToString()
                }
            };

            return response;
        }
    }
}
