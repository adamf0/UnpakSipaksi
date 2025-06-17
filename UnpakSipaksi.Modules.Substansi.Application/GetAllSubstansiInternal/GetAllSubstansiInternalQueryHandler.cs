using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Substansi.Application.GetSubstansiInternal;
using UnpakSipaksi.Modules.Substansi.Domain.SubstansiInternal;

namespace UnpakSipaksi.Modules.Substansi.Application.GetAllSubstansiInternal
{
    internal sealed class GetAllSubstansiInternalQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllSubstansiInternalQuery, List<SubstansiInfoInternalResponse>>
    {
        public async Task<Result<List<SubstansiInfoInternalResponse>>> Handle(GetAllSubstansiInternalQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(pis.Uuid, '') AS VARCHAR(36)) AS Uuid,
                sp.name AS NamaPublikasiDisitasiProposal,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN sp.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN sp.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN sp.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN sp.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotPublikasiDisitasiProposal,

                ktp.name AS NamaRelevansiKepakaranTemaProposal,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN ktp.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN ktp.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN ktp.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN ktp.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotRelevansiKepakaranTemaProposal,

                kpb.name as NamaJumlahKolaboratorPublikasiBereputasi,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN kpb.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN kpb.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN kpb.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN kpb.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotJumlahKolaboratorPublikasiBereputasi,

                pkn.name as NamaRelevansiProdukKepentinganNasional,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN pkn.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN pkn.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN pkn.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN pkn.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotRelevansiProdukKepentinganNasional,

                kpm.name as NamaKetajamanPerumusanMasalah,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN kpm.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN kpm.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN kpm.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN kpm.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotKetajamanPerumusanMasalah,

                ipm.name as NamaInovasiPemecahanMasalah,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN ipm.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN ipm.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN ipm.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN ipm.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotInovasiPemecahanMasalah,

                sk.name as NamaSotaKebaharuan,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN sk.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN sk.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN sk.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN sk.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotSotaKebaharuan,

                sp.name NamaRoadmapPenelitian,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN sp.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN sp.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN sp.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN sp.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotRoadmapPenelitian,

                ap.name as NamaAkurasiPenelitian,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN ap.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN ap.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN ap.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN ap.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotAkurasiPenelitian,

                ptt.name as NamaKejelasanPembagianTugasTim,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN ptt.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN ptt.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN ptt.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN ptt.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotKejelasanPembagianTugasTim,

                rlf.name as NamaKesesuaianWaktuRabLuaranFasilitas,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN rlf.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN rlf.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN rlf.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN rlf.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotKesesuaianWaktuRabLuaranFasilitas,

                kld.name as NamaPotensiKetercapaianLuaranDijanjikan,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN kld.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN kld.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN kld.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN kld.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotPotensiKetercapaianLuaranDijanjikan,

                mfs.name as NamaModelFeasibilityStudy,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN mfs.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN mfs.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN mfs.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN mfs.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotModelFeasibilityStudy,

                kt.name as NamaKesesuaianTkt,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN kt.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN kt.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN kt.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN kt.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotKesesuaianTkt,

                kmd.name as NamaKredibilitasMitraDukungan,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN kmd.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN kmd.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN kmd.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN kmd.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotKredibilitasMitraDukungan,

                kr.name as NamaKebaruanReferensi,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN kr.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN kr.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN kr.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN kr.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotKebaruanReferensi,

                rkr.name as NamaRelevansiKualitasReferensi,
                (
                    CASE
                        WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN rkr.bobot_pdp
                        WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN rkr.bobot_penelitian_dasar
                        WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN rkr.bobot_terapan
                        WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN rkr.bobot_kerjasama
                        ELSE 0
                    END
                ) as BobotRelevansiKualitasReferensi,
                (
                    COALESCE(sp.skor, 0) * (
                        CASE
                            WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN sp.bobot_pdp
                            WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN sp.bobot_penelitian_dasar
                            WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN sp.bobot_terapan
                            WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN sp.bobot_kerjasama
                            ELSE 0
                        END
                    )
                    + COALESCE(ktp.skor, 0) * (
                        CASE
                            WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN ktp.bobot_pdp
                            WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN ktp.bobot_penelitian_dasar
                            WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN ktp.bobot_terapan
                            WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN ktp.bobot_kerjasama
                            ELSE 0
                        END
                    )
                    + COALESCE(kpb.skor, 0) * (
                        CASE
                            WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN kpb.bobot_pdp
                            WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN kpb.bobot_penelitian_dasar
                            WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN kpb.bobot_terapan
                            WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN kpb.bobot_kerjasama
                            ELSE 0
                        END
                    )
                    + COALESCE(pkn.skor, 0) * (
                        CASE
                            WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN pkn.bobot_pdp
                            WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN pkn.bobot_penelitian_dasar
                            WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN pkn.bobot_terapan
                            WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN pkn.bobot_kerjasama
                            ELSE 0
                        END
                    )
                    + COALESCE(kpm.skor, 0) * (
                        CASE
                            WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN kpm.bobot_pdp
                            WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN kpm.bobot_penelitian_dasar
                            WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN kpm.bobot_terapan
                            WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN kpm.bobot_kerjasama
                            ELSE 0
                        END
                    )
                    + COALESCE(ipm.skor, 0) * (
                        CASE
                            WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN ipm.bobot_pdp
                            WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN ipm.bobot_penelitian_dasar
                            WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN ipm.bobot_terapan
                            WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN ipm.bobot_kerjasama
                            ELSE 0
                        END
                    )
                    + COALESCE(sk.skor, 0) * (
                        CASE
                            WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN sk.bobot_pdp
                            WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN sk.bobot_penelitian_dasar
                            WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN sk.bobot_terapan
                            WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN sk.bobot_kerjasama
                            ELSE 0
                        END
                    )
                    + COALESCE(rp.skor, 0) * (
                        CASE
                            WHEN LOWER(pi.nama) LIKE '%dosen pemula%' THEN rp.bobot_pdp
                            WHEN LOWER(pi.nama) LIKE '%penelitian dasar%' THEN rp.bobot_penelitian_dasar
                            WHEN LOWER(pi.nama) LIKE '%penelitian terapan%' THEN rp.bobot_terapan
                            WHEN LOWER(pi.nama) LIKE '%penelitian kolaborasi%' THEN rp.bobot_kerjasama
                            ELSE 0
                        END
                    )
                ) AS Total,
                pis.Komentar AS Komentar,
                pis.reviewerInternal AS ReviewerInternal,
                pis.reviewerExternal AS ReviewerExternal,
                DATE_FORMAT(pis.tanggal_mulai, '%Y-%m-%d') As TanggalMulai,
                DATE_FORMAT(pis.tanggal_berakhir, '%Y-%m-%d') as TanggalBerakhir
            FROM penelitian_internal_substansi pis 
            LEFT JOIN (
                SELECT penelitian_internal.id, penelitian_internal.uuid, penelitian_internal.id_skema, kategori_skema.nama
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
            WHERE pi.uuid = @UuidPenelitianHibah
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<SubstansiInfoInternalResponse>(sql, new { UuidPenelitianHibah = request.UuidPenelitianHibah });

            if (result == null || !result.Any())
            {
                return Result.Failure<List<SubstansiInfoInternalResponse>>(SubstansiInternalErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
