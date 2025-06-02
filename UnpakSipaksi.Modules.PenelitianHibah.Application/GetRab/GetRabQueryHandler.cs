using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.RAB;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetRab
{
    internal sealed class GetRabQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRabQuery, RabResponse>
    {
        public async Task<Result<RabResponse>> Handle(GetRabQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $""""
                SELECT 
                    CAST(NULLIF(pir.uuid, '') AS VARCHAR(36)) AS Uuid,
                    CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                    
                    CAST(NULLIF(kr.uuid, '') AS VARCHAR(36)) AS UuidKelompokRab,
                    kr.nama AS KelompokRab,
                    
                    CAST(NULLIF(k.uuid, '') AS VARCHAR(36)) AS UuidKomponen,
                    k.nama AS Komponen,
                
                    pir.item AS Item,
                
                    CAST(NULLIF(s.uuid, '') AS VARCHAR(36)) AS UuidSatuan,
                    s.nama AS Satuan,
                
                    pir.harga_satuan AS HargaSatuan,
                    pir.total AS Total
                
                FROM penelitian_internal_rab pir 
                LEFT JOIN penelitian_internal pi ON pir.id_pdp = pi.id
                LEFT JOIN kelompok_rab kr ON pir.kelompok_rab = kr.id
                LEFT JOIN komponen k ON pir.komponen = k.id
                LEFT JOIN komponen s ON pir.satuan = s.id
                WHERE pir.uuid = @Uuid and pi.uuid = @UuidPenelitianHibah
                """";

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync(sql, new { Uuid = request.Uuid, UuidPenelitianHibah = request.UuidPenelitianHibah });
            if (result == null)
            {
                return Result.Failure<RabResponse>(RABErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            RabResponse response = new RabResponse{
                Uuid = result.Uuid?.ToString(),
                UuidPenelitianHibah = result.UuidPenelitianHibah?.ToString(),
                Item = result?.Item,
                HargaSatuan = result?.HargaSatuan,
                Total = result?.Total,
                Kelompok = (string.IsNullOrEmpty(result?.UuidKelompokRab)) ? null : new KelompokResponse
                {
                    Uuid = result.UuidKelompokRab.ToString(),
                    Kelompok = result.KelompokRab.ToString()
                },
                Komponen = (string.IsNullOrEmpty(result?.UuidKomponen)) ? null : new KomponenResponse
                {
                    Uuid = result.UuidKomponen.ToString(),
                    Komponen = result.Komponen.ToString()
                },
                Satuan = (string.IsNullOrEmpty(result?.UuidSatuan)) ? null : new SatuanResponse
                {
                    Uuid = result.UuidSatuan.ToString(),
                    Satuan = result.Satuan.ToString()
                },
            };

            return result;
        }
    }
}
