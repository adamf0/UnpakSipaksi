using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.GetUrgensiPenelitian;
using UnpakSipaksi.Modules.UrgensiPenelitian.Domain.UrgensiPenelitian;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Application.GetAllUrgensiPenelitian
{
    internal sealed class GetAllUrgensiPenelitianQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllUrgensiPenelitianQuery, List<UrgensiPenelitianResponse>>
    {
        public async Task<Result<List<UrgensiPenelitianResponse>>> Handle(GetAllUrgensiPenelitianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                id_relevansi_produk_kepentingan_nasional AS UuidRelevansiProdukKepentinganNasional,
                id_ketajaman_perumusan_masalah AS UuidKetajamanPerumusanMasalah,
                id_inovasi_pemecahan_masalah AS UuidInovasiPemecahanMasalah,
                id_sota_kebaharuan AS UuidSotaKebaharuan,
                id_roadmap_penelitian AS UuidRoadmapPenelitian
            FROM urgensi_penelitian 
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<UrgensiPenelitianResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<UrgensiPenelitianResponse>>(UrgensiPenelitianErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
