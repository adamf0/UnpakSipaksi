using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetRab;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllRab
{
    internal sealed class GetAllRabQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllRabQuery, List<RabResponse>>
    {
        public async Task<Result<List<RabResponse>>> Handle(GetAllRabQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
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
            WHERE pi.uuid = @UuidPenelitianHibah
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync(sql, new { UuidPenelitianHibah = request.UuidPenelitianHibah });

            if (result == null || !result.Any())
            {
                return Result.Failure<List<RabResponse>>(PenelitianHibahErrors.EmptyData());
            }

            IEnumerable<RabResponse> list = result.Select(row => new RabResponse
            {
                Uuid = row.Uuid?.ToString(),
                UuidPenelitianHibah = row.UuidPenelitianHibah?.ToString(),
                Item = row?.Item,
                HargaSatuan = row?.HargaSatuan,
                Total = row?.Total,
                Kelompok = (string.IsNullOrEmpty(row?.UuidKelompokRab)) ? null : new KelompokResponse
                {
                    Uuid = row.UuidKelompokRab.ToString(),
                    Kelompok = row.KelompokRab.ToString()
                },
                Komponen = (string.IsNullOrEmpty(row?.UuidKomponen)) ? null : new KomponenResponse
                {
                    Uuid = row.UuidKomponen.ToString(),
                    Komponen = row.Komponen.ToString()
                },
                Satuan = (string.IsNullOrEmpty(row?.UuidSatuan)) ? null : new SatuanResponse
                {
                    Uuid = row.UuidSatuan.ToString(),
                    Satuan = row.Satuan.ToString()
                },
            });

            return Result.Success(list.ToList());
        }
    }
}
