using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetMetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetAllMetodeRencanaKegiatan
{
    internal sealed class GetAllMetodeRencanaKegiatanQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllMetodeRencanaKegiatanQuery, List<MetodeRencanaKegiatanResponse>>
    {
        public async Task<Result<List<MetodeRencanaKegiatanResponse>>> Handle(GetAllMetodeRencanaKegiatanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 nilai as Nilai
            FROM metode_rencana_kegiatan
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<MetodeRencanaKegiatanResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<MetodeRencanaKegiatanResponse>>(MetodeRencanaKegiatanErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
