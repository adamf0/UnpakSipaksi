using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllDokumenMitra;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetDokumenMitra;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllDokumenPendukung
{
    internal sealed class GetAllDokumenMitraQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllDokumenMitraQuery, List<DokumenMitraResponse>>
    {
        public async Task<Result<List<DokumenMitraResponse>>> Handle(GetAllDokumenMitraQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
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
            WHERE p.uuid = @UuidPenelitianPkm
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<DokumenMitraResponse>(sql, new { UuidPenelitianPkm = request.UuidPenelitianPkm });

            if (result == null || !result.Any())
            {
                return Result.Failure<List<DokumenMitraResponse>>(PenelitianPkmErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
