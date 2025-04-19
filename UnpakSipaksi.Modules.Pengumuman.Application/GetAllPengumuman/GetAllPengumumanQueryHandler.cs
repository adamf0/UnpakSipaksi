using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Pengumuman.Application.GetPengumuman;
using UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman;

namespace UnpakSipaksi.Modules.Pengumuman.Application.GetAllPengumuman
{
    internal sealed class GetAllPengumumanQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllPengumumanQuery, List<PengumumanResponse>>
    {
        public async Task<Result<List<PengumumanResponse>>> Handle(GetAllPengumumanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
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
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<PengumumanResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<PengumumanResponse>>(PengumumanErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
