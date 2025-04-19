using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.UrgensiPenelitian.Domain.UrgensiPenelitian;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Application.GetUrgensiPenelitian
{
    internal sealed class GetUrgensiPenelitianQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUrgensiPenelitianQuery, UrgensiPenelitianResponse>
    {
        public async Task<Result<UrgensiPenelitianResponse>> Handle(GetUrgensiPenelitianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $$"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     id_relevansi_produk_kepentingan_nasional AS UuidRelevansiProdukKepentinganNasional,
                     id_ketajaman_perumusan_masalah AS UuidKetajamanPerumusanMasalah,
                     id_inovasi_pemecahan_masalah AS UuidInovasiPemecahanMasalah,
                     id_sota_kebaharuan AS UuidSotaKebaharuan,
                     id_roadmap_penelitian AS UuidRoadmapPenelitian
                 FROM urgensi_penelitian 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<UrgensiPenelitianResponse?>(sql, new { Uuid = request.UrgensiPenelitianUuid });
            if (result == null)
            {
                return Result.Failure<UrgensiPenelitianResponse>(UrgensiPenelitianErrors.NotFound(request.UrgensiPenelitianUuid));
            }

            return result;
        }
    }
}
