using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman;

namespace UnpakSipaksi.Modules.Pengumuman.Application.GetPengumuman
{
    internal sealed class GetPengumumanQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPengumumanQuery, PengumumanResponse>
    {
        public async Task<Result<PengumumanResponse>> Handle(GetPengumumanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     isi as Pesan,
                     file as File,
                     url as Url,
                     type as Type,
                     type_target as Target,
                     nidn as Nidn,
                     kode_fakultas as KodeFakultas,
                     type_expire as TypeExpired,
                     tanggal_awal as TanggalAwal,
                     tanggal_akhir as TanggalAkhir
                 FROM pengumuman 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<PengumumanResponse?>(sql, new { Uuid = request.PengumumanUuid });
            if (result == null)
            {
                return Result.Failure<PengumumanResponse>(PengumumanErrors.NotFound(request.PengumumanUuid));
            }

            return result;
        }
    }
}
