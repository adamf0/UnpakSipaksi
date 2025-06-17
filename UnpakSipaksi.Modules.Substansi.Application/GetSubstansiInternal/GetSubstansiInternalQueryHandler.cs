using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Substansi.Domain.SubstansiInternal;

namespace UnpakSipaksi.Modules.Substansi.Application.GetSubstansiInternal
{
    internal sealed class GetSubstansiInternalQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSubstansiInternalQuery, SubstansiInternalResponse>
    {
        public async Task<Result<SubstansiInternalResponse>> Handle(GetSubstansiInternalQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(pis.Uuid, '') AS VARCHAR(36)) AS Uuid,
                     CAST(NULLIF(sp.Uuid, '') AS VARCHAR(36)) AS UuidPublikasiDisitasiProposal,                 
                     CAST(NULLIF(ktp.Uuid, '') AS VARCHAR(36)) AS UuidRelevansiKepakaranTemaProposal,                
                     CAST(NULLIF(kpb.Uuid, '') AS VARCHAR(36)) as UuidJumlahKolaboratorPublikasiBereputasi,                 
                     CAST(NULLIF(pkn.Uuid, '') AS VARCHAR(36)) as UuidRelevansiProdukKepentinganNasional,                 
                     CAST(NULLIF(kpm.Uuid, '') AS VARCHAR(36)) as UuidKetajamanPerumusanMasalah,
                     CAST(NULLIF(ipm.Uuid, '') AS VARCHAR(36)) as UuidInovasiPemecahanMasalah,
                     CAST(NULLIF(sk.Uuid, '') AS VARCHAR(36)) as UuidSotaKebaharuan,
                     CAST(NULLIF(sp.Uuid, '') AS VARCHAR(36)) UuidRoadmapPenelitian,
                     CAST(NULLIF(ap.Uuid, '') AS VARCHAR(36)) as UuidAkurasiPenelitian,
                     CAST(NULLIF(ptt.Uuid, '') AS VARCHAR(36)) as UuidKejelasanPembagianTugasTim,
                     CAST(NULLIF(rlf.Uuid, '') AS VARCHAR(36)) as UuidKesesuaianWaktuRabLuaranFasilitas,
                     CAST(NULLIF(kld.Uuid, '') AS VARCHAR(36)) as UuidPotensiKetercapaianLuaranDijanjikan,
                     CAST(NULLIF(mfs.Uuid, '') AS VARCHAR(36)) as UuidModelFeasibilityStudy,
                     CAST(NULLIF(kt.Uuid, '') AS VARCHAR(36)) as UuidKesesuaianTkt,
                     CAST(NULLIF(kmd.Uuid, '') AS VARCHAR(36)) as UuidKredibilitasMitraDukungan,
                     CAST(NULLIF(kr.Uuid, '') AS VARCHAR(36)) as UuidKebaruanReferensi,
                     CAST(NULLIF(rkr.Uuid, '') AS VARCHAR(36)) as UuidRelevansiKualitasReferensi,
                     pis.Komentar AS Komentar,
                     pis.reviewerInternal AS ReviewerInternal,
                     pis.reviewerExternal AS ReviewerExternal,
                     DATE_FORMAT(pis.tanggal_mulai, '%Y-%m-%d') As TanggalMulai,
                     DATE_FORMAT(pis.tanggal_berakhir, '%Y-%m-%d') as TanggalBerakhir
                 FROM penelitian_internal_substansi pis 
                 LEFT JOIN (
                     SELECT penelitian_internal.id, penelitian_internal.id_skema, kategori_skema.nama
                     FROM penelitian_internal 
                     LEFT JOIN kategori_skema ON penelitian_internal.id_skema = kategori_skema.id
                 ) pi ON pis.id_pdp = pi.id 
                 LEFT JOIN publikasi_disitasi_proposal sp ON pis.id_publikasi_disitasi_proposal = sp.id 
                 LEFT JOIN relevansi_kepakaran_tema_proposal ktp ON pis.id_relevansi_kepakaran_tema_proposal = ktp.id 
                 LEFT JOIN jumlah_kolaborator_publikasi_bereputasi kpb ON pis.id_jumlah_kolaborator_publikasi_bereputasi = kpb.id 
                 LEFT JOIN relevansi_produk_kepentingan_nasional pkn ON pis.id_relevansi_produk_kepentingan_nasional = pkn.id 
                 LEFT JOIN ketajaman_perumusan_masalah kpm ON pis.id_ketajaman_perumusan_masalah = kpm.id 
                 LEFT JOIN inovasi_pemecahan_masalah ipm ON pis.id_inovasi_pemecahan_masalah = ipm.id 
                 LEFT JOIN sota_kebaharuan sk ON pis.id_sota_kebaharuan = sk.id 
                 LEFT JOIN roadmap_penelitian rp ON pis.id_roadmap_penelitian = rp.id 
                 LEFT JOIN akurasi_penelitian ap ON pis.id_akurasi_penelitian = ap.id 
                 LEFT JOIN kejelasan_pembagian_tugas_tim ptt ON pis.id_kejelasan_pembagian_tugas_tim = ptt.id 
                 LEFT JOIN kesesuaian_waktu_rab_luaran_fasilitas rlf ON pis.id_kesesuaian_waktu_rab_luaran_fasilitas = rlf.id 
                 LEFT JOIN potensi_ketercapaian_luaran_dijanjikan kld ON pis.id_potensi_ketercapaian_luaran_dijanjikan = kld.id 
                 LEFT JOIN model_feasibility_study mfs ON pis.id_model_feasibility_study = mfs.id 
                 LEFT JOIN kesesuaian_tkt kt ON pis.id_kesesuaian_tkt = kt.id 
                 LEFT JOIN kredibilitas_mitra_dukungan kmd ON pis.id_kredibilitas_mitra_dukungan = kmd.id 
                 LEFT JOIN kebaruan_referensi kr ON pis.id_kebaruan_referensi = kr.id 
                 LEFT JOIN relevansi_kualitas_referensi rkr ON pis.id_relevansi_kualitas_referensi = rkr.id
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<SubstansiInternalResponse?>(sql, new { Uuid = request.UuidSubstansiInternal });
            if (result == null)
            {
                return Result.Failure<SubstansiInternalResponse>(SubstansiInternalErrors.NotFound(request.UuidSubstansiInternal));
            }

            return result;
        }
    }
}
