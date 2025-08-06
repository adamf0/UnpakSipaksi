using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenMitra;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetDokumenMitra
{
    internal sealed class GetDokumenMitraQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetDokumenMitraQuery, DokumenMitraResponse>
    {
        public async Task<Result<DokumenMitraResponse>> Handle(GetDokumenMitraQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $""""
                SELECT 
                    CAST(NULLIF(dm.uuid, '') AS VARCHAR(36)) AS Uuid,
                    CAST(NULLIF(p.uuid, '') AS VARCHAR(36)) AS UuidPenelitianPkm,
                    dm.mitra AS Mitra,
                    dm.provinsi AS Provinsi,
                    dm.kota AS Kota,
                    
                    CAST(NULLIF(km.uuid, '') AS VARCHAR(36)) AS UuidKelompokMitra,
                    km.nama AS NamaKelompokMitra,
                    
                    dm.pemimpinMitra AS PemimpinMitra,
                    dm.kontakMitra AS KontakMitra,
                    dm.suratPernyataan AS File
                FROM pkm_mitra dm 
                LEFT JOIN pkm p ON dm.id_pkm = p.id
                LEFT JOIN kelompokmitra km ON dm.kelompokMitra = km.id 
                WHERE dm.uuid = @Uuid and p.uuid = @UuidPenelitianPkm
                """";

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<DokumenMitraResponse>(sql, new { Uuid = request.Uuid, UuidPenelitianPkm = request.UuidPenelitianPkm });
            if (result == null)
            {
                return Result.Failure<DokumenMitraResponse>(DokumenMitraErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            return result;
        }
    }
}
