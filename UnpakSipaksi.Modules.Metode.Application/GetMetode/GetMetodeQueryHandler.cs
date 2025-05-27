using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Metode.Domain.Metode;

namespace UnpakSipaksi.Modules.Metode.Application.GetMetode
{
    internal sealed class GetMetodeQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMetodeQuery, MetodeResponse>
    {
        public async Task<Result<MetodeResponse>> Handle(GetMetodeQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     ap.uuid AS AkurasiPenelitianId,
                     kptt.uuid AS KejelasanPembagianTugasTimId,
                     kwrlf.uuid AS KesesuaianWaktuRabLuaranFasilitasId,
                     pkld.uuid AS PotensiKetercapaianLuaranDijanjikanId,
                     mfs.uuid AS ModelFeasibilityStudyId,
                     kt.uuid AS KesesuaianTktId,
                     kmd.uuid AS KredibilitasMitraDukunganId
                 FROM metode m
                 LEFT JOIN akurasi_penelitian ap ON m.id_akurasi_penelitian = ap.id
                 LEFT JOIN kejelasan_pembagian_tugas_tim kptt ON m.id_kejelasan_pembagian_tugas_tim = kptt.id
                 LEFT JOIN kesesuaian_waktu_rab_luaran_fasilitas kwrlf ON m.id_kesesuaian_waktu_rab_luaran_fasilitas = kwrlf.id
                 LEFT JOIN potensi_ketercapaian_luaran_dijanjikan pkld ON m.id_potensi_ketercapaian_luaran_dijanjikan = pkld.id
                 LEFT JOIN model_feasibility_study mfs ON m.id_model_feasibility_study = mfs.id
                 LEFT JOIN kt ap ON m.id_kesesuaian_tkt = kt.id
                 LEFT JOIN kredibilitas_mitra_dukungan kmd ON m.id_kredibilitas_mitra_dukungan = kmd.id 
                 WHERE uuid = @MetodeUuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<MetodeResponse?>(sql, new { Uuid = request.MetodeUuid });
            if (result == null)
            {
                return Result.Failure<MetodeResponse>(MetodeErrors.NotFound(request.MetodeUuid));
            }

            return result;
        }
    }
}
