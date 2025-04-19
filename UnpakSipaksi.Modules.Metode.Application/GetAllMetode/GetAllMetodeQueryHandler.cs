using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Metode.Application.GetMetode;
using UnpakSipaksi.Modules.Metode.Domain.Metode;

namespace UnpakSipaksi.Modules.Metode.Application.GetAllMetode
{
    internal sealed class GetAllMetodeQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllMetodeQuery, List<MetodeResponse>>
    {
        public async Task<Result<List<MetodeResponse>>> Handle(GetAllMetodeQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
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
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<MetodeResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<MetodeResponse>>(MetodeErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
